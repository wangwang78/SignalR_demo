using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace SignalRChat.Services
{
    public interface IChatService
    {
        bool QueueSendMessage(string user, string message);
    }

    public class ChatService : IChatService
    {
        private readonly BackgroundTaskQueue _queue;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(BackgroundTaskQueue queue, IHubContext<ChatHub> hubContext)
        {
            _queue = queue;
            _hubContext = hubContext;
        }

        public bool QueueSendMessage(string user, string message)
        {
            var valueTask = _queue.Enqueue(async ct =>
            {
                await Task.Delay(5000, ct);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message, ct);
            });
            
            Console.WriteLine("I didn't wait");
            return valueTask.IsCompleted;
        }
    }
}
