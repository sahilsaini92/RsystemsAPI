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

            var dto = new AccountDTO {
                AccountID = 1,
                AccountName = "Google",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.Now.AddDays(1)
            };


            _mockDbContext = new Mock<ApplicationContext>(dbContextOptions);
            _mockMapper = new Mock<IMapper>();
            repository = new Mock<IAccountService>();
            repository.Setup(x => x.AddAccountAsync(dto)).ReturnsAsync(MockData.AddAccount());
            repository.Setup(repo => repo.GetAllAsync(0,25)).ReturnsAsync(MockData.GetAccounts());

            _accountController = new AccountController(repository.Object);
            _accountService = new AccountService(_mockDbContext.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfAccountDTO()
        {
            var result = await _accountController.Index(0,25);
            Assert.That(result.TotalCount, Is.EqualTo(3));
            Assert.That(result.Accounts.First().AccountID, Is.EqualTo(1));
        }



    }
}
