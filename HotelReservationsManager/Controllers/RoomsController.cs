using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Models.Room;

namespace HotelReservationsManager.Controllers
{
    public class RoomsController : Controller
    {
        private readonly int PageSize = GlobalVar.AmountOfElementsDisplayedPerPage;
        private readonly HotelDbContext _context;
        private readonly RoomCRUDRepository _repo;
        private readonly ReservationCRUDRepository _reservationRepo;
        RoomIndexViewModel _roomIndexViewModels = new RoomIndexViewModel();

        public RoomsController(HotelDbContext context)
        {
            _context = context;
            _repo = new RoomCRUDRepository(_context);
            _reservationRepo = new ReservationCRUDRepository(_context);
            _roomIndexViewModels.Items = _repo.GetAll().Select(x => new RoomViewModel()
            {
                Id=x.Id,
                BedPriceForAdult=x.BedPriceForAdult,
                BedPriceForKid=x.BedPriceForKid,
                Capacity=x.Capacity,
                IsFree=x.IsFree,
                Number=x.Number,
                Type=x.Type,
                ReservationId=x.ReservationId,
                
            });
        }

        // GET: Rooms
        public IActionResult Index()
        {
            return View("Index",_roomIndexViewModels);
        }

        // GET: Rooms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || id<=0)
            {
                return NotFound();
            }

            var RoomViewModel =  _roomIndexViewModels.Items
                .FirstOrDefaultAsync(vm => vm.Id == id);
            if (RoomViewModel == null)
            {
                return NotFound();
            }

            return View("Details",RoomViewModel);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View("Create");
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
            if (id == null || id <=0)
            {
                return NotFound();
            }
            RoomViewModel roomVM = _roomIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            
            if (roomVM == null)
            {
                return NotFound();
            }
            return View("Edit",roomVM);
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
            room.Reservation = _context.Reservations.Find(vm.ReservationId);
            room.ReservationId = vm.ReservationId;
            




            _repo.Update(room);
            return RedirectToAction("Index", "Rooms");
        }
    

        // GET: Rooms/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null || id <=0)
            {
                return NotFound();
            }

            var roomVM = _roomIndexViewModels.Items.FirstOrDefault(x => x.Id == id);
            if (roomVM == null)
            {
                return NotFound();
            }

            return View("Delete",roomVM);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var room = _repo.GetById(id);
            _repo.Remove(room);
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }

        
    }
}
