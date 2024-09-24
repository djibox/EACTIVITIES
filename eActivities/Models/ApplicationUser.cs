using Microsoft.AspNetCore.Identity;

namespace eActivities.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
