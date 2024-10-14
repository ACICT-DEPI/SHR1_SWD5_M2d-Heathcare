using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Medicine
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }

}
