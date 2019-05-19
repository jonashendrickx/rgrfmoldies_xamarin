using System.Xml.Serialization;

namespace RgrFm.Contracts.RgrFmWeb.Xml
{
    [XmlRoot(ElementName = "player_info")]
    public class PlayerInfo
    {
        [XmlElement(ElementName = "callmeback")]
        public string CallMeBack { get; set; }
    }
}
