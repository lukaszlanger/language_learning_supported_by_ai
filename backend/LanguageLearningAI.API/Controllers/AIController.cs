using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearningAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly OpenAIService _aiService;

        public AIController(OpenAIService aiService)
        {
            _aiService = aiService;
        }

        
    }
}