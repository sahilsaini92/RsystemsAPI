using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Data;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RsystemsAssignment.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;
        public AccountService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<AccountApiResponse> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                AccountApiResponse apiResponse = new AccountApiResponse();
                var data = _dbContext.Accounts.FromSqlRaw("EXEC usp_select_account_shardDB")
                   .ToList();
                apiResponse.TotalCount = data.Count;
                data = data.Skip(pageNumber * pageSize)
                             .Take(pageSize).ToList();
                var mappedData = _mapper.Map<List<AccountDTO>>(data);
                apiResponse.Accounts = mappedData;
                return apiResponse;
            }
            catch(Exception exp)
            {
                return null;
            }
        }
        public async Task<AccountDTO> GetByIdAsync(int id)
        {
            var data = await _dbContext.Accounts.Where(x => x.AccountID == id).FirstOrDefaultAsync();
            var mappedData = _mapper.Map<AccountDTO>(data);
            return mappedData;
        }
        public async Task<AccountDTO> AddAccountAsync(AccountDTO accountDTO)
        {
            try
            {
                var mappedData = _mapper.Map<Account>(accountDTO);
                mappedData.CreatedDate = DateTime.Now;
                mappedData.ModifiedDate = null;
                await _dbContext.AddAsync(mappedData);
                await _dbContext.SaveChangesAsync();
                var insertedRecord = _dbContext.Accounts.Find(_dbContext.Accounts.Max(p => p.AccountID));
                var record = _mapper.Map<AccountDTO>(insertedRecord);
                return record;
            }
            catch (Exception exp)
            {
                return null;
            }
        }
        public async Task<AccountDTO> UpdateAccountAsync(AccountDTO accountDTO)
        {
            try
            {

                var accountName = new Microsoft.Data.SqlClient.SqlParameter("@AccountName", accountDTO.AccountName);
                var accountID = new Microsoft.Data.SqlClient.SqlParameter("@AccountID", accountDTO.AccountID);
                var data = _dbContext.Accounts.FromSqlRaw("EXEC usp_update_account_shardDB @AccountID, @AccountName", accountID, accountName)
                    .ToList();
                return null;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var dbRecord = await _dbContext.Accounts.Where(a => a.AccountID == id).FirstOrDefaultAsync();
            if (dbRecord != null)
            {
                _dbContext.Accounts.Remove(dbRecord);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
