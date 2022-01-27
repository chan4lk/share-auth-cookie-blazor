using System.Xml.Serialization;

namespace AuthDemo.Blazor.Shared
{
    [XmlRoot(ElementName = "UserModel", Namespace = "http://tempuri.org/")]
    public class UserModel
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "IsAuthenticated")]
        public bool IsAuthenticated { get; set; }
    }
}
