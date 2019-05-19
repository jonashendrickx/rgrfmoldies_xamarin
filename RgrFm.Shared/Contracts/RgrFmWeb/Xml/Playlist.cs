using System.Xml.Serialization;

namespace RgrFm.Contracts.RgrFmWeb.Xml
{
    [XmlRoot(ElementName = "playlist")]
    public class Playlist
    {
        [XmlElement(ElementName = "copyright")]
        public Copyright Copyright { get; set; }

        [XmlElement(ElementName = "station_info")]
        public StationInfo StationInfo { get; set; }

        [XmlElement(ElementName = "song_info")]
        public SongInfo SongInfo { get; set; }

        [XmlElement(ElementName = "player_info")]
        public PlayerInfo PlayerInfo { get; set; }
    }
}
