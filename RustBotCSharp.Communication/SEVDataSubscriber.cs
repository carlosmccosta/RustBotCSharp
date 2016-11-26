using System;
using System.Linq;
using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    public class SEVDataSubscriber
    {
        public SubscriberSocket SubscriberSocket { get; set; }
        public NetMQPoller NetMqPoller { get; set; }
        public int MessageTopicHeaderSize = 0;

        public void InitializeSubscriber(string subscriberUrl = "tcp://localhost:13370", string topic = "")
        {
            MessageTopicHeaderSize = topic.Length + 1;
            SubscriberSocket = new SubscriberSocket();
            SubscriberSocket.Connect(subscriberUrl);
            SubscriberSocket.Subscribe(topic);
        }

        public void StartReceivingDataAsynchronously()
        {
            SubscriberSocket.ReceiveReady += SubscriberSocketOnReceiveReady;
            NetMqPoller = new NetMQPoller {SubscriberSocket};
            NetMqPoller.RunAsync();
        }

        public void StopReceivingDataAsynchronously()
        {
            SubscriberSocket.ReceiveReady -= SubscriberSocketOnReceiveReady;
            NetMqPoller.Remove(SubscriberSocket);
            NetMqPoller.StopAsync();
            NetMqPoller.Dispose();
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
            Msg msg = new Msg();
            msg.InitEmpty();
            SubscriberSocket.Receive(ref msg);
            
            SEVData data = null;
            try
            {
                if (msg.Size > MessageTopicHeaderSize)
                {
                    byte[] serializedData = msg.Data.Skip(MessageTopicHeaderSize).ToArray();
                    data = SEVData.Parser.ParseFrom(serializedData);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return data;
        }

        public virtual void ProcessData(SEVData data) { }
    }
}
