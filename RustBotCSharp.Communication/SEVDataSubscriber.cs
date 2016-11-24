using System;
using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    abstract class SEVDataSubscriber
    {
        public SubscriberSocket SubscriberSocket { get; set; }

        protected SEVDataSubscriber(string subscriberUrl = "tcp://localhost:13370", string topic = "")
        {
            SubscriberSocket = new SubscriberSocket();
            SubscriberSocket.Connect(subscriberUrl);
            SubscriberSocket.Subscribe(topic);
        }

        public void StartReceivingData()
        {
            SubscriberSocket.ReceiveReady += SubscriberSocketOnReceiveReady;
        }

        private void SubscriberSocketOnReceiveReady(object sender, NetMQSocketEventArgs netMqSocketEventArgs)
        {
            ProcessData(ReceiveData());
        }

        public SEVData ReceiveData()
        {
            return SEVData.Parser.ParseFrom(SubscriberSocket.ReceiveFrameBytes());
        }

        public abstract void ProcessData(SEVData data);
    }
}
