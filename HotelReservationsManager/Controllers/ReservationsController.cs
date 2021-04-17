using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Entities;
using HotelReservationsManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using HotelReservationsManager.Models.Reservation;
using HotelReservationsManager.Models.Room;
using HotelReservationsManager.Models.Client;
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Validation;

namespace HotelReservationsManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly int PageSize = GlobalVar.AmountOfElementsDisplayedPerPage;

        private readonly HotelDbContext _context;
        private readonly ReservationCRUDRepository _reservationRepo;
        private readonly RoomCRUDRepository _roomRepo;
        RoomIndexViewModel _roomIndexViewModels = new RoomIndexViewModel();
        ReservationIndexViewModel _reservationIndexViewModels = new ReservationIndexViewModel();
        public IActionResult ChangePageSize(int id)
        {
            if (id > 0)
            {
                GlobalVar.AmountOfElementsDisplayedPerPage = id;
            }

            return RedirectToAction("Index");
        }
        public ReservationsController(HotelDbContext context)
        {
            _context = context;
            _reservationRepo = new ReservationCRUDRepository(_context);
            _roomRepo = new RoomCRUDRepository(_context);
            _roomIndexViewModels.Items = _roomRepo.GetAll().Select(x => new RoomViewModel()
            {
                Id = x.Id,
                BedPriceForAdult = x.BedPriceForAdult,
                BedPriceForKid = x.BedPriceForKid,
                Capacity = x.Capacity,
                IsFree = x.IsFree,
                Number = x.Number,
                Type = x.Type,
                ReservationId = x.ReservationId



            });
            _reservationIndexViewModels.Items = _reservationRepo.GetAll().Select(x => new ReservationViewModel()
            {

                Id = x.Id,
                AccommodationDate = x.AccommodationDate,
                LeaveDate = x.LeaveDate,
                AllInclusive = x.AllInclusive,
                BreakfastIncluded = x.AllInclusive,
                Cost = x.Cost,
                UserId = x.UserId,
                RoomViewModel = _roomIndexViewModels.Items.FirstOrDefault(r => r.Id == x.RoomId),
                ClientsViewModels = (IQueryable<ClientViewModel>)x.Clients.Select(c => new ClientViewModel()
                {
                    Id = c.Id,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    IsAdult = c.IsAdult,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    ReservationId = c.ReservationId


                })
            });
        }
        public IActionResult Index()
        {
            /* List<Reservation> reservations = _context.Reservations.ToList();
             List<ReservationViewModel> list = new List<ReservationViewModel>();

             foreach (var reservation in reservations)
             {
                 int userId = reservation.UserId;
                 int roomId = reservation.RoomId;

                 ClientViewModel clientVM = new ClientViewModel()
                 {
                     Id = reservation.Client.Id,
                     FirstName = reservation.Client.FirstName,
                     LastName = reservation.Client.LastName,
                 };

                 RoomViewModel roomVM = new RoomViewModel()
                 {
                     Id = reservation.Room.Id,
                     Capacity = reservation.Room.Capacity,
                     BedPriceForAdult = reservation.Room.BedPriceForAdult,
                     BedPriceForKid = reservation.Room.BedPriceForKid,
                     Number = reservation.Room.Number,
                     Type = (RoomTypeEnum)reservation.Room.Type
                 };

                 int clientsCount = _context.ClientReservation.Where(x => x.ReservationId == reservation.Id).Count();

                 list.Add(new ReservationViewModel()
                 {
                     Id = reservation.Id,
                     Client = clientVM,
                     Room = roomVM,
                     CurrentReservationClientCount = clientsCount,
                     AccomodationName = reservation.AccommodationDate,
                     LeaveDate = reservation.LeaveDate,
                     AllInclusive = reservation.AllInclusive,
                     BreakfastIncluded = reservation.BreakfastIncluded,
                     Cost = reservation.Cost,
                 });

             }
            */
            return View("Index", _reservationIndexViewModels);
        }
        // GET: Reservations/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservationViewModel reservationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(reservationVM);
            }
            try
            {
                Validate(new Validation_Reservation()
                {
                    DateOfAccommodation = reservationVM.AccommodationDate,
                    DateOfExemption = reservationVM.LeaveDate,
                    RoomId = reservationVM.RoomId,
                    ReservationId = -1
                });
            }
            catch (InvalidOperationException e)
            {
                //reservationVM.Message = e.Message;
                return View(reservationVM);
            }
            Reservation reservation = new Reservation
            {
                Id = reservationVM.Id,
                AllInclusive = reservationVM.AllInclusive,
                BreakfastIncluded = reservationVM.BreakfastIncluded,
                LeaveDate = reservationVM.LeaveDate,
                AccommodationDate = reservationVM.AccommodationDate,
                Cost = reservationVM.Cost,
                RoomId = reservationVM.RoomViewModel.Id,
                Room = _roomRepo.GetById(reservationVM.RoomViewModel.Id),
                Clients = (ICollection<Client>)reservationVM.ClientsViewModels.Select(c => new Client()
                {
                    Id = c.Id,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    IsAdult = c.IsAdult,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    ReservationId = reservationVM.Id,
                    Reservation = _reservationRepo.GetById(reservationVM.Id)


                }) // ima shans da izgurmi s nesaotvetstvie na Data structure ; ako da promenqme vsichko na ICOllection i hashset
            };

            _reservationRepo.Add(reservation);
            return RedirectToAction(nameof(Index));
        }

    
        public IActionResult Edit(int? id)
        {

            if (id == null || id <=0)
            {
                return NotFound();
            }
            ReservationViewModel reservationVM = _reservationIndexViewModels.Items.FirstOrDefault(x => x.Id == id);

            if (reservationVM == null)
            {
                return NotFound();
            }
            return View(reservationVM);
        }

        // POST: Clients/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                Validate(new Validation_Reservation()
                {
                    DateOfAccommodation = vm.AccommodationDate,
                    DateOfExemption = vm.LeaveDate,
                    RoomId = _context.Reservations.Find(vm.Id).RoomId,
                    ReservationId = vm.Id
                });
            }
            catch (InvalidOperationException e)
            {
                //vm.Message = e.Message;
                return View(vm);
            }
            Reservation reservation = _reservationRepo.GetById(vm.Id);
            reservation.Id = vm.Id;
            reservation.AllInclusive = vm.AllInclusive;
            reservation.BreakfastIncluded = vm.BreakfastIncluded;
            reservation.LeaveDate = vm.LeaveDate;
            reservation.AccommodationDate = vm.AccommodationDate;
            reservation.Cost = vm.Cost;
            reservation.RoomId = vm.RoomViewModel.Id;
            reservation.Room = _roomRepo.GetById(vm.RoomViewModel.Id);
            reservation.Clients = (ICollection<Client>)vm.ClientsViewModels.Select(c => new Client()
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                IsAdult = c.IsAdult,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                ReservationId = vm.Id,
                Reservation = _reservationRepo.GetById(vm.Id)


            });


            _reservationRepo.Update(reservation);
            return RedirectToAction("Index", "Reservations");
        }
        public IActionResult Delete(int? id)
        {

            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var reservationVM = _reservationIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            if (reservationVM == null)
            {
                return NotFound();
            }

            return View("Delete", reservationVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reservation = _reservationRepo.GetById(id);
            _reservationRepo.Remove(reservation);
            return RedirectToAction(nameof(Index));
        }
        private void Validate(Validation_Reservation model)
        {

            if (CalculateDaysPassed(model.DateOfAccommodation, model.DateOfExemption) <= 0)
            {
                throw new InvalidOperationException("Date of accommodation must be before Date of exemption");
            }

            if (model.DateOfAccommodation.AddHours(GlobalVar.DefaultReservationHourStart) <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Date of accommodation must be after current date");
            }

            foreach (var item in _context.Reservations.Where(x => x.RoomId == model.RoomId && x.Id != model.ReservationId))
            {
                if ((item.AccommodationDate >= model.DateOfAccommodation && item.AccommodationDate < model.DateOfExemption)
                    ||
                    (item.LeaveDate > model.DateOfAccommodation && item.LeaveDate <= model.DateOfExemption))
                {
                    throw new InvalidOperationException($"Room is already reserved for the chosen period. Either choose a period before {item.AccommodationDate}, or after {item.LeaveDate}");
                }
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
        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

    }
}