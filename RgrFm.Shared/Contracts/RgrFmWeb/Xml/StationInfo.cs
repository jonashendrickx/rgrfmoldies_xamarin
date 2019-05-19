using System.Xml.Serialization;

namespace RgrFm.Contracts.RgrFmWeb.Xml
{
    [XmlRoot(ElementName = "station_info")]
    public class StationInfo
    {
        [XmlElement(ElementName = "station_name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "station_website")]
        public string Website { get; set; }

        [XmlElement(ElementName = "station_stream")]
        public string Stream { get; set; }
    }
}
