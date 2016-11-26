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
