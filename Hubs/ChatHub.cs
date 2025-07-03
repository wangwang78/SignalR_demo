using Microsoft.AspNetCore.SignalR;
using SignalRChat.Services;

namespace SignalRChat.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public Task SendMessage(string user, string message)
        {
            _chatService.QueueSendMessage(user, message);
            return Task.CompletedTask; // 立即返回
        }
    }
}
