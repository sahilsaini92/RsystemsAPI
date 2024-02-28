using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RsystemsAssignment.API.Controllers;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Business.Services;
using RsystemsAssignment.Data;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Tests
{
    public class AppointmentServiceTest
    {
        private AppointmentController _appointmentController;
        private Mock<ApplicationContext> _mockDbContext;
        private Mock<IMapper> _mockMapper;
        private AppointmentService _appointmentService;
        private Mock<IAppointmentService> repository;

        [SetUp]
        public void SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            _mockDbContext = new Mock<ApplicationContext>(dbContextOptions);
            _mockMapper = new Mock<IMapper>();
            repository = new Mock<IAppointmentService>();
            repository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(MockData.GetAppointment());
            repository.Setup(repo => repo.GetAllAsync(0,25)).ReturnsAsync(MockData.GetAppointments());
            repository.Setup(db => db.AddAppointmentAsync(MockData.GetAppointment())).ReturnsAsync(MockData.GetAppointment());
            _mockDbContext.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1); // Return a completed task with a result of 1

            _appointmentController = new AppointmentController(repository.Object);
            _appointmentService = new AppointmentService(_mockDbContext.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfAccountDTO()
        {
            var result = await _appointmentController.Index(0,25);

            Assert.That(result.TotalCount, Is.EqualTo(3));
            Assert.That(result.Appointments.First().AppointmentID, Is.EqualTo(1));
        }

    }
}
