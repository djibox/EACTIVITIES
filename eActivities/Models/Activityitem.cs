using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eActivities.Models
{
    public class Activityitem
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? AppUser { get; set; }
    }
}
