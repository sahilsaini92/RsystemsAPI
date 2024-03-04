using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Data;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<ClientApiResponse> GetAllAsync(int pageIndex, int pageSize,int accountID, string? searchValue)
        {
            if (searchValue == "null")
            {
                searchValue = "";
            }
            ClientApiResponse apiResponse = new ClientApiResponse();
            var parameter = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", accountID);
            var data = await _dbContext.Clients.FromSqlRaw("EXEC usp_select_client_shardDB @AccountID", parameter)
                .ToListAsync();
           
            apiResponse.TotalCount = data.Count;
            data = data.Where(x => !string.IsNullOrEmpty(searchValue) ? x.ClientName.ToLower().Contains(searchValue.ToLower()) : true).Skip(pageIndex * pageSize)
                             .Take(pageSize).ToList();

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

            var accountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", clientDTO.AccountID);
            var clientName = new Microsoft.Data.SqlClient.SqlParameter("@ClientName", clientDTO.ClientName);
            var data = await _dbContext.Clients.FromSqlRaw("EXEC usp_insert_client_shardDB @AccountID, @ClientName", accountID, clientName)
                .ToListAsync();

            return null;
        }
        public async Task<ClientDTO> UpdateClientAsync(ClientDTO clientDTO)
        {
            try
            {
                var accountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", clientDTO.AccountID);
                var clientID = new Microsoft.Data.SqlClient.SqlParameter("@ClientID", clientDTO.ClientID);
                var clientName = new Microsoft.Data.SqlClient.SqlParameter("@ClientName", clientDTO.ClientName);
                var data =await _dbContext.Clients.FromSqlRaw("EXEC usp_update_client_shardDB @AccountID, @ClientID, @ClientName", accountID, clientID, clientName)
                    .ToListAsync();
                return null;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<bool> DeleteClientAsync(int id,int accountID)
        {
            var clientId = new Microsoft.Data.SqlClient.SqlParameter("@ClientID", id);
            var AccountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", accountID);
            var data = await _dbContext.Clients.FromSqlRaw("EXEC usp_delete_client_shardDB @AccountID,@ClientID", AccountID, clientId).ToListAsync();
            return true;
        }
    }
}
