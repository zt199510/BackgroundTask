using System.Collections.Concurrent;


namespace BackgroundTask
{

    public interface IBackgroundTaskQueue
    {
        void QueueAITask(TaskList task);

        TaskList Dequeue();

        void AddSuccessTsak(TaskList task);
        public List<TaskList> GetTaskList();
    }

    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<TaskList> _queue = new ConcurrentQueue<TaskList>();

        public List<TaskList> SuccessList = new List<TaskList>();

        public List<TaskList> GetTaskList() { return SuccessList; }
        public TaskList Dequeue()
        {
          
            if (_queue.Count > 0)
            {
                _queue.TryDequeue(out var task);
                return task;
            }
            else
            {
                return null;
            }
        }

        public void QueueAITask(TaskList task)
        {
            if (task==null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _queue.Enqueue(task);

        }

        public void AddSuccessTsak(TaskList task)
        {
           SuccessList.Add(task);
        }
    }
}
