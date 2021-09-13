using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITaskManager _taskManager;
        private readonly ITaskJob _taskJob;

        public TaskManagerController(ILogger<WeatherForecastController> logger, ITaskJob taskJob, ITaskManager taskManager)
        {
            _logger = logger;
            _taskManager = taskManager;
            _taskJob = taskJob;
        }

        [HttpGet]
        public int GetProgress(Guid taskId)
        {
            var progress = _taskManager.GetTaskJob(taskId).GetProgress();

            return progress;
        }
        [HttpPost]
        public string DoJob(int m)
        {
            var jobId = Guid.NewGuid();
            _taskManager.CreateTask(jobId);
            var task = _taskManager.GetTaskJob(jobId);
            task.DoTheJob(m);
            return jobId.ToString();
        }
    }
}
