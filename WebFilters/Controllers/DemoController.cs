using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebFilters.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetId(string id)
        {
            return id;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ActionResult<int> PostId(int id)
        {
            return id;
        }


        /// <summary>
        /// 以 FromBody 提交
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> PostFromBody([FromBody] DemoModel value)
        {
            return value.Title;
        }

        /// <summary>
        /// 以 FromForm 提交
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> PostFromForm([FromForm] DemoModel value)
        {
            return value.Title;
        }
    }

    /// <summary>
    /// 测试模型
    /// </summary>
    public class DemoModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
