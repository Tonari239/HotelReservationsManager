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

namespace HotelReservationsManager.Controllers
{
    public class ClientsController : Controller
    {
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
            {
                return NotFound();
            }

            Client client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
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



    }
}