namespace Hogwart.Api.Web;

public static class WebApplicationSetup
{
    public static WebApplication ConfigureWebApplication(this WebApplication webApplication)
    {
        webApplication
            .ConfigureSwagger()
            .MapControllers();

        return webApplication;
    }

    private static WebApplication ConfigureSwagger(this WebApplication webApplication)
    {
        var isSwaggerPageGenerationRequired =  webApplication.Environment.IsDevelopment();

        if (isSwaggerPageGenerationRequired)
        {
            webApplication
                .UseSwagger()
                .UseSwaggerUI();
        }

        return webApplication;
    }
}
