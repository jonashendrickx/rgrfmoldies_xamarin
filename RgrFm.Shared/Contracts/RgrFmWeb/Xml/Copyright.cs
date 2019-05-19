using System.Xml.Serialization;

namespace RgrFm.Contracts.RgrFmWeb.Xml
{
    [XmlRoot(ElementName = "copyright")]
    public class Copyright
    {
        [XmlElement(ElementName = "copyright")]
        public string Description { get; set; }

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "licence")]
        public string Licence { get; set; }
    }
}
