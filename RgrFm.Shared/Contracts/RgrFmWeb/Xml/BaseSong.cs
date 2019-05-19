using System.Xml.Serialization;

namespace RgrFm.Contracts.RgrFmWeb.Xml
{
    public abstract class BaseSong
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "start")]
        public string Start { get; set; }

        [XmlElement(ElementName = "artist")]
        public string Artist { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
    }
}
