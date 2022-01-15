namespace BackgroundTask
{
    public class HandTaskHostService:BackgroundService
    {
        private readonly ILogger _logger;
        public HandTaskHostService(ILogger<HandTaskHostService> logger,IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            TaskQueue=taskQueue;
        }
        public IBackgroundTaskQueue TaskQueue { get; }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("服务启动");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var TaskDemo = TaskQueue.Dequeue();
                    if (TaskDemo != null)
                    {
                        Console.WriteLine("队列任务" + TaskDemo.TaskId+"启动");

                        TaskQueue.AddSuccessTsak(TaskDemo);
                    }
                    else
                    {
                        Console.WriteLine("没有获取任务开始等待");
                        await Task.Delay(TimeSpan.FromMilliseconds(1000));
                    }

                    //查询任务///
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex,
                        $"Error occurred executing ai task.");
                }
            }

            _logger.LogInformation("Handle AI task Hosted Service is stopping.");
        }
    }
}
