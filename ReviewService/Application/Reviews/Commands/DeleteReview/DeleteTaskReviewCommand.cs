using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CommandInterfaces;

namespace Application.Reviews.Commands.DeleteReview
{
    public record DeleteTaskReviewCommand(string ReviewId) : ICommand;
}
