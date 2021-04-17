using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Controllers;
using HotelReservationsManager.Models.Room;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class Tests
    {
        private RoomsController _controller;
        private HotelDbContext _context;
        [SetUp]
        public void Setup()
        {
             _context = new HotelDbContext();
             _controller = new RoomsController(_context);
            
        }

        [Test]
        public void IndexReturnsView()
        {
            ViewResult result = _controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
        
        [Test]
        public void CreateReturnsView()
        {
            ViewResult result = _controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void EditRedirectsWhenInputDataIsIncorrect()
        {
            var result = _controller.Edit(-1) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void DeleteRedirectsWhenInputDataIsIncorrect()
        {
            var result = _controller.Delete(-1) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        /*[Test]
        public void DatabaseInstantiation()
        {
            var options = DbMockInstantiation();

            // Use a clean instance of the context to run the test
            using (var context = new HotelDbContext(options))
            {
                RoomCRUDRepository roomRepository = new RoomCRUDRepository(context);
                List<Room> rooms = roomRepository.GetAll().ToList();
    
            Assert.AreEqual(3, rooms.Count);
            }
        }

        private DbContextOptions<HotelDbContext> DbMockInstantiation()
        {
            var options = new DbContextOptionsBuilder<HotelDbContext>()
              .UseInMemoryDatabase(databaseName: "HotelDb");
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=HotelDB; Integrated Security=True");

            using (var context = new HotelDbContext(options.Options))
            {
                context.Rooms.Add(new Room { Id = 1, Number = 1, IsFree = true, Type = RoomTypeEnum.Apartment, Capacity = 3, BedPriceForAdult = 27.5m, BedPriceForKid = 20.5m });
                context.Rooms.Add(new Room { Id = 2, Number = 3, IsFree = false, Type = RoomTypeEnum.SingleBed, Capacity = 1, BedPriceForAdult = 17.5m, BedPriceForKid = 12.5m });
                context.Rooms.Add(new Room { Id = 3, Number = 15, IsFree = false, Type = RoomTypeEnum.DoubleBed, Capacity = 2, BedPriceForAdult = 20.5m, BedPriceForKid = 16.5m });
                context.SaveChanges();
            }
            return options.Options;

           

        }
        */





    }
}