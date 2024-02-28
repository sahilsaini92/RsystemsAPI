using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystemsAssignment.Data.DTO
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        
        [Required]
        public string AccountName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
