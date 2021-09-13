using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public class TaskManager : ITaskManager
    {
        private static IDictionary<Guid, ITaskJob> _TaskList = new Dictionary<Guid, ITaskJob>(); //Lists with current tasks.
        private readonly object _taskManagerLock = new object();
        private readonly ITaskJob _ITaskJob;
        public TaskManager(ITaskJob ITaskJob)
        {
            _ITaskJob = ITaskJob;
        }
        public void CreateTask(Guid TaskId)
        {
            lock (_taskManagerLock)
            {
                ITaskJob task = GetTaskJob(TaskId);
                if (task is null)
                {
                    task = new TaskJob();
                    _TaskList.Add(TaskId, task);
                }

            }
        }

        public ITaskJob GetTaskJob(Guid TaskId)
        {
            lock (_taskManagerLock)
            {
                return _TaskList.Where(x => x.Key == TaskId).Select(y => y.Value).FirstOrDefault();
            }
        }

        public void RemoveTask(Guid TaskId)
        {
            lock (_taskManagerLock)
            {
                _TaskList.Remove(TaskId);
            }
        }
    }
}
