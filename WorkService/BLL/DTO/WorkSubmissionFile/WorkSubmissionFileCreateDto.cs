using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.WorkSubmissionFile
{
    public class WorkSubmissionFileCreateDto
    {
        public Guid DeliveryMethodId { get; set; }
        public string? FileUrl { get; set; }
    }
}
