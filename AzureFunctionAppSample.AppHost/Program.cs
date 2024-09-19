var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AzureFunctionAppSample>("azurefunctionappsample"); 
builder.Build().Run();
