using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Support.FileHandlers;
using WalkingTec.Mvvm.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;
using NPOI.HPSF;
using GoViewWtm.ViewModel.GoViewApi;

namespace GoViewWtm.Areas.GoViewApi.Controllers
{
    [AuthorizeJwtWithCookie]
    [Route("api/goview/project")]
    [ApiController]
    public class GoViewFileController : BaseApiController
    {
        [Route("upload")]
        public async Task<IActionResult> Upload([FromServices] WtmFileProvider fp, string sm = null, string groupName = null, string subdir = null, string extra = null, string csName = null)
        {
            var FileData = Request.Form.Files[0];
            var chk = await DC.Set<FileAttachment>().AsNoTracking().FirstOrDefaultAsync(x => x.FileName == FileData.FileName);
            if (chk != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                FileData.OpenReadStream().CopyTo(memoryStream);
                chk.FileData = memoryStream.ToArray();
                DC.Set<FileAttachment>().Update(chk);
                int x = await DC.SaveChangesAsync();
                if (x > 0)
                {
                    var temp = new
                    {
                        code = 200,
                        data = new GoviewFileInfo
                        {
                            id = chk.ID.ToString(),
                            fileName = chk.FileName,
                            bucketName = null,
                            fileSize = chk.Length,
                            fileSuffix = chk.FileExt,
                            createUserId = null,
                            createUserName = null,
                            createTime = chk.UploadTime,
                            updateUserId = null,
                            updateUserName = null,
                            updateTime = null

                        }
                    };
                    return Ok(temp);
                }

                var temp1 = new
                {
                    code = 500
                };

                return Ok(temp1);


            }
            else
            {
                var file = fp.Upload(FileData.FileName, FileData.Length, FileData.OpenReadStream(), groupName, subdir, extra, sm, Wtm.CreateDC(cskey: csName));

                var temp = new
                {
                    ode = 200,
                    data = new GoviewFileInfo
                    {
                        id = file.GetID().ToString(),
                        fileName = file.FileName,
                        bucketName = null,
                        fileSize = file.Length,
                        fileSuffix = file.FileExt,
                        createUserId = null,
                        createUserName = null,
                        createTime = file.UploadTime,
                        updateUserId = null,
                        updateUserName = null,
                        updateTime = null

                    }
                };

                return Ok(temp);
                
            }

            
           
        }
    }
}
