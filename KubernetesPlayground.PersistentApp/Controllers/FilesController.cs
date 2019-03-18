using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace KubernetesPlayground.PersistentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IHttpContextAccessor _context;
        private string _folder = "files";

        public FilesController(IHttpContextAccessor context)
        {
            Directory.CreateDirectory(_folder);
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Index()
        {
            return Ok(Directory.GetFiles(_folder).Select(path => Path.GetFileName(path)));
        }

        [HttpPut("{name}")]
        public ActionResult Put(string name, [FromBody] dynamic value)
        {
            System.IO.File.AppendAllText(Path.Combine(_folder, name), (value as JObject).ToString());

            return Ok(new { host = _context.HttpContext.Request.Host.Host, file = name });
        }

    }
}
