using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DAL.Unit_of_work;
using Domain.Entities;
using Microsoft.VisualBasic.FileIO;

namespace DAL.Seeder
{
    public class WorkSubmissionFileSeeder
    {
        public static async Task SeedAsync(IUnitOfWork uow)
        {
            List<WorkSubmissionFile> filesList = (await uow.WorkSubmissionFiles.GetAllAsync()).ToList();
            if (filesList.Count > 0) return;

            List<WorkSubmission> submissions = (await uow.WorkSubmissions.GetAllAsync()).ToList();
            List<SubmissionDeliveryMethod> deliveryMethods = (await uow.SubmissionDeliveryMethods.GetAllAsync()).ToList();

            Faker<WorkSubmissionFile> faker = new Faker<WorkSubmissionFile>()
                .RuleFor((WorkSubmissionFile f) => f.WorkSubmissionId, (Faker f) => f.PickRandom(submissions).Id)
                .RuleFor((WorkSubmissionFile f) => f.DeliveryMethodId, (Faker f) => f.PickRandom(deliveryMethods).Id)
                .RuleFor((WorkSubmissionFile f) => f.FileUrl, (Faker f) => f.Internet.Url());

            List<WorkSubmissionFile> files = faker.Generate(5);
            foreach (WorkSubmissionFile f in files)
            {
                await uow.WorkSubmissionFiles.AddAsync(f);
            }
        }
    }
}
