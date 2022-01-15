using Microsoft.AspNetCore.Mvc;

namespace BackgroundTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBackgroundTaskQueue _TaskQueue;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            _TaskQueue = taskQueue;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        [Route("AddTask")]
        public async Task<IActionResult> AddTask(int id,string Message)
        {
            _TaskQueue.QueueAITask(new TaskList() { TaskId=id,TaskMessage=Message});
            await Task.Delay(1000);
            return Ok(new
            {
                Id = id,
                Message="Ìí¼Ó³É¹¦"
            });
          
        }

        [HttpPost]
        [Route("GetTsakList")]
        public async Task<IActionResult> GetTsakInfo()
        {
            await Task.Delay(1000);
            return Ok(_TaskQueue.GetTaskList());
        }
    }
}