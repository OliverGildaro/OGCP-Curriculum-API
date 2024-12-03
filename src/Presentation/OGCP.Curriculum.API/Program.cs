using OGCP.Curriculum.API.Helpers;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex)
{
}
finally
{
}

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.Run();
