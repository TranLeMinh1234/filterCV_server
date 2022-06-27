using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace filterCVSocket.Service
{
    public class WebSocketServerConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public string AddSocket(WebSocket socket)
        {
            string connID = Guid.NewGuid().ToString();
            _sockets.TryAdd(connID,socket);
            Console.WriteLine("AddSocket: " + connID );
            return connID;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllSocket()
        {
            return _sockets;
        }
    }
}
