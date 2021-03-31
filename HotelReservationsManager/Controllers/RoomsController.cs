using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Entities;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.Room;
using HotelReservationsManager.Models.Shared;
using DataLibrary.Enumeration;
using HotelReservationsManager.Models.Rooms;
using HotelReservationsManager.Models.Validation;
using HotelReservationsManager.Models.Filters;

namespace HotelReservationsManager.Controllers
{
    public class RoomsController : Controller
    {
        private readonly int PageSize = GlobalVar.AmountOfElementsDisplayedPerPage;
        private readonly HotelDbContext _context;

        public RoomsController()
        {
            _context = new HotelDbContext();
        }

        public IActionResult ChangePageSize(int id)
        {
            if (id > 0)
            {
                GlobalVar.AmountOfElementsDisplayedPerPage = id;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index(RoomsIndexViewModel model)
        {

            UpdateRoomOccupacity();


            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            var contextDb = Filter(_context.Rooms.ToList(), model.Filter);

            List<RoomsViewModel> items = contextDb.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new RoomsViewModel()
            {
                Id = c.Id,
                Number = c.Number,
                BedPriceForAdult = c.BedPriceForAdult,
                BedPriceForKid = c.BedPriceForKid,
                Type = (RoomTypeEnum)c.Type,
                Capacity = c.Capacity,
                IsFree = c.IsFree
            }).ToList();

            model.Items = items;
            model.Pager.PagesCount = Math.Max(1, (int)Math.Ceiling(contextDb.Count() / (double)PageSize));

            return View(model);
        }

        public IActionResult Create()
        {
            if (GlobalVar.LoggedOnUserRights != GlobalVar.UserRights.Admininstrator)
            {
                return RedirectToAction("LogInPermissionDenied", "Users");
            }

            return View(new RoomsCreateViewModel());
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomsCreateViewModel createModel)
        {

            if (GlobalVar.LoggedOnUserRights != GlobalVar.UserRights.Admininstrator)
            {
                return RedirectToAction("LogInPermissionDenied", "Users");
            }
            createModel.Message = null;
            if (ModelState.IsValid)
            {

                try
                {
                    Validate(new Validation_Room()
                    {
                        Capacity = createModel.Capacity,
                        Number = createModel.Number
                    });
                }
                catch (InvalidOperationException e)
                {
                    createModel.Message = e.Message;
                    return View(createModel);
                }



                if (_context.Rooms.Where(x => x.Number == createModel.Number).Count() > 0)
                {
                    createModel.Message = $"Room cant be created becuase there's already an existing room with the given number ({createModel.Number})";
                    return View(createModel);
                }

                Room room = new Room
                {
                    Number = createModel.Number,
                    BedPriceForAdult = createModel.PriceAdult,
                    BedPriceForKid = createModel.PriceChild,
                    Type = (int)createModel.RoomType,
                    Capacity = createModel.Capacity
                };

                _context.Rooms.Add(room);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {

            if (GlobalVar.LoggedOnUserRights != GlobalVar.UserRights.Admininstrator)
            {
                return RedirectToAction("LogInPermissionDenied", "Users");
            }

            if (id == null || !RoomExists((int)id))
            {
                return NotFound();
            }

            Room room = _context.Rooms.Find(id);

            RoomsEditViewModel model = new RoomsEditViewModel
            {
                Id = room.Id,
                Number = room.Number,
                PriceAdult = room.BedPriceForAdult,
                PriceChild = room.BedPriceForKid,
                RoomType = (RoomTypeEnum)room.Type,
                Capacity = room.Capacity,
                IsFree = room.IsFree
            };

            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomsEditViewModel editModel)
        {

            if (GlobalVar.LoggedOnUserRights != GlobalVar.UserRights.Admininstrator)
            {
                return RedirectToAction("LogInPermissionDenied", "Users");
            }

            if (ModelState.IsValid)
            {

                if (!RoomExists(editModel.Id))
                {
                    return NotFound();
                }

                try
                {
                    Validate(new Validation_Room()
                    {
                        Capacity = editModel.Capacity,
                        Number = editModel.Number
                    });
                }
                catch (InvalidOperationException e)
                {
                    editModel.Message = e.Message;
                    return View(editModel);
                }

                Room room = new Room()
                {
                    Id = editModel.Id,
                    Number = editModel.Number,
                    BedPriceForAdult = editModel.PriceAdult,
                    BedPriceForKid = editModel.PriceChild,
                    Type = (int)editModel.RoomType,
                    Capacity = editModel.Capacity
                };

                _context.Update(room);
                _context.SaveChanges();

                UpdateAllReservationsOverallPriceRelatedToRoom(room.Id);

                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
        }

        public IActionResult Delete(int? id)
        {

            if (GlobalVar.LoggedOnUserRights != GlobalVar.UserRights.Admininstrator)
            {
                return RedirectToAction("LogInPermissionDenied", "Users");
            }

            if (id == null || !RoomExists((int)id))
            {
                return NotFound();
            }

            Room room = _context.Rooms.Find(id);
            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        private void UpdateRoomOccupacity()
        {
            foreach (var room in _context.Rooms.ToList())
            {
                var reservations = _context.Reservations.Where(x => x.RoomId == room.Id);
                bool isFree = true;
                foreach (var reservation in reservations)
                {
                    if (reservation.AccommodationDate.AddHours(GlobalVar.DefaultReservationHourStart) < DateTime.UtcNow && DateTime.UtcNow < reservation.LeaveDate.AddHours(GlobalVar.DefaultReservationHourStart))
                    {
                        isFree = false;
                        break;
                    }
                }
                room.IsFree = isFree;
                _context.Rooms.Update(room);
                _context.SaveChanges();
            }

        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }

        private void Validate(Validation_Room model)
        {

            if (model.Number <= 0)
            {
                throw new InvalidOperationException("Room number must be positive integer");
            }

            if (model.Capacity <= 0)
            {
                throw new InvalidOperationException("Room capacity must be positive integer");
            }

        }

        private List<Room> Filter(List<Room> collection, RoomsFilterViewModel filterModel)
        {

            if (filterModel != null)
            {
                if (filterModel.Capacity != null)
                {
                    collection = collection.Where(x => x.Capacity == filterModel.Capacity).ToList();
                }
                if (filterModel.Type != null)
                {
                    collection = collection.Where(x => x.Type == (int)filterModel.Type).ToList();
                }
                if (filterModel.IsFree != null)
                {
                    collection = collection.Where(x => x.IsFree == filterModel.IsFree).ToList();
                }

            }

            return collection;
        }

        private void UpdateAllReservationsOverallPriceRelatedToRoom(int roomId)
        {
            List<Reservation> reservations = _context.Reservations.Where(x => x.RoomId == roomId).ToList();

            foreach (var reservation in reservations)
            {
                int days = CalculateDaysPassed(reservation.AccommodationDate, reservation.LeaveDate);
                List<int> clientsId = _context.ClientReservation.Where(x => x.ReservationId == reservation.Id).Select(x => x.ClientId).ToList();
                decimal bill = 0;
                Room room = _context.Rooms.Find(reservation.RoomId);
                foreach (var clientId in clientsId)
                {
                    bill += (_context.Clients.Find(clientId).IsAdult) ? (room.BedPriceForAdult) : (room.BedPriceForKid);
                }
                bill = AddExtras(bill, reservation.AllInclusive, reservation.BreakfastIncluded);
                reservation.Cost = bill;
                _context.Reservations.Update(reservation);
                _context.SaveChanges();
            }

        }

        private int CalculateDaysPassed(DateTime startDate, DateTime endDate)
        {
            double daysDiffDouble = (endDate - startDate).TotalDays;

            int daysDiff = (int)daysDiffDouble;
            if (daysDiffDouble > daysDiff)
            {
                daysDiff++;
            }

            return daysDiff;

        }

        private decimal AddExtras(decimal money, bool isAllInclusive, bool isBreakfastIncluded)
        {
            decimal bonusPercentage = 0;
            if (isAllInclusive)
            {
                bonusPercentage += GlobalVar.AllInclusiveExtraBillPercentage;
            }
            if (isBreakfastIncluded)
            {
                bonusPercentage += GlobalVar.InlcludedBreakfastExtraBillPercentage;
            }
            return money * (1 + bonusPercentage / 100);
        }

    }
}
