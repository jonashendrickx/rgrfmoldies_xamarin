using System.Collections.Generic;
using RgrFm.Contracts.RgrFmWeb.Xml;

namespace RgrFm.Models
{
    public class PlaylistFeed
    {
        public List<PlaylistItem> Playlist { get; set; }

        public static PlaylistFeed FromXmlContract(Playlist contract)
        {
            if (contract == null) return null;

            return new PlaylistFeed
            {
                Playlist = new List<PlaylistItem>
                {
                    PlaylistItem.FromXmlContract(contract.SongInfo.Previous),
                    PlaylistItem.FromXmlContract(contract.SongInfo.Current),
                    PlaylistItem.FromXmlContract(contract.SongInfo.Next),
                }
            };
        }
    }
}

