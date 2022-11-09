/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹GoviewprojectsController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using GoView.IServices;
namespace GoView.Controllers
{
    [Route("api/Goviewprojects")]
    [PermissionTable(Name = "Goviewprojects")]
    public partial class GoviewprojectsController : ApiBaseController<IGoviewprojectsService>
    {
        public GoviewprojectsController(IGoviewprojectsService service)
        : base(service)
        {
        }
    }
}

