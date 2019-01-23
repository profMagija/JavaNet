using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JavaNet.Mvn.Model;
using Microsoft.AspNetCore.Mvc;
using static JavaNet.Mvn.UrlConstants;

namespace JavaNet.Mvn.Controllers
{
    [Route(Registrations)]
    public class RegistrationsController : Controller
    {
        [HttpGet("{id}/index.json")]
        public async Task<IActionResult> Index(
            [FromQuery] string id
            )
        {
            var url = Helpers.MakeMavenUrl(id);
            var (group, artifact) = Helpers.MakeMavenName(id);

            var metadataResponse = await WebRequest.CreateHttp(url + "/maven-metadata.xml").GetResponseAsync();
            var metadata = Helpers.XmlDeserialize<MvnMetadata>(metadataResponse.GetResponseStream());
            var latestVersion = metadata.Versioning.Latest;

            var latestPomResponse =
                await WebRequest.CreateHttp($"{url}/{latestVersion}/{artifact}-{latestVersion}.pom").GetResponseAsync();
            var latestPom = Helpers.XmlDeserialize<MvnPomProject>(latestPomResponse.GetResponseStream());

            return Json(new RegistrationItem
            {
                Count = 1,
                Items = new[]
                {
                    new RegistrationPage
                    {
                        Id = this.MakeBaseUrl(
                            $"{Registrations}/{id}/index.json#page/{latestVersion}/{latestVersion}"),
                        Count = 1,
                        Items = new[]
                        {
                            new RegistrationLeaf
                            {
                                Id = this.MakeBaseUrl($"{Registrations}/{id}/{latestVersion}.json"),
                                PackageContent =
                                    this.MakeBaseUrl(
                                        $"{Registrations}/{id}/{latestVersion}/{id}.{latestVersion}.nupkg"),
                                Registration = this.MakeBaseUrl($"{Registrations}/{id}/index.json"),
                                CatalogEntry = new CatalogEntry
                                {
                                    Authors = latestPom.GroupId ?? latestPom.Parent.GroupId,
                                    DependencyGroups = new[]
                                    {
                                        new DependencyGroup
                                        {
                                            Dependencies = latestPom.Dependencies.Dependency
                                                .Select(dep =>
                                                {
                                                    var nugetName = Helpers.MakeNugetName(dep.ArtifactId, dep.GroupId);
                                                    return new Dependency
                                                    {
                                                        DependencyId = nugetName,
                                                        Registration =
                                                            this.MakeBaseUrl(
                                                                $"{Registrations}/{nugetName.ToLowerInvariant()}/index.json")
                                                    };
                                                }).ToArray()
                                        }
                                    },
                                    Description = latestPom.Description,
                                    CatalogEntryId = id,
                                    Version = latestVersion
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}