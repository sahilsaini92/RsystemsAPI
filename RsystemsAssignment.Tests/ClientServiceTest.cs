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
    public class ClientServiceTest
    {
        private ClientController _clientController;
        private Mock<ApplicationContext> _mockDbContext;
        private Mock<IMapper> _mockMapper;
        private ClientService _clientService;
        private Mock<IClientService> repository;

        [SetUp]
        public void SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            _mockDbContext = new Mock<ApplicationContext>(dbContextOptions);
            _mockMapper = new Mock<IMapper>();
            repository = new Mock<IClientService>();
            repository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(MockData.GetClient());
            repository.Setup(repo => repo.GetAllAsync(0,25,1,"")).ReturnsAsync(MockData.GetClients());
            repository.Setup(db => db.AddClientAsync(MockData.GetClient())).ReturnsAsync(MockData.GetClient());
            repository.Setup(x => x.DeleteClientAsync(1, 1)).ReturnsAsync(true);

            _mockDbContext.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1); // Return a completed task with a result of 1

            _clientController = new ClientController(repository.Object);
            _clientService = new ClientService(_mockDbContext.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfClientDTO()
        {
            var result = await _clientController.Index(0,25,1, "");
            Assert.That(result.TotalCount, Is.EqualTo(3));
            Assert.That(result.Clients.First().AccountID, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAsync_ShouldDelete()
        {
            var result = await _clientController.Delete(1, 1);
            Assert.That(result, Is.EqualTo(true));
        }

    }
}
