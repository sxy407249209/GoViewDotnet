using GoView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VOL.WebApi.Controllers.VolToGoView
{
    [AllowAnonymous]
    [Route("api/goview/sys")]
    public class OssController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        public OssController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("getOssInfo")]
        public GoViewDataReturn GetOssInfo()
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.ToString();
            bool ishttps = _httpContextAccessor.HttpContext.Request.IsHttps;
            string http = ishttps == true ? "https" : "http";
            var oosurl = http + "://" + host + "/api/goview/project/getImages/";
            OssInfo ossInfo = new OssInfo() { bucketURL = oosurl, BucketName = "getuserphoto" };
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 200,
                msg = "返回成功",
                data = ossInfo
            };
            return goViewDataReturn;
        }


    }
}
