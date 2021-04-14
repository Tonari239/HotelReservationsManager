<<<<<<< Updated upstream
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Entities;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.Client;
using HotelReservationsManager.Models.Shared;
using HotelReservationsManager.Models.Filters;
using HotelReservationsManager.Models.Clients;
using HotelReservationsManager.Models.Room;
using DataLibrary.Enumeration;
=======
﻿using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> Stashed changes

namespace HotelReservationsManager.Controllers
{
    public class ClientsController : Controller
    {
<<<<<<< Updated upstream
        private readonly int PageSize = GlobalVar.AmountOfElementsDisplayedPerPage;
        private readonly HotelDbContext _context;

        public ClientsController()
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

        public IActionResult Index(ClientsIndexViewModel model)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            var contextDb = Filter(_context.Clients.ToList(), GetFilter(model));

            List<ClientsViewModel> items = contextDb.Skip((model.Pager.CurrentPage - 1) * PageSize).Take(PageSize).Select(c => new ClientsViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IsAdult = c.IsAdult
            }).ToList();

            if (model.Filter == null)
            {
                model.Filter = new ClientsFilterViewModel();
            }
            model.Items = items;
            model.Pager.PagesCount = Math.Max(1, (int)Math.Ceiling(contextDb.Count() / (double)PageSize));

