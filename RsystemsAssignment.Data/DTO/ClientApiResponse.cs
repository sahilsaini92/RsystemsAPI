using RSystemsAssignment.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Data.DTO
{
    public class ClientApiResponse
    {
        public List<ClientDTO> Clients { get; set; }

        public int TotalCount { get; set; }
    }
}
