using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Business.Interfaces
{
    public interface IAccountService
    {
        Task<AccountApiResponse> GetAllAsync(int pageNumber, int pageSize, string searchValue);
        Task<AccountDTO> GetByIdAsync(int id);
        Task<AccountDTO> AddAccountAsync(AccountDTO accountDTO);
        Task<AccountDTO> UpdateAccountAsync(AccountDTO accountDTO);

        Task<bool> DeleteAccountAsync(int id);
    }
}