            return View(model);
        }

        private static ClientsFilterViewModel GetFilter(ClientsIndexViewModel model)
        {
            return model.Filter;
        }

        public IActionResult Create()
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            ClientsCreateViewModel model = new ClientsCreateViewModel();

            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientsCreateViewModel createModel)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (ModelState.IsValid)
            {
                Client client = new Client
                {
                    FirstName = createModel.FirstName,
                    LastName = createModel.LastName,
                    Email = createModel.Email,
                    PhoneNumber = createModel.PhoneNumber,
                    IsAdult = createModel.IsAdult
                };

                _context.Clients.Add(client);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (id == null || !ClientExists((int)id))
            {
                return NotFound();
            }

            Client client = _context.Clients.Find(id);

            ClientsEditViewModel model = new ClientsEditViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                IsAdult = client.IsAdult
            };

            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientsEditViewModel editModel)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (ModelState.IsValid)
            {

                if (!ClientExists(editModel.Id))
                {
                    return NotFound();
                }

                Client client = new Client()
                {
                    Id = editModel.Id,
                    FirstName = editModel.FirstName,
                    LastName = editModel.LastName,
                    Email = editModel.Email,
                    PhoneNumber = editModel.PhoneNumber,
                    IsAdult = editModel.IsAdult
                };

                _context.Update(client);
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
        }



        public IActionResult Detail(int? id)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }

            if (id == null || !ClientExists((int)id))
            {
                return NotFound();
            }

            Client client = _context.Clients.Find(id);
            List<Reservation> reservations = new List<Reservation>();
            List<ClientReservation> clientReservations = _context.ClientReservation.Where(x => x.ClientId == id).ToList();

            foreach (var cr in clientReservations)
            {

                reservations.Add(_context.Reservations.Find(cr.ReservationId));
            }

            ClientsDetailViewModel model = new ClientsDetailViewModel()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                IsAdult = client.IsAdult,
                PastReservations = reservations.Where(x => x.LeaveDate.AddHours(GlobalVar.DefaultReservationHourStart) < DateTime.UtcNow).Select(x => new ReservationsViewModel()
                {
                    Id = x.Id,
                    AccommodationDate = x.AccommodationDate.AddHours(GlobalVar.DefaultReservationHourStart),
                    LeaveDate = x.LeaveDate.AddHours(GlobalVar.DefaultReservationHourStart),
                    BreakfastIncluded = x.BreakfastIncluded,
                    AllInclusive = x.AllInclusive,
                    Cost = x.Cost,
                    Room = new RoomsViewModel()
                    {
                        Number = _context.Rooms.Find(x.RoomId).Number,
                        Capacity = _context.Rooms.Find(x.RoomId).Capacity,
                        Type = (RoomTypeEnum)_context.Rooms.Find(x.RoomId).Type
                    }
                }).ToList(),
                UpcomingReservations = reservations.Where(x => x.LeaveDate.AddHours(GlobalVar.DefaultReservationHourStart) >= DateTime.UtcNow).Select(x => new ReservationsViewModel()
                {
                    Id = x.Id,
                    AccommodationDate = x.AccommodationDate.AddHours(GlobalVar.DefaultReservationHourStart),
                    LeaveDate = x.LeaveDate.AddHours(GlobalVar.DefaultReservationHourStart),
                    BreakfastIncluded = x.BreakfastIncluded,
                    AllInclusive = x.AllInclusive,
                    Cost = x.Cost,
                    Room = new RoomsViewModel()
                    {
                        Number = _context.Rooms.Find(x.RoomId).Number,
                        Capacity = _context.Rooms.Find(x.RoomId).Capacity,
                        Type = (RoomTypeEnum)_context.Rooms.Find(x.RoomId).Type
                    }
                }).ToList(),
                TelephoneNumber = client.PhoneNumber

            };

            return View(model);
        }


        public IActionResult Delete(int? id)
        {

            if (GlobalVar.LoggedOnUserId == -1)
            {
                return RedirectToAction("LogInRequired", "Users");
            }


            if (id == null || !ClientExists((int)id))
=======
        private readonly HotelDbContext _context;
        private readonly ClientCRUDRepository _repo;
        ClientIndexViewModel _clientIndexViewModels = new ClientIndexViewModel();

        public ClientsController(HotelDbContext context)
        {
            _context = context;
            _repo = new ClientCRUDRepository(_context);
            _clientIndexViewModels.Items = _repo.GetAll().Select(x => new ClientViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                Id = x.Id,
                IsAdult = x.IsAdult,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Reservation = _context.Reservations.FirstOrDefault(r => r.Id == x.Reservation.Id)
            }); ;
        }

        // GET: Clients
        public IActionResult Index()
        {
            return View(_clientIndexViewModels);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ClientViewModel = await _clientIndexViewModels.Items
                .FirstOrDefaultAsync(vm => vm.Id == id);
            if (ClientViewModel == null)
            {
                return NotFound();
            }

            return View(ClientViewModel);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientViewModel clientVM)
        {
            if (!ModelState.IsValid)
            {
                return View(clientVM);
            }
            Client Client = new Client()
            {

                Id = clientVM.Id,
                Email = clientVM.Email,
                IsAdult = clientVM.IsAdult,
                FirstName = clientVM.FirstName,
                LastName = clientVM.FirstName,
                PhoneNumber = clientVM.PhoneNumber,
                Reservation = _context.Reservations.FirstOrDefault(r => r.Id == clientVM.Reservation.Id)
            };
            _repo.Add(Client);
            return RedirectToAction("Index", "Clients");

        }

        // GET: Clients/Edit/5
        public IActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return NotFound();
                }
            ClientViewModel clientVM = _clientIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            
                if (clientVM == null)
                {
                    return NotFound();
                }
                return View(clientVM);
            }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Client client = _repo.GetById(vm.Id);
            client.Id = vm.Id;
            client.Email = vm.Email;
            client.FirstName = vm.FirstName;
            client.IsAdult = vm.IsAdult;
            client.LastName = vm.LastName;
            client.PhoneNumber = vm.PhoneNumber;
            client.Reservation = vm.Reservation;
            
            //TODO: ADD THIS client.Reservation=vm.Reservation but with View model



            _repo.Update(client);
            return RedirectToAction("Index", "Rooms");
        }

        // GET: Clients/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ClientVM = _clientIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            if (ClientVM == null)
>>>>>>> Stashed changes
            {
                return NotFound();
            }

<<<<<<< Updated upstream
            Client client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
=======
            return View(ClientVM);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var Client = _repo.GetById(id);
            _repo.Remove(Client);
            return RedirectToAction(nameof(Index));
        }

>>>>>>> Stashed changes
        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
<<<<<<< Updated upstream
        private List<Client> Filter(List<Client> collection, ClientsFilterViewModel filterModel)
        {

            if (filterModel != null)
            {
                if (filterModel.FirstName != null)
                {
                    collection = collection.Where(x => x.FirstName.Contains(filterModel.FirstName)).ToList();
                }
                if (filterModel.LastName != null)
                {
                    collection = collection.Where(x => x.LastName.Contains(filterModel.LastName)).ToList();
                }
            }

            return collection;
        }



=======
>>>>>>> Stashed changes
    }
}
