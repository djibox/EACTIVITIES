namespace eActivities.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public ICollection<Activityitem>? Activities { get; set; }
    }
}
