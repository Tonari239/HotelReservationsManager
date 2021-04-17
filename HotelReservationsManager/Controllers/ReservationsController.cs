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
using HotelReservationsManager.Models.Shared;

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
            _reservationIndexViewModels.Pager ??= new PagerViewModel();
            _reservationIndexViewModels.Pager.CurrentPage = _reservationIndexViewModels.Pager.CurrentPage <= 0 ? 1 : _reservationIndexViewModels.Pager.CurrentPage;
            _reservationIndexViewModels.Pager.PagesCount = Math.Max(1, (int)Math.Ceiling(_context.Reservations.Count() / (double)PageSize));
            return View("Index", _reservationIndexViewModels);
        }
        // GET: Reservations/Create

        public IActionResult Create()
        {
            return View("Create");
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


                }) 
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
            return View("Edit",reservationVM);
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

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

    }
}