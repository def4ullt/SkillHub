using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly HttpClient http;
        private readonly string baseUrl;

        public SentimentAnalysisService(IConfiguration config)
        {
            baseUrl = config["MLService:BaseUrl"] ?? "http://localhost:5400";
            http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        }

        public async Task<(string Sentiment, List<string> KeyIssues)> AnalyzeAsync(string comment, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return ("Neutral", new List<string>());

            try
            {
                var body = new { comment };
                var response = await http.PostAsJsonAsync($"{baseUrl}/analyze", body, cancellationToken);
                if (!response.IsSuccessStatusCode) return ("Neutral", new List<string>());

                var result = await response.Content.ReadFromJsonAsync<MlAnalyzeResponse>(cancellationToken: cancellationToken);
                return (result?.Sentiment ?? "Neutral", result?.Keywords ?? new List<string>());
            }
            catch
            {
                return ("Neutral", new List<string>());
            }
        }

        private record MlAnalyzeResponse(string Sentiment, int Score, List<string> Keywords);
    }
}
