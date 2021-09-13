using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public interface ITaskJob
    {
        bool IsRunning();
        void CancelJob();
        int GetProgress();
        void DoTheJob(int m);
    }
}
