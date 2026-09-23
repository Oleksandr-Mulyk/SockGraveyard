var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SockGraveyard_Cases_Api>("sockgraveyard-cases-api");

builder.Build().Run();
