using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AspnetCoreWebApiLab.SignalR
{
    /// <summary>
    /// 测试的前端需搭配 JavascriptLabs 中的 SignalRLab
    /// </summary>
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
