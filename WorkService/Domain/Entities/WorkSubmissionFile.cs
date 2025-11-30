using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace Domain.Entities
{
    public class WorkSubmissionFile
    {
        public Guid Id { get; set; }
        public Guid WorkSubmissionId { get; set; }
        public WorkSubmission? WorkSubmission { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public SubmissionDeliveryMethod? DeliveryMethod { get; set; }
        public string? FileUrl { get; set; }
    }
}
