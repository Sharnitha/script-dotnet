using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
 
namespace MyMvcApp.Controllers
{
    public class PipelineController : Controller
    {
        private readonly GitHubService _gitHubService;
        private readonly IConfiguration _configuration;
 
        public PipelineController(GitHubService gitHubService, IConfiguration configuration)
        {
            _gitHubService = gitHubService;
            _configuration = configuration;
        }
 
        [HttpPost]
        public async Task<IActionResult> TriggerPipeline(string branch)
        {
            string owner = _configuration["GitHub:Owner"];
            string repo = _configuration["GitHub:Repo"];
            string workflowFileName = _configuration["GitHub:WorkflowFile"];
 
            bool success = await _gitHubService.TriggerGitHubWorkflow(owner, repo, workflowFileName, branch);
            if (success)
                return Json(new { message = "Pipeline triggered successfully!" });
            else
                return Json(new { message = "Failed to trigger pipeline." });
        }
 
        public IActionResult Trigger()
        {
            return View();
        }
    }
}
