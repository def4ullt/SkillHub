using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.QueryParams
{
    public class TaskReviewQueryParameters : QueryStringParameters
    {
        public Guid? UserId { get; set; }
        public Guid? TaskId { get; set; }
        public int? Rating { get; set; }
    }
}
