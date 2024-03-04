using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Data;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RsystemsAssignment.Business.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;
        public AppointmentService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<AppointmentApiResponse> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                AppointmentApiResponse apiResponse = new AppointmentApiResponse();
                var parameter = new Microsoft.Data.SqlClient.SqlParameter("@MonthNumber", DateTime.UtcNow.Month);
                var data = await _dbContext.Appointment.FromSqlRaw("EXEC usp_select_appointment_shardDB @MonthNumber", parameter)
                    .ToListAsync();
                apiResponse.TotalCount = data.Count;
                data = data.Skip(pageNumber * pageSize).Take(pageSize).ToList();
                var mappedData = _mapper.Map<List<AppointmentDTO>>(data);
                apiResponse.Appointments = mappedData;
                return apiResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<AppointmentDTO> GetByIdAsync(int id)
        {
            var data = await _dbContext.Appointment.Where(x => x.AppointmentID == id).FirstOrDefaultAsync();
            var mappedData = _mapper.Map<AppointmentDTO>(data);
            return mappedData;
        }
        public async Task<AppointmentDTO> AddAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            var mappedData = _mapper.Map<AppointmentDTO>(appointmentDTO);
            var accountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", appointmentDTO.AccountID);
            var clientID = new Microsoft.Data.SqlClient.SqlParameter("@ClientID", appointmentDTO.ClientID);
            var appointmentStartTime = new Microsoft.Data.SqlClient.SqlParameter("@AppointmentStartTime", appointmentDTO.AppointmentStartTime);
            var appointmentEndTime = new Microsoft.Data.SqlClient.SqlParameter("@AppointmentEndTime", appointmentDTO.AppointmentEndTime);
            var data = await _dbContext.Appointment.FromSqlRaw("EXEC usp_insert_appointment_shardDB @AccountID, @ClientID, @AppointmentStartTime, @AppointmentEndTime", accountID, clientID, appointmentStartTime, appointmentEndTime)
                .ToListAsync();            
            //var record = _mapper.Map<AppointmentDTO>(data);
            return null;
        }
        public async Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            try
            {
                var appointmentID = new Microsoft.Data.SqlClient.SqlParameter("@AppointmentID", appointmentDTO.AppointmentID);
                var accountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", appointmentDTO.AccountID);
                var clientID = new Microsoft.Data.SqlClient.SqlParameter("@ClientID", appointmentDTO.ClientID);
                var appointmentStartTime = new Microsoft.Data.SqlClient.SqlParameter("@AppointmentStartTime", appointmentDTO.AppointmentStartTime.AddDays(1));
                var appointmentEndTime = new Microsoft.Data.SqlClient.SqlParameter("@AppointmentEndTime", appointmentDTO.AppointmentEndTime);
                var data = await _dbContext.Appointment.FromSqlRaw("EXEC usp_update_appointment_shardDB @AppointmentID, @AccountID, @ClientID, @AppointmentStartTime, @AppointmentEndTime",appointmentID, accountID, clientID, appointmentStartTime, appointmentEndTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentID, int accountID)
        {
            var AppointmentID = new Microsoft.Data.SqlClient.SqlParameter("@AppoinmentID", appointmentID);
            var AccountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", accountID);
            var data = await _dbContext.Appointment.FromSqlRaw("EXEC usp_delete_appointment_shardDB @AccountID,@AppoinmentID", AccountID, AppointmentID).ToListAsync();
            return true;
        }
    }
}
