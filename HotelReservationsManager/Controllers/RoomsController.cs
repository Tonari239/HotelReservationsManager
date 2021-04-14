using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
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
=======
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Room;
>>>>>>> Stashed changes

namespace HotelReservationsManager.Controllers
{
    public class RoomsController : Controller
    {
<<<<<<< Updated upstream
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
=======
        private readonly HotelDbContext _context;
        private readonly RoomCRUDRepository _repo;
        RoomIndexViewModel _roomIndexViewModels = new RoomIndexViewModel();

        public RoomsController(HotelDbContext context)
        {
            _context = context;
            _repo = new RoomCRUDRepository(_context);
            _roomIndexViewModels.Items = _repo.GetAll().Select(x => new RoomViewModel()
            {
                Id=x.Id,
                BedPriceForAdult=x.BedPriceForAdult,
                BedPriceForKid=x.BedPriceForKid,
                Capacity=x.Capacity,
                IsFree=x.IsFree,
                Number=x.Number,
                Type=x.Type
            });
        }

        // GET: Rooms
        public IActionResult Index()
        {
            return View(_roomIndexViewModels);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RoomViewModel = await _roomIndexViewModels.Items
                .FirstOrDefaultAsync(vm => vm.Id == id);
            if (RoomViewModel == null)
            {
                return NotFound();
            }

            return View(RoomViewModel);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(RoomViewModel roomVM)
        {
            if (!ModelState.IsValid)
            {
                return View(roomVM);
            }
            Room room = new Room()
            {
                
                Id = roomVM.Id,
                BedPriceForAdult = roomVM.BedPriceForAdult,
                BedPriceForKid = roomVM.BedPriceForKid,
                Capacity = roomVM.Capacity,
                IsFree = true,
                Number = roomVM.Number,
                Type = roomVM.Type,
                Reservation=_context.Reservations.FirstOrDefault(x=>x.RoomId==roomVM.Id)
            };
            _repo.Add(room);
            return  RedirectToAction("Index", "Rooms");

        }

        // GET: Rooms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RoomViewModel roomVM = _roomIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            
            if (roomVM == null)
            {
                return NotFound();
            }
            return View(roomVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(RoomViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Room room = _repo.GetById(vm.Id);
            room.Id = vm.Id;
            room.BedPriceForAdult = vm.BedPriceForAdult;
            room.BedPriceForKid = vm.BedPriceForKid;
            room.Capacity = vm.Capacity;
            room.IsFree = vm.IsFree;
            room.Number = vm.Number;
            room.Type = vm.Type;
           //TODO: ADD THIS room.Reservation=vm.Reservation
            


            _repo.Update(room);
            return RedirectToAction("Index", "Rooms");
        }
    

        // GET: Rooms/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomVM = _roomIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            if (roomVM == null)
>>>>>>> Stashed changes
            {
                return NotFound();
            }

<<<<<<< Updated upstream
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

=======
            return View(roomVM);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var room = _repo.GetById(id);
            _repo.Remove(room);
            return RedirectToAction(nameof(Index));
>>>>>>> Stashed changes
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
<<<<<<< Updated upstream

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

=======
>>>>>>> Stashed changes
    }
}
