namespace eActivities.Models
{
    public class StatusTracker
    {
        public int Id { get; set; }
        public string StatusTrackerName { get; set; }
        public ICollection<DayTask> DayTasks { get; set; }
    }
}