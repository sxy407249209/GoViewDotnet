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

namespace GoViewWtm.Controllers
{
    [Area("GoViewApi")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("项目信息")]
    [ApiController]
    [Route("api/goview/project")]
	public partial class GoviewProjectApiController : BaseApiController
    {
        [ActionDescription("Sys.Search")]
        [HttpGet("list")]
		public IActionResult Search(int page=1,int limit = 10)
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

        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public GoviewProjectApiVM Get(string id)
        {
            var vm = Wtm.CreateVM<GoviewProjectApiVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(GoviewProjectApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(GoviewProjectApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

		[HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<GoviewProjectApiBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(GoviewProjectApiSearcher searcher)
        {
            var vm = Wtm.CreateVM<GoviewProjectApiListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<GoviewProjectApiListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<GoviewProjectApiImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(GoviewProjectApiImportVM vm)
        {
            if (vm!=null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm?.EntityList?.Count ?? 0);
            }
        }


    }
}
