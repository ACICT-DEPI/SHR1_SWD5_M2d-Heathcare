using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class TestType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }

}
