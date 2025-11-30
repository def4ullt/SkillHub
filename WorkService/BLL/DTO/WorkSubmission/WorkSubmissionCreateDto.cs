using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmissionFile;

namespace BLL.DTO.WorkSubmission
{
    public class WorkSubmissionCreateDto
    {
        public Guid TaskId { get; set; }
        public string? TaskName { get; set; }
        public Guid UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public Guid StatusId { get; set; }
        public List<WorkSubmissionFileCreateDto>? Files { get; set; }
    }
}
