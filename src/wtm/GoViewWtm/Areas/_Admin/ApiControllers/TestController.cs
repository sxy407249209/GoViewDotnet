using Microsoft.AspNetCore.Mvc;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace GoViewWtm.Areas._Admin.ApiControllers
{
    [AuthorizeJwtWithCookie]
    [Public]
    [Route("api/test")]
    [AllRights]
    [ActionDescription("_Admin.FileApi")]
    public class TestController : BaseController
    {

        [HttpGet("index")]
        public IActionResult Index()
        {
            Retest retest = new()
            {
                Code = 200,
                msg = "测试"
            };
           

            return Ok(retest);
        }
    }

    public class Retest
    {
        public int Code { get; set; }

        public string msg { get; set; }
    }
}
