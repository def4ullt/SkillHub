using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DAL.Unit_of_work;
using Domain.Entities;

namespace DAL.Seeder
{
    public class WorkSubmissionStatusSeeder
    {
        public static async Task SeedAsync(IUnitOfWork uow)
        {
            List<WorkSubmissionStatus> statusesList = (await uow.WorkSubmissionStatuses.GetAllAsync()).ToList();
            if (statusesList.Count > 0) return;

            string[] statusNames = new string[] { "Pending", "Approved", "Rejected", "InReview", "Completed" };

            foreach (string name in statusNames)
            {
                WorkSubmissionStatus status = new WorkSubmissionStatus
                {
                    Name = name,
                    CreatedAt = DateTime.UtcNow
                };
                await uow.WorkSubmissionStatuses.AddAsync(status);
            }
        }
    }
}
