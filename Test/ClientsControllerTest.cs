using DataLibrary;
using DataLibrary.Entities;
using DataLibrary.Repositories;
using HotelReservationsManager.Controllers;
using HotelReservationsManager.Models.Room;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{
    public class ClientsControllerTest
    {
        private HotelDbContext _context;
        private ClientsController _controller;

        [SetUp]
        public void Setup()
        {

            _context = new HotelDbContext();
            _controller = new ClientsController(_context);

        }



        [Test]
        public void Index_ReturnsView()
        {
            ViewResult result = _controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Create_ReturnsView()
        {
            ViewResult result = _controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }


        [Test]
        public void Edit_RedirectsWhenInputDataIsIncorrect()
        {
            var result = _controller.Edit(-1) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }


        [Test]
        public void Delete_RedirectsWhenInputDataIsIncorrect()
        {
            var result = _controller.Delete(-1) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }



        [Test]
        public void Details_RedirectsWhenIdDataIsIncorrect()
        {
            var result = _controller.Details(-1) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void Details_ReturnsCorrectView()
        {
            ViewResult result = _controller.Details(1) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }















    }
}