namespace SignalRChat.Services
{
    public class BackgroundTaskService : BackgroundService
    {
        private readonly BackgroundTaskQueue _queue;

        public BackgroundTaskService(BackgroundTaskQueue queue)
        {
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _queue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"后台任务错误: {ex.Message}");
                }
            }
        }
    }
}
