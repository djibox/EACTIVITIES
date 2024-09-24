using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eActivities.ViewModels
{
    public class ActivityVM
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? AppUser { get; set; }
    }
}
