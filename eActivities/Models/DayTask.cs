using System.ComponentModel.DataAnnotations;

namespace eActivities.Models
{
    public class DayTask
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string TaskAction { get; set; }
        public int ResponsibleManagerId { get; set; }
        public ResponsibleManager ResponsibleManager { get; set; }
        public int StatusTrackerId { get; set; }
        public StatusTracker StatusTracker { get; set; }
        [DataType(DataType.Date)]
        public DateTime InitialTargetDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime RevisedDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
    }
}
