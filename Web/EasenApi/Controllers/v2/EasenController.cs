using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasenApi.Controllers.v2
{
    /// <summary>
    /// 2.1 版本
    /// </summary>
    [ApiController]
    [ApiVersion("2.1")]
    [Route("api/v{api-version:apiVersion}/[controller]/[action]")]
    public class EasenController : ControllerBase
    {
        /// <summary>
        /// 添加人脸信息
        /// </summary>
        /// <param name="file">人脸图片</param>
        /// <param name="faceData">人脸特征数据</param>
        /// <param name="house">住户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Add(IFormFile file, IFormFile faceData, [FromForm] House house)
        {
            var resPath = "Res/";
            var fileName = System.Guid.NewGuid().ToString(); //随机生成新的文件名
            if (file != null)
            {
                string fileExt = Path.GetExtension(file.FileName); //文件扩展名
                fileName += fileExt; //随机生成新的文件名
                var filePath = resPath + fileName;
                if (!Directory.Exists(resPath))
                {
                    Directory.CreateDirectory(resPath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var faceDataName = System.Guid.NewGuid().ToString();
            if (faceData != null)
            {
                string fileExt = Path.GetExtension(faceData.FileName); //文件扩展名
                faceDataName += fileExt; //随机生成新的文件名
                var filePath = resPath + faceDataName;
                if (!Directory.Exists(resPath))
                {
                    Directory.CreateDirectory(resPath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await faceData.CopyToAsync(stream);
                }
            }


            return new JsonResult(new
            {
                state = true,
                data = new
                {
                    faceDataName = faceDataName,
                    fileName = fileName
                }
            });
        }

        /// <summary>
        /// 更新人脸信息
        /// </summary>
        /// <param name="file">人脸图片</param>
        /// <param name="house">住户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Update(IFormFile file, [FromForm] House house)
        {
            var files = Request.Form.Files;
            var newFileName = System.Guid.NewGuid().ToString(); //随机生成新的文件名
            var resPath = "Res/";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    newFileName = newFileName + fileExt;
                    var filePath = resPath + newFileName;
                    if (!Directory.Exists(resPath))
                    {
                        Directory.CreateDirectory(resPath);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return new JsonResult(new { state = true, data = newFileName });
        }

        /// <summary>
        /// 删除人脸信息
        /// </summary>
        /// <param name="mobile">手机号/帐号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Delete(string mobile)
        {
            return new JsonResult(new { state = true, data = mobile });
        }
    }
}