using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using JavaNet.Mvn.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JavaNet.Mvn.Controllers
{
    [Route("/")]
    public class ApiController : Controller
    {
        [HttpGet("index.json")]
        [HttpHead("index.json")]
        public IActionResult Index()
        {
            return Ok(new ServiceIndex
            {
                Version = "3.0.0",
                Resources = new[]
                {
                    new ServiceResource(
                        this.MakeBaseUrl(UrlConstants.Package),
                        new[]
                        {
                            "PackagePublish/2.0.0",
                            "PackageBaseAddress/3.0.0"
                        }),
                    new ServiceResource(
                        this.MakeBaseUrl(UrlConstants.Search),
                        new[]
                        {
                            "SearchQueryService",
                            "SearchQueryService/3.0.0-beta",
                            "SearchQueryService/3.0.0-rc"
                        }),
                    new ServiceResource(
                        this.MakeBaseUrl(UrlConstants.Registrations),
                        new[]
                        {
                            "RegistrationsBaseUrl",
                            "RegistrationsBaseUrl/3.0.0-beta",
                            "RegistrationsBaseUrl/3.0.0-rc"
                        })
                }
            });
        }
    }
}