using System;
using Microsoft.AspNetCore.Mvc;

namespace JavaNet.Mvn.Controllers
{
    [Route(UrlConstants.Package)]
    public class PackageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}