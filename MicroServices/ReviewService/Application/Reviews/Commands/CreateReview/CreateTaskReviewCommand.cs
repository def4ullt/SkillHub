using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;
using Domain.ValueObjects;

namespace Application.Reviews.Commands.CreateReview
{
    public record CreateTaskReviewCommand(
        Guid TaskId,
        int Rating,
        string Comment,
        UserInformation User
    ) : ICommand<string>;
}
