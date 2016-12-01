using System;
using System.IO;
using System.Xml.Serialization;
using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class CommunicationsModel
    {
        [XmlIgnoreAttribute]
        public string XmlFilename { get; set; } = "";

        public CommunicationsModel() { }

        public CommunicationsModel(string xmlFilename)
        {
            XmlFilename = xmlFilename;
            LoadFromXml(XmlFilename);
        }

        ~CommunicationsModel()
        {
            SaveToXml(XmlFilename);
        }

        public bool LoadFromXml(string filename)
        {
            if (filename.Length > 0 && System.IO.File.Exists(filename))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CommunicationsModel));
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    CommunicationsModel communicationsModel = (CommunicationsModel) serializer.Deserialize(fs);
                    fs.Close();
                    Initialize(communicationsModel);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool SaveToXml(string filename)
        {
            if (filename.Length > 0)
            {
                try
                {
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);

                    XmlSerializer serializer = new XmlSerializer(typeof(CommunicationsModel));
                    TextWriter writer = new StreamWriter(filename);
                    serializer.Serialize(writer, this);
                    writer.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public void Initialize(CommunicationsModel communicationsModel)
        {
            StreamingPublisherCommunicationsModel = communicationsModel.StreamingPublisherCommunicationsModel;
            StreamingPublisherTopic = communicationsModel.StreamingPublisherTopic;
            SSHConnectionModel = communicationsModel.SSHConnectionModel;
            SSHStartRecordCommand = communicationsModel.SSHStartRecordCommand;
            SSHStopRecordCommand = communicationsModel.SSHStopRecordCommand;
        }

        [XmlElement]
        public URLModel StreamingPublisherCommunicationsModel { get; set; } = new URLModel();
        [XmlElement]
        public string StreamingPublisherTopic { get; set; } = "777";
        [XmlElement]
        public SSHConnectionModel SSHConnectionModel { get; set; } = new SSHConnectionModel();
        [XmlElement]
        public string SSHStartRecordCommand { get; set; } = "~/start_recording.bash";
        [XmlElement]
        public string SSHStopRecordCommand { get; set; } = "~/stop_recording.bash";
    }
}
