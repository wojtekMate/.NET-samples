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
        private readonly ILogger<TaskManagerController> _logger;
        private readonly ITaskManager _taskManager;
        private readonly ITaskJob _taskJob;

        public TaskManagerController(ILogger<TaskManagerController> logger, ITaskJob taskJob, ITaskManager taskManager)
        {
            _logger = logger;
            _taskManager = taskManager;
            _taskJob = taskJob;
        }

        [HttpGet]
        public ActionResult<object> GetProgress(string taskId)
        {
            var task = _taskManager.GetTaskJob(Guid.Parse(taskId));
            var progress = task.GetProgress();

            return Ok(new { progress = progress, taskId = taskId});
        }
        [HttpPost]
        public ActionResult DoJob(int m)
        {
            var jobId = Guid.NewGuid();
            _taskManager.CreateTask(jobId);
            var task = _taskManager.GetTaskJob(jobId);
            task.DoTheJob(m);
            // return Created(GetProgress(jobId.ToString());
            return  CreatedAtAction(nameof(GetProgress), new { jobId = jobId } );
        }
    }
}
