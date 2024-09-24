namespace eActivities.Models
{
    public class ResponsibleManager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<DayTask> DayTasks { get; set; }

    }
}