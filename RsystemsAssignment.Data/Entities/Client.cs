using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystemsAssignment.Data.Entities
{
    public class Client
    {
        public int ClientID { get; set; }
        [Required]
        public string ClientName { get; set; }
        public Account Account { get; set; }
        [Required]
        public int AccountID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

    }
}
