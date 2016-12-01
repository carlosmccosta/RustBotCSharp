using System;
using System.Diagnostics;
using RustBotCSharp.Communication;
using RustBotCSharp.MessageConverter;

namespace RustBotCSharp.GUI
{
    public class SEVDataSubscriberWPF : SEVDataSubscriber
    {
        public SEVDataModel SEVDataModel { get; set; } = new SEVDataModel();

        public override SEVData ReceiveData()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            SEVData data = base.ReceiveData();
            stopWatch.Stop();
            SEVDataModel.DiagnosticsModel.MessageSizeInBytes = LastMessageSizeInBytes;
            SEVDataModel.DiagnosticsModel.MessageParsingTimeMilliseconds = stopWatch.ElapsedMilliseconds;
            return data;
        }

        public override bool ProcessData(SEVData data)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                if (data != null)
                {
                    if (data.LeftImage != null)
                        SEVDataModel.LeftImageWriteableBitmap = ImageConverter.ConvertToWrittableBitmap(data.LeftImage);

                    if (data.RightImage != null)
                        SEVDataModel.RightImageWriteableBitmap = ImageConverter.ConvertToWrittableBitmap(data.RightImage);

                    if (data.PointCloud != null)
                        SEVDataModel.PointGeometry3D = PointCloudConverter.ConvertToPointGeometry3D(data.PointCloud);
                }
                stopWatch.Stop();
                SEVDataModel.DiagnosticsModel.MessageProcessingTimeMilliseconds = stopWatch.ElapsedMilliseconds;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
