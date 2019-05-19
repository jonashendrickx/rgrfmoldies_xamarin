using RgrFm.Contracts.RgrFmWeb.Xml;
using System;
using System.IO;
using System.Xml.Serialization;

namespace RgrFm.Services
{
    public class XmlSerializerService
    {
        public static Playlist Deserialize(string content)
        {
            try
            {
                using (TextReader sr = new StringReader(content))
                {
                    var serializer = new XmlSerializer(typeof(Playlist));
                    var result = (Playlist)serializer.Deserialize(sr);
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
