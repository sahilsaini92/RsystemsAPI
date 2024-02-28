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
    public class AccountServiceTest
    {
        private AccountController _accountController;
        private Mock<ApplicationContext> _mockDbContext;
        private Mock<IMapper> _mockMapper;
        private AccountService _accountService;
        private Mock<IAccountService> repository;

        [SetUp]
        public void SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            _mockDbContext = new Mock<ApplicationContext>(dbContextOptions);
            _mockMapper = new Mock<IMapper>();
            repository = new Mock<IAccountService>();
            repository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(MockData.GetAccount());
            repository.Setup(repo => repo.GetAllAsync(0,25)).ReturnsAsync(MockData.GetAccounts());
            repository.Setup(db => db.AddAccountAsync(MockData.GetAccount())).ReturnsAsync(MockData.GetAccount());
            _mockDbContext.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1); // Return a completed task with a result of 1

            _accountController = new AccountController(repository.Object);
            _accountService = new AccountService(_mockDbContext.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfAccountDTO()
        {
            var result = await _accountController.Index(0,25);
            //var result = await _accountService.GetAllAsync();

            // Assert
            Assert.That(result.TotalCount, Is.EqualTo(3));
            Assert.That(result.Accounts.First().AccountID, Is.EqualTo(1));
        }

    }
}
