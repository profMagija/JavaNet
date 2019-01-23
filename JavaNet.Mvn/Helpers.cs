using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace JavaNet.Mvn
{
    public static class Helpers
    {
        public static string MakeNugetName(string group, string artefact)
        {
            return group + "." + artefact;
        }

        public static string MakeNugetVersion(string mvnVersion)
        {
            return mvnVersion;
        }

        public static async Task<string> ReadStream(Stream stream)
        {
            var sb = new StringBuilder();
            var buf = new byte[8192];
            int count;
            do
            {
                count = await stream.ReadAsync(buf, 0, buf.Length);
                if (count != 0)
                {
                    var tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            } while (count > 0);

            return sb.ToString();
        }

        public static Uri MakeBaseUrl(this ControllerBase api, string s)
        {
            if (Uri.TryCreate(api.Request.Scheme + "://" + api.Request.Host.ToUriComponent() + api.Request.PathBase + s, UriKind.Absolute, out var res))
            {
                Console.WriteLine(res.AbsoluteUri);
                return res;
            }

            throw new ArgumentException("asdkjhfadksfjhaldkjf");
        }

        public static string MakeMavenUrl(string nugetId)
        {
            return "http://central.maven.org/maven2/" + nugetId.Replace('.', '/');
        }

        public static T XmlDeserialize<T>(Stream s)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(s);
        }

        public static (string group, string artifact) MakeMavenName(string nugetId)
        {
            var split = nugetId.Split('.');
            return (string.Join(".", split.SkipLast(1)), split.Last());
        }
    }

    public static class UrlConstants
    {
        public const string Package = "/package";
        public const string Search = "/search";
        public const string Registrations = "/registration";
        public const string PackageBaseUrl = "/package";
    }
}