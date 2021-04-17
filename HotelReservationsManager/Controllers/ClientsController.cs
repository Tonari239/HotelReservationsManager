using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Client;
using HotelReservationsManager.Models.Filters;
using HotelReservationsManager.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Controllers
{
    public class ClientsController : Controller
    {
        private readonly int PageSize = GlobalVar.AmountOfElementsDisplayedPerPage;
        private readonly HotelDbContext _context;
        private readonly ClientCRUDRepository _repo;
        private readonly ReservationCRUDRepository _reservationRepo;
        ClientIndexViewModel _clientIndexViewModels = new ClientIndexViewModel();

        public ClientsController(HotelDbContext context)
        {
            _context = context;
            _repo = new ClientCRUDRepository(_context);
            _reservationRepo = new ReservationCRUDRepository(_context);
            _clientIndexViewModels.Items = _repo.GetAll().Select(x => new ClientViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                Id = x.Id,
                IsAdult = x.IsAdult,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ReservationId = x.ReservationId
            }); ;
        }

        // GET: Clients
        public IActionResult Index()
        {
            _clientIndexViewModels.Pager ??= new PagerViewModel();
            _clientIndexViewModels.Pager.CurrentPage = _clientIndexViewModels.Pager.CurrentPage <= 0 ? 1 : _clientIndexViewModels.Pager.CurrentPage;

            var contextDb = Filter(_context.Clients.ToList(), _clientIndexViewModels.Filter);
            _clientIndexViewModels.Pager.PagesCount = Math.Max(1, (int)Math.Ceiling(contextDb.Count() / (double)PageSize));
            return View("Index", _clientIndexViewModels);
        }

        // GET: Clients/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var ClientViewModel = _clientIndexViewModels.Items
                .FirstOrDefaultAsync(vm => vm.Id == id);
            if (ClientViewModel == null)
            {
                return NotFound();
            }

            return View("Details", ClientViewModel);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View("Create");
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
                Reservation = _reservationRepo.GetById(clientVM.ReservationId),
                ReservationId = clientVM.ReservationId
            };
            _repo.Add(Client);
            return RedirectToAction("Index", "Clients");

        }

        // GET: Clients/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            ClientViewModel clientVM = _clientIndexViewModels.Items.FirstOrDefault(x => x.Id == id);

            if (clientVM == null)
            {
                return NotFound();
            }
            return View("Edit", clientVM);
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
            client.Reservation = _reservationRepo.GetById(vm.ReservationId);
            client.ReservationId = vm.ReservationId;


            //TODO: ADD THIS client.Reservation=vm.Reservation but with View model



            _repo.Update(client);
            return RedirectToAction("Index", "Rooms");
        }

        // GET: Clients/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var ClientVM = _clientIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            if (ClientVM == null)
            {
                return NotFound();
            }

            return View("Delete", ClientVM);
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
