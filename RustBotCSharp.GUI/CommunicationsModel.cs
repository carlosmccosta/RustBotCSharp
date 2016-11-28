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
                catch (Exception e)
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
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public void Initialize(CommunicationsModel communicationsModel)
        {
            StreamingSubscriberCommunicationsModel = communicationsModel.StreamingSubscriberCommunicationsModel;
            StreamingSubscriberTopic = communicationsModel.StreamingSubscriberTopic;
            RecordRequestCommunicationsModel = communicationsModel.RecordRequestCommunicationsModel;
            RecordResponseCommunicationsModel = communicationsModel.RecordResponseCommunicationsModel;
        }

        [XmlElement]
        public URLModel StreamingSubscriberCommunicationsModel { get; set; } = new URLModel();
        [XmlElement]
        public string StreamingSubscriberTopic { get; set; } = "13371";
        [XmlElement]
        public URLModel RecordRequestCommunicationsModel { get; set; } = new URLModel("@tcp", "*", 13372);
        [XmlElement]
        public URLModel RecordResponseCommunicationsModel { get; set; } = new URLModel(">tcp", "localhost", 13373);
    }
}
