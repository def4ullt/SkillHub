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
    public class SubmissionDeliveryMethodSeeder
    {
        public static async Task SeedAsync(IUnitOfWork uow)
        {
            List<SubmissionDeliveryMethod> methodsList = (await uow.SubmissionDeliveryMethods.GetAllAsync()).ToList();
            if (methodsList.Count > 0) return;

            string[] methodNames = new string[] { "GitHub", "Zip", "Link" };

            foreach (string name in methodNames)
            {
                SubmissionDeliveryMethod method = new SubmissionDeliveryMethod
                {
                    Name = name,
                    CreatedAt = DateTime.UtcNow
                };
                await uow.SubmissionDeliveryMethods.AddAsync(method);
            }
        }
    }
}
