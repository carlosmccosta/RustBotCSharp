using System;
using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    public class RequestSender
    {
        public RequestSocket RequestSocket { get; set; }
        public ResponseSocket ResponseSocket { get; set; }

        ~RequestSender()
        {
            try
            {
                ResponseSocket?.Dispose();
                ResponseSocket?.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool InitializeRequestSender(string requestSocketUrl = "@tcp://*:13370",
            string responseSocketUrl = ">tcp://localhost:13370")
        {
            try
            {
                RequestSocket = new RequestSocket(requestSocketUrl);
                ResponseSocket = new ResponseSocket(responseSocketUrl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendRequestSynchronously(string request)
        {
            try
            {
                RequestSocket.SendFrame(request);
                ProcessResponse(ResponseSocket.ReceiveFrameString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendRequestAsynchronously(string request)
        {
            try
            {
                RequestSocket.SendFrame(request);
                ResponseSocket.ReceiveReady += ResponseSocketOnReceiveReady;
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private void ResponseSocketOnReceiveReady(object sender, NetMQSocketEventArgs netMqSocketEventArgs)
        {
            ProcessResponse(ResponseSocket.ReceiveFrameString());
        }

        public virtual bool ProcessResponse(string response)
        {
            return false;
        }
    }
}
