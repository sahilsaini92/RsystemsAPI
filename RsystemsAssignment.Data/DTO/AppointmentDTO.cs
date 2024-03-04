using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystemsAssignment.Data.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }

        public int AccountID { get; set; }
        public int ClientID { get; set; }
        
        public string ClientName { get; set; }

        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

}
