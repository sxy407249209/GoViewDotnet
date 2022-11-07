using GoViewWtm.ViewModel.GoViewApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace GoViewWtm.Areas.GoViewApi.Controllers
{
    [Route("api/goview/sys")]
    [AuthorizeJwtWithCookie]
    [ApiController]
    [ActionDescription("获取文件上传oss信息")]
    public class OssController : BaseApiController
    {
        private IHttpContextAccessor _httpContextAccessor;
        public OssController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Public]
        [Route("getOssInfo")]
        public IActionResult GetOssInfo()
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.ToString();
            bool ishttps = _httpContextAccessor.HttpContext.Request.IsHttps;
            string http = ishttps == true ? "http" : "https";
            var oosurl = http + "://" + host + "/api/_file/getuserphoto/";
            OssInfo ossInfo = new() { bucketURL = oosurl,BucketName= "getuserphoto" };
            GoViewDataReturn goViewDataReturn = new()
            {
                code = 200,
                msg = "返回成功",
                data = ossInfo
            };
            return Ok(goViewDataReturn);
        }
    }
}
