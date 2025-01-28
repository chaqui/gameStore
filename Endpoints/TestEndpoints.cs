namespace GameStore.Api.Endpoints
{
    public static class TestEndpoints
    {
        public static WebApplication MapTestEndPoints(this WebApplication app)
        {
            app.MapGet("/test", () => "Hello World!");
            return app;
        }
    }
}