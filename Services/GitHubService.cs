using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
 
namespace MyMvcApp.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
 
        public GitHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
 
        public async Task<bool> TriggerGitHubWorkflow(string owner, string repo, string workflowFile, string branch)
        {
            string token = _configuration["GitHub:Token"];
string url = $"https://api.github.com/repos/{owner}/{repo}/actions/workflows/{workflowFile}/dispatches";
 
            var requestData = new { ref = branch };
            string json = JsonSerializer.Serialize(requestData);
 
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "MyMvcApp");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
 
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
