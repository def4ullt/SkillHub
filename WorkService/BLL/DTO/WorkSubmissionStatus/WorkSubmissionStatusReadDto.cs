using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.WorkSubmissionStatus
{
    public class WorkSubmissionStatusReadDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
