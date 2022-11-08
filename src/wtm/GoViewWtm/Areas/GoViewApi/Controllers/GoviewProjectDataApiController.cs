using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using GoViewWtm.ViewModel.GoViewApi.GoviewProjectDataVMs;
using GoViewWtm.Model.GoViewModel;
using GoViewWtm.ViewModel.GoViewApi;
using System.Text;
using System.Reflection.Metadata;
using Fare;
using Microsoft.CodeAnalysis;
using WalkingTec.Mvvm.Mvc.Binders;

namespace GoViewWtm.Controllers
{
    [Area("GoViewApi")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("项目数据")]
    [ApiController]
    [Route("api/goview/project")]
    public partial class GoviewProjectDataApiController : BaseApiController
    {
        //获取设计数据
        [Public]
        [ActionDescription("Sys.Get")]
        [HttpGet("getData")]
        public IActionResult Get(string projectId)
        {
            var pid = Convert.ToInt32(projectId);
            var model = DC.Set<GoviewProjectData>().FirstOrDefault(x => x.ProjectId == pid);
            var pro = DC.Set<GoviewProject>().FirstOrDefault(x => x.ID == pid);
            if (pro!=null && model!=null)
            {              
                

                if (model.ContentData == null)
                {
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 200,
                        msg = "无数据",
                        data = null,
                    };
                    return Ok(goViewDataReturn);
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
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 200,
                        msg = "获取成功",
                        data = temp,
                    };
                    return Ok(goViewDataReturn);
                }

                
            }         
            GoViewDataReturn goViewDataReturn1 = new()
            {
                code = 500,
                msg = "获取失败",
            };
            return Ok(goViewDataReturn1);
        }
        //保存
        [ActionDescription("Sys.Edit")]
        [HttpPost("save/data")]
        [StringNeedLTGT]
        public IActionResult Edit([FromForm] GoviewProjectDataCURDVM data)
        {

            var model = DC.Set<GoviewProjectData>().FirstOrDefault(x => x.ProjectId == data.projectId);
            if (model!=null)
            {
                model.ContentData = Encoding.ASCII.GetBytes(data.content);
                DC.Set<GoviewProjectData>().Update(model);
                int x = DC.SaveChanges();
                if (x>0)
                {
                    GoViewDataReturn goViewDataReturn = new()
                    {
                        code = 200,
                        msg = "数据保存成功",
                    };
                    return Ok(goViewDataReturn);
                }             
            }
            GoViewDataReturn goViewDataReturn1 = new()
            {
                code = 500,
                msg = "数据保存失败",
            };
            return Ok(goViewDataReturn1);

        }

       
        

		

        


    }
}
