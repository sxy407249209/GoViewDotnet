using GoView;
using GoView.IRepositories;
using GoView.IServices;
using GoView.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using VOL.Core.Controllers.Basic;
using VOL.Core.Extensions;
using VOL.Core.Filters;
using VOL.Core.ManageUser;
using VOL.Entity.DomainModels;
using VOL.Entity.DomainModels.GoviewVol;

namespace VOL.WebApi.Controllers.VolToGoView
{
    [Route("api/goview/project")]
    [ApiController, JWTAuthorize()]
    public class GoviewProjectApiController : ApiBaseController<IGoviewprojectsService>
    {
        private IGoviewprojectsRepository _goviewprojectsRepository;//访问业务代码

        [ActivatorUtilitiesConstructor]
        public GoviewProjectApiController(
            IGoviewprojectsService service,
            IGoviewprojectsRepository  goviewprojectsRepository
        )
        : base(service)
        {
            _goviewprojectsRepository = goviewprojectsRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(int page = 1, int limit = 10)
        {
            var goviewProjectData = await _goviewprojectsRepository.DbContext.Set<Goviewprojects>()
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(x => new
                {
                    id = x.Id.ToString(),
                    projectName = x.ProjectName,
                    state = x.State,
                    createTime = x.CreateTime,
                    createUserId = x.CreateUserId.ToString(),
                    isDelete = x.IsDelete,
                    indexImage = x.IndexImage,
                    remarks = x.Remarks
                })
                .ToListAsync();

            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 200,
                msg = "获取成功",
                count = goviewProjectData.Count(),
                data = goviewProjectData,
            };
            return Ok(goViewDataReturn);
        }

        [HttpDelete("delete")]
        public async Task<GoViewDataReturn> Delete(string ids)
        {
            var id = Convert.ToUInt32(ids);
            var model = await _goviewprojectsRepository.DbContext.Set<Goviewprojects>()
                .FirstOrDefaultAsync(x => x.Id == id);
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 500,
                msg = "操作失败",
            };
            if (model==null)
            {               
                return goViewDataReturn;
            }
            model.IsDelete = 1;
            _goviewprojectsRepository.DbContext.Set<Goviewprojects>().Update(model);
            int x = await _goviewprojectsRepository.DbContext.SaveChangesAsync();
            if (x<0)
            {
                goViewDataReturn.code = 200;
                goViewDataReturn.msg = "操作成功";
                return goViewDataReturn;
            }
            return goViewDataReturn;
        }

        [HttpPost("edit")]
        public async Task<GoViewDataReturn> Edit(GoviewProjectCURDVM data)
        {
            var pid = Convert.ToInt32(data.id);
            var model = await _goviewprojectsRepository.DbContext.Set<Goviewprojects>()
                .FirstOrDefaultAsync(x => x.Id == pid);
            model.IndexImage = data.indexImage;
            model.ProjectName = data.projectName != null ? data.projectName : model.ProjectName;
            model.Remarks = data.remarks != null ? data.remarks : model.Remarks;
            _goviewprojectsRepository.DbContext.Set<Goviewprojects>().Update(model);
            int x = _goviewprojectsRepository.DbContext.SaveChanges();
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 500,
                msg = "操作失败",
            };
            if (x < 0)
            {               
                return goViewDataReturn;
            }
            else
            {
                goViewDataReturn.code = 200;
                goViewDataReturn.msg = "操作成功";
                return goViewDataReturn;
            }
        }

        [HttpPost("create")]
        public async Task<GoViewDataReturn> Add(GoviewProjectCURDVM data)
        {
            Goviewprojects goviewProject = new Goviewprojects()
            {
                ProjectName = data.projectName,
                IndexImage = data.indexImage,
                Remarks = data.remarks,
                CreateUserId = UserContext.Current.UserId,
                CreateTime = DateTime.Now,
                IsDelete = -1,
                State = -1,
            };
            await _goviewprojectsRepository.DbContext.Set<Goviewprojects>().AddAsync(goviewProject);
            int x = await _goviewprojectsRepository.DbContext.SaveChangesAsync();
            GoViewDataReturn goViewDataReturn = new GoViewDataReturn()
            {
                code = 500,
                msg = "操作失败",
            };
            if (x < 0)
            {
                return goViewDataReturn;
            }
            else
            {
                goViewDataReturn.code = 200;
                goViewDataReturn.msg = "操作成功";
                return goViewDataReturn;
            }
        }


    }
}
