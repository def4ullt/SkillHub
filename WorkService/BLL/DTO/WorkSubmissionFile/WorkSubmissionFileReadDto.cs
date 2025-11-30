using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.WorkSubmissionFile
{
    public class WorkSubmissionFileReadDto
    {
        public Guid Id { get; set; }
        public Guid WorkSubmissionId { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public string? FileUrl { get; set; }
    }
}
