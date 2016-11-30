using System.Xml.Serialization;
using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class SSHConnectionModel
    {
        [XmlAttribute]
        public string Host { get; set; } = "localhost";
        [XmlAttribute]
        public int Port { get; set; } = 22;
        [XmlAttribute]
        public string Username { get; set; } = "root";
        [XmlAttribute]
        public string Password { get; set; } = "";
    }
}
