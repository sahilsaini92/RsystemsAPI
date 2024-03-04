using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystemsAssignment.Data.Entities
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int AccountID { get; set; }
        public int ClientID { get; set; }

        public string ClientName { get; set; }
        [NotMapped]
        public Client Client { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppointmentStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppointmentEndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }

}
