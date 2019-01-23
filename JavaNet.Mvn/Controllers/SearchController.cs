using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JavaNet.Mvn.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JavaNet.Mvn.Controllers
{
    [Route(UrlConstants.Search)]
    public class SearchController : Controller
    {
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> Index(
            [FromQuery] string q = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 10,
            [FromQuery] bool prerelease = false,
            [FromQuery] string semVerLevel = "1.0.0"
            )
        {
            var mvnSearch = await WebRequest.CreateHttp(new Uri(
                string.Format(
                    "https://search.maven.org/solrsearch/select?q={0}&rows={1}&wt=json&start={2}",
                    q, take, skip
                ))).GetResponseAsync();


            var mvnResult =
                JsonConvert.DeserializeObject<MvnSearchResult>(await Helpers.ReadStream(mvnSearch.GetResponseStream()));

            var response = new SearchResults
            {
                TotalHits = mvnResult.Response.NumFound,
                Data = mvnResult.Response.Docs
                    .Select(doc =>
                    {
                        var nugetName = Helpers.MakeNugetName(doc.Group, doc.Artifact);
                        var nugetVersion = Helpers.MakeNugetVersion(doc.LatestVersion);
                        return new SearchResult
                        {
                            Id = this.MakeBaseUrl("registration/" + nugetName + "/index.json"),
                            Type = "Package",
                            DatumId = nugetName,
                            Version = nugetVersion,
                            Versions = new[]
                            {
                                new MvnVersion
                                {
                                    Id = this.MakeBaseUrl("resistration/" + nugetName + "/" + nugetVersion),
                                    VersionVersion = nugetVersion,
                                    Downloads = 15
                                }
                            }
                        };
                    })
                    .ToArray()
            };

            return Json(response);
        }
    }
}