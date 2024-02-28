using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystemsAssignment.Data.Entities
{
    public class Account
    {
        public int AccountID { get; set; }
        
        [Required]
        public string AccountName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}
