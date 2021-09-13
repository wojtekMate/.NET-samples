using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public class TaskJob : ITaskJob
    {
        private Task _task = null;
        private int _progress = 0;
        private static readonly object myLock = new object();
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public TaskJob()
        {
            //inject some service to do sth, fast version with new Service() not injected;
        }
        public void DoTheJob(int m)
        {
            lock(myLock)
            {
                if (IsRunning()) return;
                _task = new Task(() => JobImpl(m), _cancellationTokenSource.Token);
                _progress = 0;
                _task.Start();
            }
        }
        public void CancelJob()
        {
            lock(myLock)
            {
                if(IsRunning() && _task.IsCanceled)
                {
                    _cancellationTokenSource.Cancel();
                }
            }
        }

        public int GetProgress()
        {
            return _progress;
        }

        public bool IsRunning()
        {
            return _task != null && !_task.IsCompleted;
        }
        private void JobImpl(int m)
        {
            try
            {
                if(_cancellationTokenSource.IsCancellationRequested)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
                //do the job
                int j = 0; 
                for (int i=0;i<m;i++) //duration job m
                {
                    _task.Wait(1000);
                    _progress = (int)((++j * 100.0) / m);
                }

            }
            catch (Exception)
            {
                _progress = -1;
                //logs
            }
        }
    }
}
