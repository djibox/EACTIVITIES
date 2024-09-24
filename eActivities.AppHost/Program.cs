var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.eActivities>("eactivities");

builder.Build().Run();
