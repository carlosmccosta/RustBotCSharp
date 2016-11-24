using NetMQ;
using NetMQ.Sockets;

namespace RustBotCSharp.Communication
{
    public abstract class RequestSender
    {
        public RequestSocket RequestSocket { get; set; }
        public ResponseSocket ResponseSocket { get; set; }

        protected RequestSender(string requestSocketUrl = "@tcp://*:13370", string responseSocketUrl = ">tcp://localhost:13370")
        {
            RequestSocket = new RequestSocket(requestSocketUrl);
            ResponseSocket = new ResponseSocket(responseSocketUrl);
        }

        public void SendRequest(string request)
        {
            RequestSocket.SendFrame(request);
            ProcessResponse(ResponseSocket.ReceiveFrameString());
        }

        public abstract void ProcessResponse(string response);
    }
}
