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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("Index")]
        public async Task<AppointmentApiResponse> Index(int pageIndex, int pageSize)
        {
            return await _appointmentService.GetAllAsync(pageIndex, pageSize);
        }

        [HttpGet("Details/id")]
        public async Task<AppointmentDTO> Details(int id)
        {
            return await _appointmentService.GetByIdAsync(id);
        }

        [HttpPost("Create")]
        public async Task<AppointmentDTO> Create(AppointmentDTO dto)
        {
            try
            {
                return await _appointmentService.AddAppointmentAsync(dto);
            }
            catch
            {
                return null;
            }
        }

        [HttpPut("Update")]
        public async Task<AppointmentDTO> Edit(AppointmentDTO appointment)
        {
            try
            {
                return await _appointmentService.UpdateAppointmentAsync(appointment);
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
                return await _appointmentService.DeleteAppointmentAsync(id);
            }
            catch
            {
                return false;
            }
        }
    }
}
