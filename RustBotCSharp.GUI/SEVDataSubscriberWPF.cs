using RustBotCSharp.Communication;
using RustBotCSharp.MessageConverter;

namespace RustBotCSharp.GUI
{
    public class SEVDataSubscriberWPF : SEVDataSubscriber
    {
        public SEVDataModel SEVDataModel { get; set; } = new SEVDataModel();

        public override void ProcessData(SEVData data)
        {
            if (data != null)
            {
                if (data.LeftImage != null)
                    SEVDataModel.LeftImageWriteableBitmap = ImageConverter.ConvertToWrittableBitmap(data.LeftImage);

                if (data.RightImage != null)
                    SEVDataModel.RightImageWriteableBitmap = ImageConverter.ConvertToWrittableBitmap(data.RightImage);
            }
        }
    }
}
