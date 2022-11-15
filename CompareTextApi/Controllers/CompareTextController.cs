using Microsoft.AspNetCore.Mvc;

namespace CompareTextApi.Controllers
{
    [ApiController]
    [Route("v1/diff")]
    public class CompareTextController : ControllerBase
    {

        private readonly ILogger<CompareTextController> _logger;

        public CompareTextController(ILogger<CompareTextController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("{id:int}/left")]
        public void SetLeftValueToCompare(int id, [FromBody] string text)
        {
            Results.Ok();
        }

        [HttpPost]
        [Route("{id:int}/right")]
        public void SetRightValueToCompare(int id, [FromBody] string text)
        {
            Results.Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public void Compare()
        {
            Results.Ok();
        }
    }
}