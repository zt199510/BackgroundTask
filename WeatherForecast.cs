namespace BackgroundTask
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }


    public class TaskList
    {
        public int TaskId { get; set; }

        public string TaskMessage { get; set; }
    }
}