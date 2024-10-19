using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.BLL.DTOs
{
    public class AppointmentWithRelatedTablesDto
    {
        public int ID { get; set; }

        public DateTime Schedule { get; set; }


        public string Status { get; set; }

        public string Note { get; set; }

        public string Reason { get; set; }

        public string CancellationReason { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public int ClinicId { get; set; }
        public string ClinicName { get;set; }

    }
}
