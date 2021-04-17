﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Entities;
using HotelReservationsManager.Models.Shared;
using HotelReservationsManager.Models.Users;
using HotelReservationsManager.Models.Clients;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLibrary.Enumeration;
using System.Diagnostics;
using HotelReservationsManager.Models.Validation;
using HotelReservationsManager.Models.Reservation;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.Room;
using HotelReservationsManager.Models.Reservations;
using HotelReservationsManager.Models.Client;
<<<<<<< Updated upstream
=======
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Validation;

>>>>>>> Stashed changes

namespace HotelReservationsManager.Controllers
{
    public class ReservationsController : Controller
    {
<<<<<<< Updated upstream

        private readonly int PageSize = GlobalVar.AmountOfElementsDisplayedPerPage;

        private readonly HotelDbContext _context;

        public ReservationsController()
=======
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
>>>>>>> Stashed changes
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

        // GET: Reservations
        public IActionResult Index(ReservationsIndexViewModel model)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            List<Reservation> reservations = _context.Reservations.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            List<ReservationsViewModel> list = new List<ReservationsViewModel>();

            foreach (var reservation in reservations)
            {
                int userId = reservation.UserId;
                int roomId = reservation.RoomId;

                UsersViewModel userVM = new UsersViewModel()
                {
<<<<<<< Updated upstream
                    Id = reservation.User.Id,
                    FirstName = reservation.User.FirstName,
                    LastName = reservation.User.LastName,
                };

                RoomsViewModel roomVM = new RoomsViewModel()
                {
                    Id = reservation.Room.Id,
                    Capacity = reservation.Room.Capacity,
                    BedPriceForAdult = reservation.Room.BedPriceForAdult,
                    BedPriceForKid = reservation.Room.BedPriceForKid,
                    Number = reservation.Room.Number,
                    Type = (RoomTypeEnum)reservation.Room.Type
                };

                int clientsCount = _context.ClientReservation.Where(x => x.ReservationId == reservation.Id).Count();

                list.Add(new ReservationsViewModel()
                {
                    Id = reservation.Id,
                    User = userVM,
                    Room = roomVM,
                    CurrentReservationClientCount = clientsCount,
                    AccommodationDate = reservation.AccommodationDate,
                    LeaveDate = reservation.LeaveDate,
                    AllInclusive = reservation.AllInclusive,
                    BreakfastIncluded = reservation.BreakfastIncluded,
                    Cost = reservation.Cost,
                });

            }

            model.Items = list;
            model.Pager.PagesCount = Math.Max(1, (int)Math.Ceiling(_context.Reservations.Count() / (double)PageSize));

            return View(model);
        }
=======
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
>>>>>>> Stashed changes

        public IActionResult Create()
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            ReservationsCreateViewModel model = new ReservationsCreateViewModel();

            model = CreateReservationVMWithDropdown(model, null);

            return View(model);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReservationsCreateViewModel createModel)
        {
            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }
<<<<<<< Updated upstream
=======
            try
            {
                Validate(new Validation_Reservation()
                {
                    AccomodationDate = reservationVM.AccommodationDate,
                    LeaveDate = reservationVM.LeaveDate,
                    RoomId = _context.Reservations.Find(reservationVM.Id).RoomId,
                    ReservationId = reservationVM.Id
                });
            }
            catch (InvalidOperationException e)
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
>>>>>>> Stashed changes

            if (ModelState.IsValid)
            {

                try
                {
                    Validate(new Validation_Reservation()
                    {
                        DateOfAccommodation = createModel.AccomodationDate,
                        DateOfExemption = createModel.LeaveDate,
                        RoomId = createModel.RoomId,
                        ReservationId = -1
                    });
                }
                catch (InvalidOperationException e)
                {
                    createModel = CreateReservationVMWithDropdown(createModel, e.Message);
                    return View(createModel);
                }

                Reservation reservation = new Reservation
                {
                    UserId = createModel.UserId,
                    RoomId = createModel.RoomId,
                    AccommodationDate = createModel.AccomodationDate,
                    LeaveDate = createModel.LeaveDate,
                    AllInclusive = createModel.IsAllInclusive,
                    BreakfastIncluded = createModel.IsBreakfastIncluded,
                    Cost = 0
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            createModel = CreateReservationVMWithDropdown(createModel, null);
            return View(createModel);
        }

        public IActionResult Edit(int? id)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (id == null || !ReservationExists((int)id))
            {
                return NotFound();
            }

            Reservation reservation = _context.Reservations.Find(id);

            ReservationsEditViewModel model = new ReservationsEditViewModel()
            {
                Id = reservation.Id,
                AccomodationDate = reservation.AccommodationDate,
                LeaveDate = reservation.LeaveDate,
                IsAllInclusive = reservation.AllInclusive,
                IsBreakfastIncluded = reservation.BreakfastIncluded,
                OverallBill = reservation.Cost
            };

            return View(model);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationsEditViewModel editModel)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (!ReservationExists(editModel.Id))
            {
                return NotFound();
            }
