using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Business.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentApiResponse> GetAllAsync(int pageNumber, int pageCount);
        Task<AppointmentDTO> GetByIdAsync(int id);
        Task<AppointmentDTO> AddAppointmentAsync(AppointmentDTO appointmentDTO);
        Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDTO);

        Task<bool> DeleteAppointmentAsync(int id);
    }
}
