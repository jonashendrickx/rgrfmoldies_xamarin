using RgrFm.Contracts.RgrFmWeb.Xml;

namespace RgrFm.Models
{
    public class PlaylistItem
    {
        public string Artist { get; set; }

        public string Title { get; set; }

        public string Time { get; set; }

        public static PlaylistItem FromXmlContract(BaseSong contract)
        {
            if (contract == null) return null;

            return new PlaylistItem
            {
                Artist = contract.Artist,
                Title = contract.Title,
                Time = contract.Start
            };
        }
    }
}
