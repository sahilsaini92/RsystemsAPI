using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Business.Services;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;

namespace RsystemsAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet("Index")]
        public async Task<ClientApiResponse> Index(int pageIndex, int pageSize)
        {
            return await _clientService.GetAllAsync(pageIndex, pageSize);
        }

        [HttpGet("Details/id")]
        public async Task<ClientDTO> Details(int id)
        {
            return await _clientService.GetByIdAsync(id);
        }

        [HttpPost("Create")]
        public async Task<ClientDTO> Create(ClientDTO dto)
        {
            try
            {
                return await _clientService.AddClientAsync(dto);
            }
            catch
            {
                return null;
            }
        }

        [HttpPut("Update")]
        public async Task<ClientDTO> Edit(ClientDTO client)
        {
            try
            {
                return await _clientService.UpdateClientAsync(client);
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
                return await _clientService.DeleteClientAsync(id);
            }
            catch
            {
                return false;
            }
        }
    }
}
