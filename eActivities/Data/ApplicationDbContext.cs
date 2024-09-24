using eActivities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eActivities.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<eActivities.Models.Category> Category { get; set; }

public DbSet<eActivities.Models.Genre> Genre { get; set; }

public DbSet<eActivities.Models.Activityitem> Activityitem { get; set; }
public DbSet<eActivities.Models.StatusTracker> StatusTrackers { get; set; }
public DbSet<eActivities.Models.ResponsibleManager> ResponsibleManagers { get; set; }
public DbSet<eActivities.Models.DayTask> DayTask { get; set; }
}
