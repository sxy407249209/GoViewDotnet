using GoView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VOL.Core.CacheManager;
using VOL.Core.Configuration;
using VOL.Core.Controllers.Basic;
using VOL.Core.Extensions;
using VOL.Core.Filters;
using VOL.Core.Utilities;
using VOL.Entity.DomainModels;
using VOL.Entity.DomainModels.GoviewVol;
using VOL.System.IRepositories;
using VOL.System.IServices;

namespace VOL.WebApi.Controllers.VolToGoView
{
    /// <summary>
    /// goview用户登录控制器
    /// </summary>
    [Route("api/goview/sys")]
    [ApiController, JWTAuthorize()]
    public class AccountController : ApiBaseController<ISys_UserService>
    {
        private ISys_UserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;
        [ActivatorUtilitiesConstructor]
        public AccountController(
               ISys_UserService userService,
               ISys_UserRepository userRepository,
               IHttpContextAccessor httpContextAccessor
              )
          : base(userService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<GoViewDataReturn> Login(GoViewAccount viewAccount)
        {
            
            var user = await _userRepository
                .DbContext.Set<Sys_User>()
                .FirstOrDefaultAsync(x => x.UserName == viewAccount.username && x.UserPwd == viewAccount.password.Trim().EncryptDES(AppSetting.Secret.User));

            if (user == null)
            {
                GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
                {
                    msg = "用户名或密码不正确",
                    code = 500
                };
                return goViewDataReturn;
            }
            string token = JwtHelper.IssueJwt(new UserInfo()
            {
                User_Id = user.User_Id,
                UserName = user.UserName,
                Role_Id = user.Role_Id
            });
            user.Token = token;
            userinfo userinfo = new userinfo()
            {
                id = user.User_Id.ToString(),
                username = user.UserName,
                password = "",
                nickname = user.UserTrueName,
                depId = null,
                posId = null,
                depName = null,
                posName = null,
            };
            token retoken = new token()
            {
                tokenName = "Authorization",
                tokenValue = "Bearer " + token,
                isLogin = true,
                loginId = user.User_Id.ToString(),
                loginType = "login",
                tokenTimeout = AppSetting.ExpMinutes*60*1000,
                sessionTimeout = AppSetting.ExpMinutes * 60 * 1000,
                tokenSessionTimeout = AppSetting.ExpMinutes * 60 * 1000,
                tokenActivityTimeout = AppSetting.ExpMinutes * 60 * 1000,
                loginDevice = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                tag = null,

            };
            userData userData = new userData()
            {
                token = retoken,
                userinfo = userinfo
            };
            GoViewDataReturn goViewDataReturn1 = new GoViewDataReturn()
            {
                msg = "操作成功",
                code = 200,
                data = userData
            };
            return goViewDataReturn1;

        }

        [HttpGet("Logout")]
        public  GoViewDataReturn Logout()
        {
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                msg = "退出成功",
                code = 200,
            };
            return goViewDataReturn;
        }





    }
}
