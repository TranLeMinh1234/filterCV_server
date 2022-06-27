using filterCVSocket.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace filterCVSocket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProccessController : ControllerBase
    {
        private readonly WebSocketServerConnectionManager _manager;

        public ProccessController(WebSocketServerConnectionManager manager)
        {
            _manager = manager;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProccessAsync([FromBody] JsonElement body)
        {
            string textInfo = body.GetString("textInfo");
            ConcurrentDictionary<string, WebSocket> sockets = _manager.GetAllSocket();
            IEnumerable<WebSocket> listSocketID = sockets.Values;
            foreach (WebSocket socket in listSocketID)
            {
                if(socket.State == WebSocketState.Connecting || socket.State == WebSocketState.Open)
                    await SendText(socket, textInfo);
            }
            return Ok();
        }

        public async Task SendText(WebSocket socket, string text)
        {
            var buffer = Encoding.UTF8.GetBytes(text);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
