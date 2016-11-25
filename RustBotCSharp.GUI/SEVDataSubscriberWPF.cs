using RustBotCSharp.Communication;

namespace RustBotCSharp.GUI
{
    class SEVDataSubscriberWPF : SEVDataSubscriber
    {
        public SEVDataModel SEVDataModel { get; set; } = new SEVDataModel();
        public override void ProcessData(SEVData data)
        {
            
        }
    }
}
