using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Transactions;
using System.Xml;
using System.Xml.Serialization;
using Mono.Cecil;

namespace JavaNet.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            var compiled = CompileMaven(new MavenCoordinate("com.google.cloud", "google-cloud-core"));
        }

        private static readonly Dictionary<MavenCoordinate, CompiledAssembly[]> Cache =
            new Dictionary<MavenCoordinate, CompiledAssembly[]>();

        private static Stream HttpGet(string url)
        {
            System.Console.WriteLine("  GET {0}", url);
            return WebRequest.CreateHttp(url).GetResponse().GetResponseStream();
        }

        public static CompiledAssembly[] CompileMaven(MavenCoordinate cord)
        {
            
            System.Console.WriteLine("Starting {0}", cord);
            
            if (cord.Version == null)
            {
                var metadata = XmlDeserialize<Metadata>(HttpGet(
                    $"http://central.maven.org/maven2/{cord.GroupId.Replace('.', '/')}/{cord.ArtefactId}/maven-metadata.xml"));

                cord = new MavenCoordinate(cord.GroupId, cord.ArtefactId, metadata.Versioning.Release);
            }
            
            if (Cache.TryGetValue(cord, out var cached))
            {
                if (cached == null)
                    throw new NotImplementedException("Don't know what to do with cyclic dependencies");
                return cached;
            }

            Cache[cord] = null;

            var pom = XmlDeserialize<Project>(HttpGet(
                $"http://central.maven.org/maven2/{cord.GroupId.Replace('.', '/')}/{cord.ArtefactId}/{cord.Version}/{cord.ArtefactId}-{cord.Version}.pom"));

            var compiledDeps = new List<CompiledAssembly>();

            foreach (var dependency in pom.Dependencies?.Dependency ?? Enumerable.Empty<Dependency>())
            {
                var depCord = new MavenCoordinate(dependency.GroupId, dependency.ArtifactId, dependency.Version);
                compiledDeps.AddRange(CompileMaven(depCord));
            }

            var jar = JarReader.BuildJarFile(WebRequest
                .CreateHttp(
                    $"http://central.maven.org/maven2/{cord.GroupId.Replace('.', '/')}/{cord.ArtefactId}/{cord.Version}/{cord.ArtefactId}-{cord.Version}.jar")
                .GetResponse().GetResponseStream());
            
            var jab = new JavaAssemblyBuilder();
            
            foreach (var dep in compiledDeps)
            {
                jab.RegisterReferenceAssembly(dep.Loaded);
            }

            var built = jab.BuildAssembly(cord.ArtefactId, SanitizeVersion(cord.Version), jar);
            compiledDeps.Add(new CompiledAssembly(built));

            System.Console.WriteLine("Finished {0}:{1}:{2}", cord.GroupId, cord.ArtefactId, cord.Version);

            return Cache[cord] = compiledDeps.ToArray();
        }
        
        public static T XmlDeserialize<T>(Stream s)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(s);
        }

        private static Version SanitizeVersion(string version)
        {
            var i = 0;
            var parts = new int[4];
            var pindex = 0;
            while (i < version.Length && pindex < parts.Length)
            {
                var num = 0;
                while (i < version.Length && char.IsDigit(version[i]))
                {
                    num = num * 10 + (version[i] - '0');
                    i++;
                }

                parts[pindex++] = num;

                if (pindex == 4)
                    break;
                
                while (i < version.Length && !char.IsDigit(version[i]))
                {
                    i++;
                }
            }
            
            return new Version(parts[0], parts[1], parts[2], parts[3]);
        }
    }

    public class MavenCoordinate
    {
        public string GroupId { get; }
        public string ArtefactId { get; }
        public string Version { get; }

        public MavenCoordinate(string groupId, string artefactId, string version = null)
        {
            GroupId = groupId;
            ArtefactId = artefactId;
            Version = version;
        }

        public bool Matches(MavenCoordinate other)
        {
            return string.Equals(GroupId, other.GroupId) &&
                   string.Equals(ArtefactId, other.ArtefactId) &&
                   (string.Equals(Version, other.Version) ||
                    string.IsNullOrEmpty(Version) ||
                    string.IsNullOrEmpty(other.Version));
        }

        protected bool Equals(MavenCoordinate other)
        {
            return string.Equals(GroupId, other.GroupId) && string.Equals(ArtefactId, other.ArtefactId) && string.Equals(Version, other.Version);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MavenCoordinate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (GroupId != null ? GroupId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ArtefactId != null ? ArtefactId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{GroupId}:{ArtefactId}:{Version ?? "*"}";
        }

        public static bool operator ==(MavenCoordinate left, MavenCoordinate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MavenCoordinate left, MavenCoordinate right)
        {
            return !Equals(left, right);
        }
    }

    public class CompiledAssembly
    {
        public AssemblyDefinition Definition { get; }
        
        public Assembly Loaded { get; }

        public CompiledAssembly(AssemblyDefinition definition)
        {
            Definition = definition;
            using (var mstr = new MemoryStream())
            {
                Definition.Write(mstr);
                Loaded = Assembly.Load(mstr.ToArray());
            }
        }
    }
}