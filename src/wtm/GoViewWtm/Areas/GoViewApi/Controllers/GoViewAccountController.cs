using GoViewWtm.ViewModel.GoViewApi;
using GoViewWtm.ViewModel.GoViewApi.GoViewAccountVMs;
using GoViewWtm.ViewModel.HomeVMs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace GoViewWtm.Areas.GoViewApi.Controllers
{
    [Route("api/goview/sys")]
    [ApiController]
    [AuthorizeJwtWithCookie]
    [ActionDescription("用户登录操作")]
    public class GoViewAccountController : BaseApiController
    {
        private IHttpContextAccessor _httpContextAccessor;
        public GoViewAccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Public]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(GoViewAccountVM vm)
        {
            var user = Wtm.DoLogin(vm.username, vm.password, "");
            if (user == null)
            {
                GoViewDataReturn goViewDataReturn = new()
                {
                    msg = "用户名或密码不正确",
                    code = 500
                };
                return Ok(goViewDataReturn);
            }
            Wtm.LoginUserInfo = user;
            var authService = HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;
            var token = await authService.IssueTokenAsync(Wtm.LoginUserInfo);
            userinfo userinfo = new()
            {
                id = user.UserId,
                username = user.ITCode,
                password = "",
                nickname = user.Name,
                depId = null,
                posId = null,
                depName = null,
                posName = null,
            };
            token retoken = new()
            {
                tokenName= "Authorization",
                tokenValue = "Bearer " + token.AccessToken,
                isLogin = true,
                loginId = user.UserId,
                loginType = "login",
                tokenTimeout = ConfigInfo.JwtOptions.Expires *1000,
                sessionTimeout = ConfigInfo.JwtOptions.Expires * 1000,
                tokenSessionTimeout = ConfigInfo.JwtOptions.Expires * 1000,
                tokenActivityTimeout = ConfigInfo.JwtOptions.Expires * 1000,
                loginDevice = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                tag = null,

            };

            userData userData = new()
            {
                token = retoken,
                userinfo = userinfo
            };
            GoViewDataReturn goViewDataReturn1 = new()
            {
                msg = "操作成功",
                code = 200,
                data = userData
            };
            return Ok(goViewDataReturn1);

        }
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            GoViewDataReturn goViewDataReturn = new()
            {
                msg = "退出成功",
                code = 200,
            };
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                await Wtm.CallAPI<string>("mainhost", "/api/_account/logout", HttpMethodEnum.GET, new { }, 10);
                return Ok(goViewDataReturn);
            }
            else
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok(goViewDataReturn);
            }
        }




    }
}
