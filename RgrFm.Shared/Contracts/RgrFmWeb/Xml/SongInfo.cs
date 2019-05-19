using System.Xml.Serialization;

namespace RgrFm.Contracts.RgrFmWeb.Xml
{
    [XmlRoot(ElementName = "song_info")]
    public class SongInfo
    {
        [XmlElement(ElementName = "song1")]
        public PreviousSong Previous { get; set; }

        [XmlElement(ElementName = "song2")]
        public CurrentSong Current { get; set; }

        [XmlElement(ElementName = "song3")]
        public NextSong Next { get; set; }
    }
}
