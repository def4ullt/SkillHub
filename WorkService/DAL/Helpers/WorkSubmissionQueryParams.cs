using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public class WorkSubmissionQueryParams
    {
        public Guid? TaskId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StatusId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool SortDescending { get; set; } = true;
    }
}
