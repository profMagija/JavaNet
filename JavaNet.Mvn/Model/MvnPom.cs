using System.Collections.Generic;
using System.Xml.Serialization;

namespace JavaNet.Mvn.Model
{
	[XmlRoot(ElementName="parent", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomParent {
		[XmlElement(ElementName="groupId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string GroupId { get; set; }
		[XmlElement(ElementName="artifactId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string ArtifactId { get; set; }
		[XmlElement(ElementName="version", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Version { get; set; }
	}

	[XmlRoot(ElementName="properties", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomProperties {
		[XmlElement(ElementName="site.installationModule", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string SiteInstallationModule { get; set; }
	}

	[XmlRoot(ElementName="dependency", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomDependency {
		[XmlElement(ElementName="groupId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string GroupId { get; set; }
		[XmlElement(ElementName="artifactId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string ArtifactId { get; set; }
		[XmlElement(ElementName="scope", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Scope { get; set; }
		[XmlElement(ElementName="exclusions", Namespace="http://maven.apache.org/POM/4.0.0")]
		public MvnPomExclusions Exclusions { get; set; }
	}

	[XmlRoot(ElementName="exclusion", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomExclusion {
		[XmlElement(ElementName="groupId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string GroupId { get; set; }
		[XmlElement(ElementName="artifactId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string ArtifactId { get; set; }
	}

	[XmlRoot(ElementName="exclusions", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomExclusions {
		[XmlElement(ElementName="exclusion", Namespace="http://maven.apache.org/POM/4.0.0")]
		public MvnPomExclusion Exclusion { get; set; }
	}

	[XmlRoot(ElementName="dependencies", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomDependencies {
		[XmlElement(ElementName="dependency", Namespace="http://maven.apache.org/POM/4.0.0")]
		public List<MvnPomDependency> Dependency { get; set; }
	}

	[XmlRoot(ElementName="project", Namespace="http://maven.apache.org/POM/4.0.0")]
	public class MvnPomProject {
		[XmlElement(ElementName="modelVersion", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string ModelVersion { get; set; }
		[XmlElement(ElementName="artifactId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string ArtifactId { get; set; }
		[XmlElement(ElementName="groupId", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string GroupId { get; set; }
		[XmlElement(ElementName="version", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Version { get; set; }
		[XmlElement(ElementName="packaging", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Packaging { get; set; }
		[XmlElement(ElementName="name", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Name { get; set; }
		[XmlElement(ElementName="url", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Url { get; set; }
		[XmlElement(ElementName="description", Namespace="http://maven.apache.org/POM/4.0.0")]
		public string Description { get; set; }
		[XmlElement(ElementName="parent", Namespace="http://maven.apache.org/POM/4.0.0")]
		public MvnPomParent Parent { get; set; }
		[XmlElement(ElementName="properties", Namespace="http://maven.apache.org/POM/4.0.0")]
		public MvnPomProperties Properties { get; set; }
		[XmlElement(ElementName="dependencies", Namespace="http://maven.apache.org/POM/4.0.0")]
		public MvnPomDependencies Dependencies { get; set; }
		[XmlAttribute(AttributeName="xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName="xsi", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlAttribute(AttributeName="schemaLocation", Namespace="http://www.w3.org/2001/XMLSchema-instance")]
		public string SchemaLocation { get; set; }
	}

}
