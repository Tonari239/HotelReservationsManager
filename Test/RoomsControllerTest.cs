using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Controllers;
using HotelReservationsManager.Models.Room;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Test
{
    public class RoomsControllerTest
    {
        private RoomsController _controller;
        private HotelDbContext _context;
        [SetUp]
        public void Setup()
        {
            /*
            var options = new DbContextOptionsBuilder<HotelDbContext>()
           .UseInMemoryDatabase(databaseName: "HotelDB")
           .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new HotelDbContext(options))
            {
                context.Rooms.Add(new Room() { Id = 1, BedPriceForAdult = 15, BedPriceForKid = 13, Capacity = 2, IsFree = true, Type = RoomTypeEnum.DoubleBed, Number = 1, ReservationId = 0, Reservation = null });
                context.Rooms.Add(new Room() { Id = 2, BedPriceForAdult = 20, BedPriceForKid = 14, Capacity = 6, IsFree = false, Type = RoomTypeEnum.Maisonette, Number = 2, ReservationId = 0, Reservation = null });
                context.Rooms.Add(new Room() { Id = 3, BedPriceForAdult = 10, BedPriceForKid = 8, Capacity = 1, IsFree = true, Type = RoomTypeEnum.SingleBed, Number = 3, ReservationId = 0, Reservation = null });
                context.Rooms.Add(new Room() { Id = 4, BedPriceForAdult = 16, BedPriceForKid = 13, Capacity = 5, IsFree = true, Type = RoomTypeEnum.Penthouse, Number = 4, ReservationId = 0, Reservation = null });
                context.SaveChanges();
            }
            */

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

        





    }
}