using Microsoft.AspNetCore.Mvc;

namespace CompareTextApi.Controllers
{
    /// <summary>
    /// Controller which helps with testing
    /// </summary>
    [Route("ForTests")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Generate random GUID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getRandomGuid")]
        public Guid GetRandomGuid()
        {
            return Guid.NewGuid();
        }
    }
}
