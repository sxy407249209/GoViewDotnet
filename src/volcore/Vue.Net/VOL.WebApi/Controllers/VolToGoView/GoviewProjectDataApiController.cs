using GoView;
using GoView.IRepositories;
using GoView.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;
using VOL.Core.Controllers.Basic;
using VOL.Core.Filters;
using VOL.Entity.DomainModels;
using VOL.WebApi.Controllers.GoView;

namespace VOL.WebApi.Controllers.VolToGoView
{
    [Route("api/goview/project")]
    [ApiController, JWTAuthorize()]
    public class GoviewProjectDataApiController : ApiBaseController<IGoviewprojectsService>
    {
        private readonly IGoviewprojectdatasRepository _repository;//访问数据库

        [ActivatorUtilitiesConstructor]
        public GoviewProjectDataApiController(
            IGoviewprojectsService service,
             IGoviewprojectdatasRepository dbRepository
        )
        : base(service)
        {
            _repository = dbRepository;
        }


        [AllowAnonymous]
        [HttpGet("getData")]
        public async Task<GoViewDataReturn> Get(string projectId)
        {
            var pid = Convert.ToInt32(projectId);
            var model = await _repository.DbContext.Set<Goviewprojectdatas>().FirstOrDefaultAsync(x => x.ProjectId == pid);
            var pro = await _repository.DbContext.Set<Goviewprojects>().FirstOrDefaultAsync(x => x.Id == pid);
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 500,
                msg = "获取失败",
            };
            if (pro != null && model != null)
            {
                if (model.ContentData == null)
                {

                    goViewDataReturn.code = 200;
                    goViewDataReturn.msg = "无数据";
                    goViewDataReturn.data = null;
                    return goViewDataReturn;
                }
                else
                {
                    var temp = new
                    {
                        content = Encoding.UTF8.GetString(model.ContentData),
                        createTime = pro.CreateTime,
                        createUserId = pro.CreateUserId.ToString(),
                        id = projectId,
                        indexImage = pro.IndexImage,
                        isDelete = pro.IsDelete,
                        projectName = pro.ProjectName,
                        remarks = pro.Remarks,
                        state = pro.State
                    };
                    goViewDataReturn.code = 200;
                    goViewDataReturn.msg = "获取成功";
                    goViewDataReturn.data = temp;
                    return goViewDataReturn;
                }
            }

            return goViewDataReturn;

        }

        [StringNeedLTGT]
        [HttpPost("save/data")]
        public async Task<GoViewDataReturn> Edit([FromForm] GoviewProjectDataCURDVM data)
        {
            var model = await _repository.DbContext.Set<Goviewprojectdatas>().FirstOrDefaultAsync(x => x.ProjectId == data.projectId);
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 500,
                msg = "数据保存失败",
            };
            if (model != null)
            {
                model.ContentData = Encoding.ASCII.GetBytes(data.content);
                _repository.DbContext.Set<Goviewprojectdatas>().Update(model);
                int x = await _repository.DbContext.SaveChangesAsync();
                if (x > 0)
                {

                    goViewDataReturn.code = 200;
                    goViewDataReturn.msg = "数据保存成功";
                    
                    return goViewDataReturn;
                }
            }
            return goViewDataReturn;
        }





    }
}
