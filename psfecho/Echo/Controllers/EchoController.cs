
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using static System.Net.Http.HttpResponseHeadersExtensions;
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
        public HttpResponseHeaders Get1()
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("session-id", "12345");
            resp.Headers.AddCookies(new List<CookieHeaderValue>() { cookie });

            return resp.Headers;
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
        public HttpResponseMessage Post2()
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("session-id", "12345");
            IEnumerable<CookieHeaderValue> lst = new List<CookieHeaderValue>();
            lst.Append(cookie);

            resp.Headers.AddCookies(lst);
            return resp;

        }
    }
}
