using System.Net.Http.Json;

namespace BLL.Services
{
    public interface ITaskServiceClient
    {
        Task<Guid?> GetTaskAuthorIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    }

    public class TaskServiceClient : ITaskServiceClient
    {
        private readonly HttpClient http;

        public TaskServiceClient(HttpClient http)
        {
            this.http = http;
        }

        public async Task<Guid?> GetTaskAuthorIdAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await http.GetAsync($"api/tasks/{taskId}", cancellationToken);
                if (!response.IsSuccessStatusCode) return null;

                var task = await response.Content.ReadFromJsonAsync<TaskDto>(cancellationToken: cancellationToken);
                return task?.AuthorId;
            }
            catch
            {
                return null;
            }
        }

        private record TaskDto(Guid Id, Guid AuthorId);
    }
}
