/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹GoviewprojectdatasController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using GoView.IServices;
namespace GoView.Controllers
{
    [Route("api/Goviewprojectdatas")]
    [PermissionTable(Name = "Goviewprojectdatas")]
    public partial class GoviewprojectdatasController : ApiBaseController<IGoviewprojectdatasService>
    {
        public GoviewprojectdatasController(IGoviewprojectdatasService service)
        : base(service)
        {
        }
    }
}

