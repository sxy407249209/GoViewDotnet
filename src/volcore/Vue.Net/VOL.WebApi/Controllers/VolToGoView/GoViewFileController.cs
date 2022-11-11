using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Collections.Generic;
using System.IO;
using System;
using VOL.Core.Extensions;
using VOL.Core.Filters;
using VOL.Core.Utilities;
using GoView;
using Microsoft.AspNetCore.Authorization;
using static System.Net.WebRequestMethods;

namespace VOL.WebApi.Controllers.VolToGoView
{
    [Route("api/goview/project")]
    [ApiController, JWTAuthorize()]
    public class GoViewFileController : Controller
    {
        [HttpPost]
        [Route("upload")]
        public IActionResult Upload(List<Microsoft.AspNetCore.Http.IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                var temp1 = new
                {
                    code = 500
                };

                return Ok(temp1);
            }


            string filePath = $"Upload/govew/";
            string fullPath = filePath.MapPath(true);
            int i = 0;
            try
            {
                if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

                string fileName = files[0].FileName;
                var filenamearr = fileName.Split(".");
                using var stream = new FileStream(fullPath + fileName, FileMode.Create);
                files[i].CopyTo(stream);
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
            if (Directory.Exists(fullPath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
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
