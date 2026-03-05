using Dottor12DvdRentalFilm.Models;
using Npgsql;
using NpgsqlTypes;
using Spectre.Console;

namespace Dottor12DvdRentalFilm.Services;

internal class FilmService
{
    private readonly string _connectionString = "Server=127.0.0.1;port=5433;Database=dvdrental;User Id=admin;Password=adminpass;";

    public IEnumerable<Film> GetList()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = """
            SELECT *
            FROM film
            """;

        using var command = new NpgsqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        List<Film> films = [];
        while(reader.Read())
        {
            var film = new Film();
            films.Add(film);
            film.Id = (int)reader["film_id"];
            film.Title = (string)reader["title"];
            film.Description = (string)reader["description"];
            film.ReleaseYear = (int)reader["release_year"];
            film.LanguageId = (short)reader["language_id"];
            film.RentalDuration = (short)reader["rental_duration"];
            film.RentalRate = (decimal)reader["rental_rate"];
            film.Length = (short)reader["length"];
            film.ReplacementCost = (decimal)reader["replacement_cost"];
            film.Rating = (string)reader["rating"];
            film.LastUpdate = (DateTime)reader["last_update"];
            film.SpecialFeatures = [.. (string[])reader["special_features"]];
            film.FullText = (NpgsqlTsVector)reader["fulltext"];
        }
        return films;
    }

    public Film? GetById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        string query = $"""
            SELECT *
            FROM film
            WHERE film_id = @id
            """;
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            var film = new Film();
            film.Id = (int)reader["film_id"];
            film.Title = (string)reader["title"];
            film.Description = (string)reader["description"];
            film.ReleaseYear = (int)reader["release_year"];
            film.LanguageId = (short)reader["language_id"];
            film.RentalDuration = (short)reader["rental_duration"];
            film.RentalRate = (decimal)reader["rental_rate"];
            film.Length = (short)reader["length"];
            film.ReplacementCost = (decimal)reader["replacement_cost"];
            film.Rating = (string)reader["rating"];
            film.LastUpdate = (DateTime)reader["last_update"];
            film.SpecialFeatures = [.. (string[])reader["special_features"]];
            film.FullText = (NpgsqlTsVector)reader["fulltext"];
            return film;
        }
        else
        {
            return null;
        }
    }

    public void Insert(Film film)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        string query = $"""
            INSERT INTO film
            (title, description, release_year, language_id, rental_duration, rental_rate, length, replacement_cost, rating, last_update, special_features, fulltext)
            VALUES
            (@title, @description, @release_year, @language_id, @rental_duration, @rental_rate, @length, @replacement_cost, @rating, @last_update, @special_features, @fulltext)
            RETURNING film_id
            """;
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("title", film.Title);
        command.Parameters.AddWithValue("description", film.Description);
        command.Parameters.AddWithValue("release_year", film.ReleaseYear);
        command.Parameters.AddWithValue("language_id", film.LanguageId);
        command.Parameters.AddWithValue("rental_duration", film.RentalDuration);
        command.Parameters.AddWithValue("rental_rate", film.RentalRate);
        command.Parameters.AddWithValue("length", film.Length);
        command.Parameters.AddWithValue("replacement_cost", film.ReplacementCost);
        command.Parameters.AddWithValue("rating", film.Rating);
        command.Parameters.AddWithValue("last_update", DateTime.Now);
        command.Parameters.AddWithValue("special_features", film.SpecialFeatures.ToArray());
        command.Parameters.AddWithValue("fulltext", film.FullText);
        int newId = (int)command.ExecuteScalar();
        film.Id = newId;
    }

    public void Update(Film film)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        string query = $"""
            UPDATE film
            SET title = @title,
                description = @description,
                release_year = @release_year,
                language_id = @language_id,
                rental_duration = @rental_duration,
                rental_rate = @rental_rate,
                length = @length,
                replacement_cost = @replacement_cost,
                rating = @rating,
                last_update = @last_update,
                special_features = @special_features,
                fulltext = @fulltext
            WHERE film_id = @id
            """;
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("id", film.Id);
        command.Parameters.AddWithValue("title", film.Title);
        command.Parameters.AddWithValue("description", film.Description);
        command.Parameters.AddWithValue("release_year", film.ReleaseYear);
        command.Parameters.AddWithValue("language_id", film.LanguageId);
        command.Parameters.AddWithValue("rental_duration", film.RentalDuration);
        command.Parameters.AddWithValue("rental_rate", film.RentalRate);
        command.Parameters.AddWithValue("length", film.Length);
        command.Parameters.AddWithValue("replacement_cost", film.ReplacementCost);
        command.Parameters.AddWithValue("rating", film.Rating);
        command.Parameters.AddWithValue("last_update", DateTime.Now);
        command.Parameters.AddWithValue("special_features", film.SpecialFeatures.ToArray());
        command.Parameters.AddWithValue("fulltext", film.FullText);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        string query = $"""
            DELETE FROM film
            WHERE film_id = @id
            """;
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public long Count()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        string query = $"""
            SELECT COUNT(*)
            FROM film
            """;
        using var command = new NpgsqlCommand(query, connection);
        long count = (long)command.ExecuteScalar();
        return count;
    }

    /**
     * method to get a list of films based on the provided parameters. The parameters are used to filter the results based on the corresponding columns in the film table. The method returns an IEnumerable of Film objects that match the specified criteria.
     * all the parameters are optional, so if a parameter is not provided, it will not be used in the filtering process. The method uses a SQL query to retrieve the data from the database and maps the results to Film objects before returning them as a list.
     */
    public IEnumerable<Film> GetList(
            int? id = null,
            string? title = null,
            string? description = null,
            int? releaseYear = null,
            short? languageId = null,
            short? rentalDuration = null,
            decimal? rentalRate = null,
            short? length = null,
            decimal? replacementCost = null,
            string? rating = null,
            DateTime? lastUpdate = null,
            string[]? specialFeatures = null,
            NpgsqlTsVector? fullText = null
        )
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string query = """
            SELECT *
            FROM film
            WHERE
                (@id is null OR film_id = @id) AND
                (@title is null OR title like @title) AND
                (@description is null OR description like @description) AND
                (@releaseYear is null OR release_year = @releaseYear) AND
                (@languageId is null OR language_id = @languageId) AND
                (@rentalDuration is null OR rental_duration = @rentalDuration) AND
                (@rentalRate is null OR rental_rate = @rentalRate) AND
                (@length is null OR length = @length) AND
                (@replacementCost is null OR replacement_cost = @replacementCost) AND
                (@rating is null OR rating like @rating) AND
                (@lastUpdate is null OR last_update >= @lastUpdate) AND
                (@specialFeatures is specialFeatures OR special_features && @specialFeatures) AND
                (@fullText is null OR fulltext @@ plainto_tsquery(@fullText)
            """;

        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id is null ? DBNull.Value : id);
        command.Parameters.AddWithValue("@title", title is null ? DBNull.Value : title);
        command.Parameters.AddWithValue("@description", description is null ? DBNull.Value : description);
        command.Parameters.AddWithValue("@releaseYear", releaseYear is null ? DBNull.Value : releaseYear);
        command.Parameters.AddWithValue("@languageId", languageId is null ? DBNull.Value : languageId);
        command.Parameters.AddWithValue("@rentalDuration", rentalDuration is null ? DBNull.Value : rentalDuration);
        command.Parameters.AddWithValue("@rentalRate", rentalRate is null ? DBNull.Value : rentalRate);
        command.Parameters.AddWithValue("@length", length is null ? DBNull.Value : length);
        command.Parameters.AddWithValue("@replacementCost", replacementCost is null ? DBNull.Value : replacementCost);
        command.Parameters.AddWithValue("@rating", rating is null ? DBNull.Value : rating);
        command.Parameters.AddWithValue("@lastUpdate", lastUpdate is null ? DBNull.Value : lastUpdate);
        command.Parameters.AddWithValue("@specialFeatures", specialFeatures is null ? DBNull.Value : specialFeatures);
        command.Parameters.AddWithValue("@fullText", fullText is null ? DBNull.Value : fullText);

        using var reader = command.ExecuteReader();

        List<Film> films = [];
        while (reader.Read())
        {
            var film = new Film();
            films.Add(film);
            film.Id = (Int16)reader["film_id"];
            film.Title = (string)reader["title"];
            film.Description = (string)reader["description"];
            film.ReleaseYear = (int)reader["release_year"];
            film.LanguageId = (short)reader["language_id"];
            film.RentalDuration = (short)reader["rental_duration"];
            film.RentalRate = (decimal)reader["rental_rate"];
            film.Length = (short)reader["length"];
            film.ReplacementCost = (decimal)reader["replacement_cost"];
            film.Rating = (string)reader["rating"];
            film.LastUpdate = (DateTime)reader["last_update"];
            film.SpecialFeatures = [.. (string[])reader["special_features"]];
            film.FullText = (NpgsqlTsVector)reader["fulltext"];
        }
        return films;
    }
}
