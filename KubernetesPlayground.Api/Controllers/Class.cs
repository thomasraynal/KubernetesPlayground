using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KubernetesPlayground.Api.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }

    }
}
