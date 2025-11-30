using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskTechnology
    {
        public Guid TaskId { get; set; }
        public TaskEntity Task { get; set; }
        public Guid TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }
}
