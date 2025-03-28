using System.Collections.Concurrent;

namespace Smudging.src.Server
{
    // 队列处理任务
    public class QueueProcessTask
    {
        // 单例
        private static QueueProcessTask _instance = new();
        // 队列
        private readonly BlockingCollection<Func<Task>> taskQueue;
        // 取消标记
        private readonly CancellationTokenSource cancellationTokenSource;
        // 队列处理任务
        private readonly Task? queueProcessorTask;

        private QueueProcessTask()
        {
            taskQueue = [];
            cancellationTokenSource = new CancellationTokenSource();
            // 启动任务队列处理线程
            queueProcessorTask = Task.Run(() => ProcessTaskQueue(cancellationTokenSource.Token));
        }

        // 单例
        public static QueueProcessTask Instance()
        {
            _instance ??= new QueueProcessTask();
            return _instance;
        }

        // 添加任务
        public void AddTask(Func<Task> task)
        {
            taskQueue.Add(task);
        }

        // 处理任务队列中的任务
        private async Task ProcessTaskQueue(CancellationToken cancellationToken)
        {
            // 无限循环，直到取消
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // 阻塞等待队列中的任务
                    var task = taskQueue.Take(cancellationToken);
                    // 执行任务
                    await task();
                }
                catch (OperationCanceledException)
                {
                    // 如果取消了任务，退出循环
                    break;
                }
            }
        }

        // 停止任务队列处理线程
        public void Stop()
        {
            cancellationTokenSource.Cancel();
            queueProcessorTask?.Wait(); // 等待队列处理线程退出
        }

        // 直接启动异步任务
        public static void StartThreadTask(Action task)
        {
            new Thread(() =>
            {
                task();
            }).Start();
        }

    }
}