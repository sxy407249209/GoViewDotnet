using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using GoViewWtm.ViewModel.GoViewApi.GoviewProjectVMs;
using GoViewWtm.Model.GoViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Fare;
using GoViewWtm.ViewModel.GoViewApi;
using NPOI.SS.Formula.Functions;
using NuGet.Packaging.Signing;
using Microsoft.CodeAnalysis;

namespace GoViewWtm.Controllers
{
    [Area("GoViewApi")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("项目信息")]
    [ApiController]
    [Route("api/goview/project")]
    public partial class GoviewProjectApiController : BaseApiController
    {
        //列表
        [ActionDescription("Sys.Search")]
        [HttpGet("list")]
        public IActionResult Search(int page = 1, int limit = 10)
        {


            GoviewProjectApiSearcher searcher = new()
            {
                Page = page,
                Limit = limit
            };
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<GoviewProjectApiListVM>(passInit: true);
                vm.Searcher = searcher;
                var list = vm.GetSearchQuery();
                var goviewProjectData = list.Select(x => new
                {
                    id = x.ID.ToString(),
                    projectName = x.ProjectName,
                    state = x.State,
                    createTime = x.CreateTime,
                    createUserId = x.CreateUserId.ToString(),
                    isDelete = x.IsDelete,
                    indexImage = x.IndexImage,
                    remarks = x.Remarks
                });

                GoViewDataReturn goViewDataReturn = new()
                {
                    code = 200,
                    msg = "获取成功",
                    count = goviewProjectData.Count(),
                    data = goviewProjectData,
                };
                return Ok(goViewDataReturn);
            }
            else
            {
                GoViewDataReturn goViewDataReturn = new()
                {
                    code = 500,
                    msg = "获取失败",
                };
                return Ok(goViewDataReturn);
            }
        }


        //删除
        [ActionDescription("Sys.Delete")]
        [HttpDelete("delete")]
        public IActionResult Delete(string ids)
        {
            var vm = Wtm.CreateVM<GoviewProjectApiVM>(ids);
            vm.Entity.IsDelete = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(true);
                if (!ModelState.IsValid)
                {
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 500,
                        msg = "操作失败",
                    };
                    return Ok(goViewDataReturn);
                }
                else
                {
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 200,
                        msg = "操作成功",
                    };
                    return Ok(goViewDataReturn);
                }
            }

        }

        //修改
        [ActionDescription("Sys.Edit")]
        [HttpPost("edit")]
        public IActionResult Edit(GoviewProjectCURDVM data)
        {
            //var vm = Wtm.CreateVM<GoviewProjectApiVM>(data.id);
            var pid = Convert.ToInt32(data.id);
            var model = DC.Set<GoviewProject>().FirstOrDefault(x => x.ID == pid);

            model.IndexImage = data.indexImage;
            model.ProjectName = data.projectName != null ? data.projectName : model.ProjectName; 
            model.Remarks = data.remarks!=null? data.remarks: model.Remarks;
            DC.Set<GoviewProject>().Update(model);
            int x = DC.SaveChanges();
            
            if (x<0)
            {
                GoViewDataReturn goViewDataReturn = new()
                {
                    code = 500,
                    msg = "操作失败",
                };
                return Ok(goViewDataReturn);
            }
            else
            {
                GoViewDataReturn goViewDataReturn = new()
                {
                    code = 200,
                    msg = "操作成功",
                };
                return Ok(goViewDataReturn);
            }

        }

        //新增
        [ActionDescription("Sys.Create")]
        [HttpPost("create")]
        public IActionResult Add(GoviewProjectCURDVM data)
        {
            GoviewProject goviewProject = new()
            {
                ProjectName = data.projectName,
                IndexImage = data.indexImage,
                Remarks = data.remarks,
                CreateUserId = new Guid(Wtm.LoginUserInfo.UserId),
                CreateTime = DateTime.Now,
                IsDelete = -1,
                State = -1,
            };
            DC.Set<GoviewProject>().Add(goviewProject);
            int x = DC.SaveChanges();
            if (x > 0)
            {
                GoviewProjectData goviewProjectData = new()
                {
                    ProjectId = goviewProject.ID,
                    CreateTime = DateTime.Now,
                    CreateUserId = new Guid(Wtm.LoginUserInfo.UserId),
                };

                DC.Set<GoviewProjectData>().Add(goviewProjectData);
                int y = DC.SaveChanges();

                if (y>0)
                {
                    var temp = new
                    {
                        id = goviewProject.ID,
                        projectName = goviewProject.ProjectName,
                        state = goviewProject.State,
                        createTime = goviewProject.CreateTime,
                        createUserId = Wtm.LoginUserInfo.UserId,
                        isDelete = goviewProject.IsDelete,
                        indexImage = goviewProject.IndexImage,
                        Remarks = goviewProject.Remarks,
                    };
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 200,
                        msg = "操作成功",
                        data = temp
                    };
                    return Ok(goViewDataReturn);
                }
                else
                {
                    DC.Set<GoviewProject>().Remove(goviewProject);
                    DC.SaveChanges();
                    GoViewDataReturn goViewDataReturn1 = new()
                    {
                        code = 500,
                        msg = "操作失败",
                    };
                    return Ok(goViewDataReturn1);
                }

                
            }
            GoViewDataReturn goViewDataReturn2 = new()
            {
                code = 500,
                msg = "操作失败",
            };
            return Ok(goViewDataReturn2);




        }

        //发布
        [ActionDescription("Sys.publish")]
        [HttpPut("publish")]
        public IActionResult Publish(GoviewProjectCURDVM data)
        {
            var vm = Wtm.CreateVM<GoviewProjectApiVM>(data.id);
            vm.Entity.State = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(true);
                if (!ModelState.IsValid)
                {
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 500,
                        msg = "操作失败",
                    };
                    return Ok(goViewDataReturn);
                }
                else
                {
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 200,
                        msg = "操作成功",
                    };
                    return Ok(goViewDataReturn);
                }
            }

        }





















    }
}
