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
    public class WorkSubmissionSeeder
    {
        public static async Task SeedAsync(IUnitOfWork uow)
        {
            List<WorkSubmission> submissionsList = (await uow.WorkSubmissions.GetAllAsync()).ToList();
            if (submissionsList.Count > 0) return;

            List<WorkSubmissionStatus> statusList = (await uow.WorkSubmissionStatuses.GetAllAsync()).ToList();

            Faker<WorkSubmission> faker = new Faker<WorkSubmission>()
                .RuleFor((WorkSubmission ws) => ws.TaskId, (Faker f) => Guid.NewGuid())
                .RuleFor((WorkSubmission ws) => ws.TaskName, (Faker f) => f.Lorem.Sentence(3))
                .RuleFor((WorkSubmission ws) => ws.UserId, (Faker f) => Guid.NewGuid())
                .RuleFor((WorkSubmission ws) => ws.UserFirstName, (Faker f) => f.Name.FirstName())
                .RuleFor((WorkSubmission ws) => ws.UserLastName, (Faker f) => f.Name.LastName())
                .RuleFor((WorkSubmission ws) => ws.StatusId, (Faker f) => f.PickRandom(statusList).Id)
                .RuleFor((WorkSubmission ws) => ws.SubmissionDate, (Faker f) => f.Date.Recent(30));

            List<WorkSubmission> submissions = faker.Generate(5);
            foreach (WorkSubmission ws in submissions)
            {
                await uow.WorkSubmissions.AddAsync(ws);
            }
        }
    }
}
