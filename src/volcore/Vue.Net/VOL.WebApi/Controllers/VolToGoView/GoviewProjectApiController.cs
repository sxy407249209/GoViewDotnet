using GoView;
using GoView.IRepositories;
using GoView.IServices;
using GoView.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOL.Core.BaseProvider;
using VOL.Core.Controllers.Basic;
using VOL.Core.Extensions;
using VOL.Core.Filters;
using VOL.Core.ManageUser;
using VOL.Entity.DomainModels;
using VOL.Entity.DomainModels.GoviewVol;
using VOL.WebApi.Controllers.GoView;
using Sys = System.IO;

namespace VOL.WebApi.Controllers.VolToGoView
{
    [Route("api/goview/project")]
    [ApiController, JWTAuthorize()]
    public class GoviewProjectApiController : ApiBaseController<IGoviewprojectsService>
    {
        private IGoviewprojectsRepository _goviewprojectsRepository;//访问业务代码
        private readonly IGoviewprojectdatasRepository _repository;//访问数据库
        [ActivatorUtilitiesConstructor]
        public GoviewProjectApiController(
            IGoviewprojectsService service,
            IGoviewprojectsRepository  goviewprojectsRepository,
            IGoviewprojectdatasRepository dbRepository
        )
        : base(service)
        {
            _goviewprojectsRepository = goviewprojectsRepository;
            _repository = dbRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(int page = 1, int limit = 10)
        {
            var goviewProjectData = await _goviewprojectsRepository.DbContext.Set<Goviewprojects>()
                .Where(x=>x.IsDelete==-1)
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
            if (x>0)
            {
                goViewDataReturn.code = 200;
                goViewDataReturn.msg = "操作成功";
                return goViewDataReturn;
            }
            return goViewDataReturn;
        }

        [HttpPost("edit")]
        public async Task<GoViewDataReturn> Edit([FromBody] GoviewProjectCURDVM data)
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
        public async Task<GoViewDataReturn> Add([FromBody]GoviewProjectCURDVM data)
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

                Goviewprojectdatas goviewProjectData = new Goviewprojectdatas()
                {
                    ProjectId = goviewProject.Id,
                    CreateTime = DateTime.Now,
                    CreateUserId = UserContext.Current.UserId,
                };

                await  _repository.DbContext.Set<Goviewprojectdatas>().AddAsync(goviewProjectData);
                int y = await _repository.DbContext.SaveChangesAsync();

                if (y>0)
                {
                    var temp = new
                    {
                        id = goviewProject.Id,
                        projectName = goviewProject.ProjectName,
                        state = goviewProject.State,
                        createTime = goviewProject.CreateTime,
                        createUserId = UserContext.Current.UserId,
                        isDelete = goviewProject.IsDelete,
                        indexImage = goviewProject.IndexImage,
                        Remarks = goviewProject.Remarks,
                    };

                    goViewDataReturn.code = 200;
                    goViewDataReturn.msg = "操作成功";
                    goViewDataReturn.data = temp;
                    return goViewDataReturn;
                }

                return goViewDataReturn;



            }
        }

        [HttpPut("publish")]
        public async Task<GoViewDataReturn> Publish([FromBody] GoviewProjectCURDVM data)
        {
            var pid = Convert.ToInt32(data.id);
            var model = await _goviewprojectsRepository.DbContext.Set<Goviewprojects>()
                .FirstOrDefaultAsync(x => x.Id == pid);
            model.State = data.Statu;
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

        public override IActionResult Upload(IEnumerable<IFormFile> fileInput)
        {
            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0)
            {
                var temp1 = new
                {
                    code = 500
                };

                return Ok(temp1);
            }


            string filePath = $"Upload/govew/";
            string fullPath = filePath.MapPath(true);
            try
            {
                if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

                string fileName = files[0].FileName;
                var filenamearr = fileName.Split(".");
                using var stream = new FileStream(fullPath + fileName, FileMode.Create);
                files[0].CopyTo(stream);
                var temp = new
                {
                    code = 200,
                    data = new GoviewFileInfo
                    {
                        id = DateTime.Now.ToString(),
                        fileName = fileName,
                        bucketName = null,
                        fileSize = files[0].Length,
                        fileSuffix = filenamearr[1],
                        createUserId = null,
                        createUserName = null,
                        createTime = DateTime.Now,
                        updateUserId = null,
                        updateUserName = null,
                        updateTime = null
                    }
                };
                return Ok(temp);

            }
            catch (Exception)
            {

                var temp1 = new
                {
                    code = 500
                };

                return Ok(temp1);
            }
        }


      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> 这是的id是实际上文件名</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("getImages/{id}")]
        public IActionResult GetImages(string id)
        {
            string filePath = $"Upload/govew/";
            string fullPath = filePath.MapPath(true) + id;

            //var b = Directory.Exists(fullPath);


            var b = Sys.File.Exists(fullPath);

            if (b==true)
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);
                byte[] fileArray = r.ReadBytes((int)fs.Length);
                fs.Dispose();
                var response = File(fileArray, "image/jpeg");
                return response;
            }

            GoViewDataReturn goViewDataReturn1 = new GoViewDataReturn()
            {
                code = 500,
                msg = "获取图片失败",
            };
            return Ok(goViewDataReturn1);




        }


    }
}
