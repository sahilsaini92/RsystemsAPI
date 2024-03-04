using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;

namespace RsystemsAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("Index")]
        public async Task<AccountApiResponse> Index(int pageNumber, int pageSize,string searchValue)
        {
            return await _accountService.GetAllAsync(pageNumber,pageSize, searchValue);
        }

        [HttpGet("Details/id")]
        public async Task<AccountDTO> Details(int id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        [HttpPost("Create")]
        public async Task<AccountDTO> Create(AccountDTO dto)
        {
            try
            {
                return await _accountService.AddAccountAsync(dto);
            }
            catch
            {
                return null;
            }
        }


        [HttpPut("Update")]
        public async Task<AccountDTO> Edit(AccountDTO account)
        {
            try
            {
                return await _accountService.UpdateAccountAsync(account);
            }
            catch
            {
                return null;
            }
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _accountService.DeleteAccountAsync(id);
            }
            catch
            {
                return false;
            }
        }
    }
}
