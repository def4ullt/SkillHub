using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskTag
    {
        public Guid TaskId { get; set; }
        public TaskEntity Task { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
