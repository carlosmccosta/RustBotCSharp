using RustBotCSharp.Communication;

namespace RustBotCSharp.MessageConverter
{
    public class SEVDataFileSaver : SEVDataSubscriber
    {
        public override void ProcessData(SEVData data)
        {
            SaveImage(data.LeftImage);
            SaveImage(data.RightImage);
            SavePointCloud(data.PointCloud);
            SavePose();
            SaveGNSS();
        }

        public void SaveImage(Image image)
        {

        }

        public void SavePointCloud(PointCloud2 pointCloud)
        {
            
        }

        public void SavePose()
        {
            
        }

        public void SaveGNSS()
        {
            
        }
    }
}
