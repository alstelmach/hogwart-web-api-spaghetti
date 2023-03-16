using Hogwart.Api.DependencyInjection;
using Hogwart.Api.Web;

WebApplication
    .CreateBuilder(args)
    .ConfigureDependencyInjection()
    .Build()
    .ConfigureWebApplication()
    .Run();
