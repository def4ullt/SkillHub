using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.CommandInterfaces;

namespace Application.Reviews.Commands.UpdateReview
{
    public record UpdateTaskReviewCommand(
        string ReviewId,
        int Rating,
        string Comment,
        Guid RequestingUserId,
        bool IsAdmin = false
    ) : ICommand;
}
