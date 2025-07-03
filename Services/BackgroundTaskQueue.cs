using System.Threading.Channels;

namespace SignalRChat.Services
{
    public class BackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> _queue = Channel.CreateUnbounded<Func<CancellationToken, Task>>();

        public ValueTask Enqueue(Func<CancellationToken, Task> task)
            => _queue.Writer.WriteAsync(task);

        public ValueTask<Func<CancellationToken, Task>> DequeueAsync(CancellationToken token)
            => _queue.Reader.ReadAsync(token);
    }
}
