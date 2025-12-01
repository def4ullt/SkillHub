using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmissionStatus;

namespace BLL.DTO.WorkSubmission
{
    public class WorkSubmissionReadDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string? TaskName { get; set; }
        public Guid UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public Guid StatusId { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
