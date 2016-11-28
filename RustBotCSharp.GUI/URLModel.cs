using System.Text;
using System.Xml.Serialization;
using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class URLModel
    {
        public URLModel() { }

        public URLModel(string protocol = "tcp", string ip = "localhost", uint port = 13370)
        {
            Protocol = protocol;
            IP = ip;
            Port = port;
        }

        [XmlAttribute]
        public string Protocol { get; set; } = "tcp";

        [XmlAttribute]
        public string IP { get; set; } = "localhost";

        [XmlAttribute]
        public uint Port { get; set; } = 13370;

        public string URL()
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(Protocol);
            urlBuilder.Append("://");
            urlBuilder.Append(IP);
            urlBuilder.Append(":");
            urlBuilder.Append(Port);
            return urlBuilder.ToString();
        }
    }
}
