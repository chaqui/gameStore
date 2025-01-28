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

        public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
        {
            const string GetGameEndpoint = "GetGameById";

            var group = app.MapGroup("/games");

            group.MapGet("/", () => games);

            //GET
            group.MapGet(
                    "/{id}",
                    (int id) =>
                    {
                        GameDto? game = games.FirstOrDefault(g => g.Id == id);
                        return game is not null ? Results.Ok(game) : Results.NotFound();
                    }
                )
                .WithName(GetGameEndpoint);

            //POST
            group.MapPost(
                "/",
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
            group.MapPut(
                "/{id}",
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
            group.MapDelete(
                "/{id}",
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
            return group;
        }
    }
}
