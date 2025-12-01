using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmissionFile;

namespace BLL.DTO.WorkSubmission
{
    public class WorkSubmissionUpdateDto
    {
        public Guid StatusId { get; set; }
        public List<WorkSubmissionFileUpdateDto>? Files { get; set; }
    }
}
