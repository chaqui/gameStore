using GameStore.Api.Dtos;


namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        private static readonly List<GameDto> games = new()
        {
            new GameDto(1, "GTA V", "Action", 99.99m, new DateOnly(2013, 9, 17)),
            new GameDto(2, "FIFA 22", "Sport", 199.99m, new DateOnly(2021, 10, 1)),
            new GameDto(3, "Cyberpunk 2077", "RPG", 199.99m, new DateOnly(2020, 12, 10)),
        };

        public static WebApplication MapGamesEndPoints(this WebApplication app)
        {
            const string GetGameEndpoint = "GetGameById";

            app.MapGet("/games", () => games);
            app.MapGet("/", () => "Hello World!");

            //GET
            app.MapGet(
                    "/games/{id}",
                    (int id) =>
                    {
                        GameDto? game = games.FirstOrDefault(g => g.Id == id);
                        return game is not null ? Results.Ok(game) : Results.NotFound();
                    }
                )
                .WithName(GetGameEndpoint);

            //POST
            app.MapPost(
                "/games",
                (CreateGameDto game) =>
                {
                    var newGame = new GameDto(game, games.Count + 1);
                    games.Add(newGame);
                    return Results.CreatedAtRoute(
                        GetGameEndpoint,
                        new { id = newGame.Id },
                        newGame
                    );
                }
            );

            //PUT
            app.MapPut(
                "/games/{id}",
                (int id, UpdateGameDto game) =>
                {
                    var existingGame = games.FirstOrDefault(g => g.Id == id);
                    if (existingGame is null)
                    {
                        return Results.NotFound();
                    }

                    var updatedGame = new GameDto(game, id);
                    games[games.IndexOf(existingGame)] = updatedGame;
                    return Results.Ok(updatedGame);
                }
            );

            //DEETE
            app.MapDelete(
                "/games/{id}",
                (int id) =>
                {
                    var existingGame = games.FirstOrDefault(g => g.Id == id);
                    if (existingGame is null)
                    {
                        return Results.NotFound();
                    }

                    games.Remove(existingGame);
                    return Results.NoContent();
                }
            );
            return app;
        }
    }
}
