using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using RgrFmOldies.Droid.Models;

namespace RgrFmOldies.Droid.Services
{
    public class RgrOldiesWebService
    {
        public const string ArtistIdentifier = "Artist: ";
        public const string TitleIdentifier = "Title: ";

        public static async Task<PlaylistItem> GetPlaylistAsync()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://oldiesradiorgr.be/scripts/gplaylist.php"));
                request.Method = "GET";
                request.UserAgent = "RGR Oldies (Android)";
                
                using (var response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        string htmlString;
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            htmlString = reader.ReadToEnd();
                        }

                        var playlistItem = new PlaylistItem();

                        var artistIndex = htmlString.IndexOf(ArtistIdentifier) + ArtistIdentifier.Length;
                        playlistItem.Artist = GetValue(htmlString.Substring(artistIndex, htmlString.Length - artistIndex));

                        var titleIndex = htmlString.IndexOf(TitleIdentifier) + TitleIdentifier.Length;
                        playlistItem.Title = GetValue(htmlString.Substring(titleIndex, htmlString.Length - titleIndex));

                        return playlistItem;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        private static string GetValue(string content)
        {
            var stopIndex = 0;
            for (int i = 0; i < content.Length && stopIndex == 0; i++)
            {
                if (content[i] == '<') stopIndex = i;
            }

            return content.Substring(0, stopIndex);
        }
    }
}