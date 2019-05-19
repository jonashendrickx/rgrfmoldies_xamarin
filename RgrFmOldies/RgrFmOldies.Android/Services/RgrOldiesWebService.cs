using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using RgrFm.Models;
using RgrFm.Services;

namespace RgrFmOldies.Droid.Services
{
    public class RgrOldiesWebService
    {
        public static async Task<PlaylistFeed> GetPlaylistAsync()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://www.rgrfm.be/rgr_classic_hits/apps/playlist.php"));
                request.ContentType = "application/xhtml+xml";
                request.Method = "GET";

                using (var response = await request.GetResponseAsync())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        string content;
                        using (var reader = new StreamReader(stream))
                        {
                            content = reader.ReadToEnd();
                        }

                        var result = XmlSerializerService.Deserialize(content);
                        return PlaylistFeed.FromXmlContract(result);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}