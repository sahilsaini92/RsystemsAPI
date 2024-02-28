using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystemsAssignment.Data.DTO
{
    public class ClientDTO
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public AccountDTO? Account { get; set; }
        public int AccountID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
