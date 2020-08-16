
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Echo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {
        private readonly ILogger<EchoController> _logger;

        public EchoController(ILogger<EchoController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ContentResult Get()
        {
            var res = new Dictionary<string, string>();
            var str = "";
            foreach (var (headerName, values) in Request.Headers)
            {
                res.Add($"[Header] {headerName}", values.ToString());
                str = str + "<div item=";
                str += values.ToString();
                str += ">";
                str += headerName;
                str += "</div>";
            }

            res.Add("[Request] Host", Request.Host.ToString());
            res.Add("[Request] Scheme", Request.Scheme);

            return new ContentResult
            {
                ContentType = "text/html",
                Content = str
            };
        }

        [HttpGet("~/index")]
        public ContentResult Get1()
        {

            return new ContentResult
            {
                ContentType = "text/html",
                Content = "Calling multiple requests to this endpoint"
            };
        }


        [HttpPost]
        public IDictionary<string, string> Post(IDictionary<string, string> body)
        {
            var res = new Dictionary<string, string>(body);

            foreach (var (headerName, values) in Request.Headers)
            {
                res.Add($"[Header] {headerName}", values.ToString());
            }

            return res;
        }

        [HttpPost("~/index")]
        public ContentResult Post2()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = "Calling multiple requests to this endpoint"
            };

        }
    }
}
