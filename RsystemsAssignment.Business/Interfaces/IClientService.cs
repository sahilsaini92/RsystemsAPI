using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Business.Interfaces
{
    public interface IClientService
    {
        Task<ClientApiResponse> GetAllAsync(int pageIndex, int pageSize);
        Task<ClientDTO> GetByIdAsync(int id);
        Task<ClientDTO> AddClientAsync(ClientDTO clientDTO);
        Task<ClientDTO> UpdateClientAsync(ClientDTO clientDTO);

        Task<bool> DeleteClientAsync(int id);
    }
}
