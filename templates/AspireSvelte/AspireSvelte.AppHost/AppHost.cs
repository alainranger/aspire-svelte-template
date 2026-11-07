var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AspireSvelte_ApiService>("api")
    .WithHttpEndpoint()
    .WithHttpHealthCheck("/health");

builder.AddJavaScriptApp("frontend", "../frontend")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithHttpEndpoint(env: "PORT")
    .PublishAsDockerFile();

builder.Build().Run();
