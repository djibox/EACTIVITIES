namespace eActivities.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<Activityitem>? Activities { get; set; }
    }
}
