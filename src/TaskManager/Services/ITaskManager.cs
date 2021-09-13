using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public interface ITaskManager
    {
        void CreateTask(Guid TaskId);
        ITaskJob GetTaskJob(Guid TaskId);
        void RemoveTask(Guid TaskId);
    }
}
