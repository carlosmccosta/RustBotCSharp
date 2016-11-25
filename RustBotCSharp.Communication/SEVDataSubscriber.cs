using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    public class SEVDataSubscriber
    {
        public SubscriberSocket SubscriberSocket { get; set; }

        public SEVDataSubscriber(string subscriberUrl = "tcp://localhost:13370", string topic = "")
        {
            SubscriberSocket = new SubscriberSocket();
            SubscriberSocket.Connect(subscriberUrl);
            SubscriberSocket.Subscribe(topic);
        }

        public void StartReceivingDataAsynchronously()
        {
            SubscriberSocket.ReceiveReady += SubscriberSocketOnReceiveReady;
        }

        public void StopReceivingDataAsynchronously()
        {
            SubscriberSocket.ReceiveReady -= SubscriberSocketOnReceiveReady;
        }

        public void StartReceivingDataSynchronously()
        {
            while (true)
            {
                ProcessData(ReceiveData());
            }
        }

        private void SubscriberSocketOnReceiveReady(object sender, NetMQSocketEventArgs netMqSocketEventArgs)
        {
            ProcessData(ReceiveData());
        }

        public SEVData ReceiveData()
        {
            return SEVData.Parser.ParseFrom(SubscriberSocket.ReceiveFrameBytes());
        }

        public virtual void ProcessData(SEVData data) { }
    }
}
