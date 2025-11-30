using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Technology
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 

        public ICollection<TaskTechnology> TaskTechnologies { get; set; } = new List<TaskTechnology>();
    }
}
