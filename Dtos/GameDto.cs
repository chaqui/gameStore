namespace GameStore.Api.Dtos;

public record class GameDto(int Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate) : CreateGameDto(Name, Genre, Price, ReleaseDate)
{
    public GameDto(CreateGameDto game, int id) : this(id, game.Name, game.Genre, game.Price, game.ReleaseDate)
    {
    }

    public GameDto(UpdateGameDto game, int id) : this(id, game.Name, game.Genre, game.Price, game.ReleaseDate)
    {
    }
}