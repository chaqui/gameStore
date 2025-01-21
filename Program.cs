using GameStore.Api.Dtos;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        const string GetGameEndpoint = "GetGameById";
        List<GameDto> games = new()
{
    new GameDto(1, "GTA V", "Action", 99.99m, new DateOnly(2013, 9, 17)),
    new GameDto(2, "FIFA 22", "Sport", 199.99m, new DateOnly(2021, 10, 1)),
    new GameDto(3, "Cyberpunk 2077", "RPG", 199.99m, new DateOnly(2020, 12, 10))
};

        app.MapGet("/games", () => games);
        app.MapGet("/", () => "Hello World!");
        app.MapGet("/games/{id}", (int id) =>
            games.FirstOrDefault(g => g.Id == id) ?? new GameDto(0, "Not Found", "Not Found", 0, new DateOnly()))
            .WithName(GetGameEndpoint);

        app.MapPost("/games", (CreateGameDto game) =>
        {
            var newGame = new GameDto(game, games.Count + 1);
            games.Add(newGame);
            return Results.CreatedAtRoute(GetGameEndpoint, new { id = newGame.Id }, newGame);
        });
        app.Run();
    }
}