<<<<<<< Updated upstream



            if (ModelState.IsValid)
=======
            try
            {
                Validate(new Validation_Reservation()
                {
                    AccomodationDate = vm.AccommodationDate,
                    LeaveDate = vm.LeaveDate,
                    RoomId = _context.Reservations.Find(vm.Id).RoomId,
                    ReservationId = vm.Id
                });
            }
            catch (InvalidOperationException e)
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
>>>>>>> Stashed changes
            {

                try
                {
                    Validate(new Validation_Reservation()
                    {
                        DateOfAccommodation = editModel.AccomodationDate,
                        DateOfExemption = editModel.LeaveDate,
                        RoomId = _context.Reservations.Find(editModel.Id).RoomId,
                        ReservationId = editModel.Id
                    });
                }
                catch (InvalidOperationException e)
                {
                    editModel.Message = e.Message;
                    return View(editModel);
                }


                Reservation reservation = _context.Reservations.Find(editModel.Id);

                reservation.AccommodationDate = editModel.AccomodationDate;
                reservation.LeaveDate = editModel.LeaveDate;
                reservation.AllInclusive = editModel.IsAllInclusive;
                reservation.BreakfastIncluded = editModel.IsBreakfastIncluded;

                _context.Reservations.Update(reservation);
                _context.SaveChanges();


                reservation.Cost = CalculateOverAllPrice(reservation.Id);
                _context.Reservations.Update(reservation);
                _context.SaveChanges();


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

            if (id == null || !ReservationExists((int)id))
            {
                return NotFound();
            }

            Reservation reservation = _context.Reservations.Find(id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Detail(int? id)
        {
            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (id == null || !ReservationExists((int)id))
            {
                return NotFound();
            }

            Reservation reservation = _context.Reservations.Find(id);

            UsersViewModel userVM = new UsersViewModel()
            {
                Id = reservation.User.Id,
                FirstName = reservation.User.FirstName,
                SecondName = reservation.User.SecondName,
                LastName = reservation.User.LastName,
                Username = reservation.User.Username
            };

            RoomsViewModel roomVM = new RoomsViewModel()
            {
                Id = reservation.Room.Id,
                Capacity = reservation.Room.Capacity,
                BedPriceForAdult = reservation.Room.BedPriceForAdult,
                BedPriceForKid = reservation.Room.BedPriceForKid,
                Number = reservation.Room.Number,
                Type = (RoomTypeEnum)reservation.Room.Type
            };

            var allClients = _context.Clients.ToList();

            var allClientReservations = _context.ClientReservation.Where(x => x.ReservationId == id).ToList();

            var reservedClients = new List<Client>();

            var availableClients = allClients;

            foreach (var clientReservation in allClientReservations)
            {
                availableClients.RemoveAll(x => x.Id == clientReservation.ClientId);
                var client = (_context.Clients.Find(clientReservation.ClientId));
                reservedClients.Add(client);
            }

            var availableClientsVM = availableClients.Select(x => new SelectListItem()
            {
                Text = x.FirstName + " " + x.LastName + " (" + x.Email + ")",
                Value = x.Id.ToString()
            }).ToList();

            var reservedClientsVM = reservedClients.Select(x => new ClientsViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email

            }).ToList();

            var model = new ReservationsViewModel()
            {
                User = userVM,
                Room = roomVM,
                CurrentReservationClientCount = reservedClients.Count(),
                AccommodationDate = reservation.AccommodationDate,
                LeaveDate = reservation.LeaveDate,
                AllInclusive = reservation.AllInclusive,
                BreakfastIncluded = reservation.BreakfastIncluded,
                Cost = reservation.Cost,
                AvailableClients = availableClientsVM,
                SignedInClients = reservedClientsVM
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkClientReservation(ReservationsViewModel linkModel)
        {
            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            var clientId = linkModel.ClientId;
            var reservationId = linkModel.Id;

            if (reservationId <= 0)
            {
                return RedirectToAction("Index");
            }

            if (clientId <= 0)
            {
                return RedirectToAction("Detail", new { id = reservationId });
            }

            var clientReservation = new ClientReservation()
            {
                ClientId = clientId,
                ReservationId = reservationId
            };

            var currentRoomOccupyCount = (_context.ClientReservation.Where(x => x.ReservationId == reservationId).ToList()).Count;

            Room room = _context.Rooms.Find(_context.Reservations.Find(reservationId).RoomId);


            if (currentRoomOccupyCount >= room.Capacity)
            {
                return RedirectToAction("Detail", new { id = reservationId });
            }

            var elem = _context.ClientReservation.Find(clientId, reservationId);

            if (elem != null)
            {
                throw new InvalidOperationException($"CUSTOM EXCEPTION: This client {clientId} is already added to this reservation {reservationId}");
            }
            else
            {
                _context.ClientReservation.Add(clientReservation);
                _context.SaveChanges();

                bool isClientAdult = (_context.Clients.Find(clientId)).IsAdult;
                decimal pricePerDay = 0;
                if (isClientAdult)
                {
                    pricePerDay += (_context.Rooms.Find(room.Id)).BedPriceForAdult;
                }
                else
                {
                    pricePerDay += (_context.Rooms.Find(room.Id)).BedPriceForKid;
                }

                Reservation reservation = _context.Reservations.Find(reservationId);
                decimal clientOverall = pricePerDay * CalculateDaysPassed(reservation.AccommodationDate, reservation.LeaveDate);
                clientOverall = AddExtras(clientOverall, reservation.AllInclusive, reservation.BreakfastIncluded);
                reservation.Cost += clientOverall;

                _context.Reservations.Update(reservation);
                _context.SaveChanges();
            }
            return RedirectToAction("Detail", new { id = reservationId });
        }
        private void Validate(Validation_Reservation model)
        {

            if (CalculateDaysPassed(model.AccomodationDate, model.LeaveDate) <= 0)
            {
                throw new InvalidOperationException("Date of accommodation must be before Date of exemption");
            }

            /*if (model.AccomodationDate.AddHours(GlobalVar.DefaultReservationHourStart) <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Date of accommodation must be after current date");
            }*/

            foreach (var item in _context.Reservations.Where(x => x.RoomId == model.RoomId && x.Id != model.ReservationId))
            {
                if ((item.AccommodationDate >= model.AccomodationDate && item.AccommodationDate < model.LeaveDate)
                    ||
                    (item.LeaveDate > model.AccomodationDate && item.LeaveDate <= model.LeaveDate))
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
                    throw new InvalidOperationException($"Room is already reserved for the chosen period. Either choose a period before {item.AccommodationDate}, or after {item.AccommodationDate}");
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

        private decimal CalculateOneDayPriceWithoutExtrasForRoom(int reservationId)
        {

            Reservation reservation = _context.Reservations.Find(reservationId);


            decimal underagePrice = _context.Rooms.Find(reservation.RoomId).BedPriceForKid;
            decimal adultPrice = _context.Rooms.Find(reservation.RoomId).BedPriceForKid;

            var clientList = _context.ClientReservation.Where(x => x.ReservationId == reservationId).ToList();


            decimal pricePerDay = 0;
            foreach (var id in clientList)
            {
                bool isClientAdult = _context.Clients.Find(id.ClientId).IsAdult;
                if (isClientAdult)
                {
                    pricePerDay += adultPrice;
                }
                else
                {
                    pricePerDay += underagePrice;
                }
            }
            return pricePerDay;
        }

        private decimal CalculateOverallBillWithoutExtrasForRoom(int reservationId, int days)
        {
            return CalculateOneDayPriceWithoutExtrasForRoom(reservationId) * days;
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

        private decimal CalculateOverAllPrice(int reservationId)
        {
            Reservation reservation = _context.Reservations.Find(reservationId);
            int days = CalculateDaysPassed(reservation.AccommodationDate, reservation.LeaveDate);
            var overallBillWithoutextras = CalculateOverallBillWithoutExtrasForRoom(reservationId, days);
            return AddExtras(overallBillWithoutextras, reservation.AllInclusive, reservation.BreakfastIncluded);
        }



        private ReservationsCreateViewModel CreateReservationVMWithDropdown(ReservationsCreateViewModel model, string message)
        {
            model.Message = message;

            model.Rooms = _context.Rooms.Select(x => new SelectListItem()
            {
                Text = $"{x.Number.ToString()} [0/{x.Capacity}] (type: {((RoomTypeEnum)x.Type).ToString()})",
                Value = x.Id.ToString()
            }).ToList();

            model.Users = _context.Users.Where(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.FirstName + " " + x.LastName + " (" + x.Email + ")",
                Value = x.Id.ToString()
            }).ToList();

            return model;
        }

    }
}
