using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Client;
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
            {
                return NotFound();
            }

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

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
