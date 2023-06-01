using AuthService.Shared.Modules;
using AuthService.Shared;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureModules();

var assemblies = ModuleLoader.LoadAssemblies();

var modules = ModuleLoader.LoadModules(assemblies);

//register all services from modules
foreach (var module in modules)
{
    module.Register(builder.Services);
}




builder.Services.AddShared(modules, builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.UseShared();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", () => "API");
    endpoints.MapModuleInfo();
});

app.Run();


