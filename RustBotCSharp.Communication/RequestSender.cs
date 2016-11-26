using System;
using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    public class RequestSender
    {
        public RequestSocket RequestSocket { get; set; }
        public ResponseSocket ResponseSocket { get; set; }

        public void InitializeRequestSender(string requestSocketUrl = "@tcp://*:13370", string responseSocketUrl = ">tcp://localhost:13370")
        {
            RequestSocket = new RequestSocket(requestSocketUrl);
            ResponseSocket = new ResponseSocket(responseSocketUrl);
        }

        public void SendRequestSynchronously(string request)
        {
            RequestSocket.SendFrame(request);
            ProcessResponse(ResponseSocket.ReceiveFrameString());
        }

        public void SendRequestAsynchronously(string request)
        {
            ResponseSocket.ReceiveReady += ResponseSocketOnReceiveReady;
        }

        private void ResponseSocketOnReceiveReady(object sender, NetMQSocketEventArgs netMqSocketEventArgs)
        {
            ProcessResponse(ResponseSocket.ReceiveFrameString());
        }

        public void ProcessResponse(string response) { }
    }
}
