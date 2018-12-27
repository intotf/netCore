using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasenApi.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasenApi.Controllers.v1
{
    /// <summary>
    /// 1.0 版本
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version}/[controller]/[action]")]
    public class EasenController : ControllerBase
    {
        /// <summary>
        /// 日志信息
        /// </summary>
        ILogger<EasenController> logger;

        /// <summary>
        /// 构造函数配置日志
        /// </summary>
        /// <param name="logger"></param>
        public EasenController(ILogger<EasenController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 添加人脸信息
        /// </summary>
        /// <param name="file">人脸图片</param>
        /// <param name="house">住户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Add(IFormFile file, [FromForm] House house)
        {
            this.logger.LogError($"{DateTime.Now} LogError 日志");
            this.logger.LogInformation($"{DateTime.Now} LogInformation 日志");
            this.logger.LogDebug($"{DateTime.Now} LogDebug 日志");
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
        /// 添加人脸信息
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
