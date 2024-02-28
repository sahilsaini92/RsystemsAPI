using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Data;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using System;
using System.Collections.Generic;
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
            AppointmentApiResponse apiResponse = new AppointmentApiResponse();
            var query = _dbContext.Appointment;
            apiResponse.TotalCount = query.Count();
            var data = await query.Include(a=>a.Client).Skip(pageNumber * pageSize)
                         .Take(pageSize).ToListAsync();
            var mappedData = _mapper.Map<List<AppointmentDTO>>(data);
            apiResponse.Appointments = mappedData;
            return apiResponse;
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
            await _dbContext.AddAsync(mappedData);
            await _dbContext.SaveChangesAsync();
            var insertedRecord = _dbContext.Appointment.Find(_dbContext.Appointment.Max(p => p.AppointmentID));
            var record = _mapper.Map<AppointmentDTO>(insertedRecord);
            return record;
        }
        public async Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            try
            {
                var dbRecord = await _dbContext.Appointment.Where(a => a.AppointmentID == appointmentDTO.AppointmentID).FirstOrDefaultAsync();
                if (dbRecord != null)
                {
                    var mappedData = _mapper.Map(appointmentDTO, dbRecord);
                    dbRecord.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var dbRecord = await _dbContext.Appointment.Where(a => a.AppointmentID == id).FirstOrDefaultAsync();
            if (dbRecord != null)
            {
                _dbContext.Appointment.Remove(dbRecord);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
