using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Data;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;
        public ClientService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ClientApiResponse> GetAllAsync(int pageIndex, int pageSize)
        {
            ClientApiResponse apiResponse = new ClientApiResponse();
            var query = _dbContext.Clients;
            apiResponse.TotalCount = query.Count();
            var data = await query.Include(c => c.Account).Skip(pageIndex * pageSize)
                         .Take(pageSize).ToListAsync();
            var mappedData = _mapper.Map<List<ClientDTO>>(data);
            apiResponse.Clients = mappedData;
            return apiResponse;
        }
        public async Task<ClientDTO> GetByIdAsync(int id)
        {
            var data = await _dbContext.Clients.Where(x => x.ClientID == id).FirstOrDefaultAsync();
            var mappedData = _mapper.Map<ClientDTO>(data);
            return mappedData;
        }
        public async Task<ClientDTO> AddClientAsync(ClientDTO clientDTO)
        {
            var mappedData = _mapper.Map<Client>(clientDTO);
            mappedData.CreatedDate = DateTime.Now;
            mappedData.ModifiedDate = null;
            await _dbContext.AddAsync(mappedData);
            await _dbContext.SaveChangesAsync();
            var insertedRecord = _dbContext.Clients.Find(_dbContext.Clients.Max(p => p.ClientID));
            var record = _mapper.Map<ClientDTO>(insertedRecord);
            return record;
        }
        public async Task<ClientDTO> UpdateClientAsync(ClientDTO clientDTO)
        {
            try
            {
                var dbRecord = await _dbContext.Clients.Where(a => a.ClientID == clientDTO.ClientID).FirstOrDefaultAsync();
                if (dbRecord != null)
                {
                    var mappedData = _mapper.Map(clientDTO, dbRecord);
                    dbRecord.ModifiedDate = DateTime.UtcNow;
                    dbRecord.Account.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
                return null;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var dbRecord = await _dbContext.Clients.Where(a => a.ClientID == id).FirstOrDefaultAsync();
            if (dbRecord != null)
            {
                _dbContext.Clients.Remove(dbRecord);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
