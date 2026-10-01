using Microsoft.AspNetCore.Mvc;
using CSWMS.AIServices;
namespace CSWMS.Controllers
{
    public class AIServiceController : Controller
    {
        public readonly IAIService _aIService;
        public AIServiceController(IAIService aIService)
        {
            _aIService=aIService;
        }
        public IActionResult GetQuestion()
        {
            AIServiceModel AIService=new AIServiceModel();
            return View("~/Views/AIService/GetQuestion.cshtml", AIService);
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuestion(string question)
        {
            try
            {
                var embedding = await _aIService.GetAnswerAsync(question);

                return Ok(embedding);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
