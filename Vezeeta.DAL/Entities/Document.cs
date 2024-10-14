using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Document
    {
        [Key]
        public int ID { get; set; }

        public string Image { get; set; } 

        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }

        public int TestTypeID { get; set; }

        [ForeignKey("TestTypeID")]
        public TestType TestType { get; set; }
    }

}
