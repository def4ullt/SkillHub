using Ardalis.Specification;
using Domain.Entities;
using Domain.Query;

namespace DAL.SpecificationPattern
{
    public class TaskWithFiltersSpecification : Specification<TaskEntity>
    {
        public TaskWithFiltersSpecification(TaskQueryParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Title))
                Query.Where(t => t.Title.Contains(parameters.Title));

            if (parameters.Difficulty.HasValue)
                Query.Where(t => t.Difficulty == parameters.Difficulty.Value);

            if (parameters.EstimatedTimeMin.HasValue)
                Query.Where(t => t.EstimatedTimeMinutes >= parameters.EstimatedTimeMin.Value);

            if (parameters.EstimatedTimeMax.HasValue)
                Query.Where(t => t.EstimatedTimeMinutes <= parameters.EstimatedTimeMax.Value);

            if (parameters.XpRewardMin.HasValue)
                Query.Where(t => t.XpReward >= parameters.XpRewardMin.Value);

            if (parameters.XpRewardMax.HasValue)
                Query.Where(t => t.XpReward <= parameters.XpRewardMax.Value);

            if (parameters.IsActive.HasValue)
                Query.Where(t => t.IsActive == parameters.IsActive.Value);

            if (parameters.TechnologyIds != null && parameters.TechnologyIds.Count > 0)
                Query.Where(t => t.TaskTechnologies.Any(tt => parameters.TechnologyIds.Contains(tt.TechnologyId)));

            if (parameters.TagIds != null && parameters.TagIds.Count > 0)
                Query.Where(t => t.TaskTags.Any(tt => parameters.TagIds.Contains(tt.TagId)));

            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                switch (parameters.SortBy.ToLower())
                {
                    case "title":
                        Query.OrderBy(t => t.Title, parameters.SortDesc);
                        break;
                    case "xpreward":
                        Query.OrderBy(t => t.XpReward, parameters.SortDesc);
                        break;
                    case "estimatedtimeminutes":
                        Query.OrderBy(t => t.EstimatedTimeMinutes, parameters.SortDesc);
                        break;
                    case "difficulty":
                        Query.OrderBy(t => t.Difficulty, parameters.SortDesc);
                        break;
                }
            }

            Query.Include(t => t.TaskTechnologies)
                 .ThenInclude(tt => tt.Technology);

            Query.Include(t => t.TaskTags)
                 .ThenInclude(tt => tt.Tag);

        }
    }
}