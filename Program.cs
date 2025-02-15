using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGamesEndPoints();
        app.MapTestEndPoints();
        app.Run();
    }
}
