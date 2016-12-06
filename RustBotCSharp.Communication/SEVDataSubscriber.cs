using System;
using Google.Protobuf;
using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    public class SEVDataSubscriber
    {
        public SubscriberSocket SubscriberSocket { get; set; }
        public NetMQPoller NetMqPoller { get; set; }
        public int MessageTopicHeaderSize { get; set; } = 0;
        public int LastMessageSizeInBytes { get; set; } = 0;

        ~SEVDataSubscriber()
        {
            try
            {
                SubscriberSocket?.Dispose();
                NetMqPoller?.Stop();
                NetMqPoller?.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool InitializeSubscriber(string subscriberUrl = "tcp://localhost:13370", string topic = "")
        {
            MessageTopicHeaderSize = topic.Length + 1;
            try
            {
                SubscriberSocket = new SubscriberSocket();
                SubscriberSocket.Connect(subscriberUrl);
                SubscriberSocket.Subscribe(topic);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StartReceivingDataAsynchronously()
        {
            try
            {
                SubscriberSocket.ReceiveReady += SubscriberSocketOnReceiveReady;
                NetMqPoller = new NetMQPoller { SubscriberSocket };
                NetMqPoller.RunAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StopReceivingDataAsynchronously()
        {
            try
            {
                SubscriberSocket.ReceiveReady -= SubscriberSocketOnReceiveReady;
                NetMqPoller.Remove(SubscriberSocket);
                NetMqPoller.StopAsync();
                NetMqPoller.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void StartReceivingDataSynchronously()
        {
            while (true)
            {
                ProcessData(ParseData(ReceiveData()));
            }
        }

        private void SubscriberSocketOnReceiveReady(object sender, NetMQSocketEventArgs netMqSocketEventArgs)
        {
            ProcessData(ParseData(ReceiveData()));
        }

        public virtual CodedInputStream ReceiveData()
        {
            CodedInputStream data = null;
            try
            {
                Msg msg = new Msg();
                msg.InitEmpty();
                SubscriberSocket.Receive(ref msg);

                if (msg.Size > MessageTopicHeaderSize)
                {
                    LastMessageSizeInBytes = msg.Size - MessageTopicHeaderSize;
                    data = new CodedInputStream(msg.Data, MessageTopicHeaderSize, LastMessageSizeInBytes);
                }
                else
                {
                    LastMessageSizeInBytes = 0;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return data;
        }

        public virtual SEVData ParseData(CodedInputStream serializedData)
        {
            if (serializedData != null)
            {
                try
                {
                    return SEVData.Parser.ParseFrom(serializedData);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return null;
        }

        public virtual bool ProcessData(SEVData data)
        {
            return false;
        }
    }
}
