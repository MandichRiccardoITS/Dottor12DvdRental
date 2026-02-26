using Dottor12DvdRental.Models;

namespace Dottor12DvdRental.Services;

class ActorsService
{
    private readonly string _connectionString = "Server=127.0.0.1;port=5433;Database=dvdrental;User Id=admin;Password=adminpass;";

    public IEnumerable<Actor> GetList()
    {
        using var conn = new Npgsql.NpgsqlConnection(_connectionString);
        conn.Open();
        string selectQuery = """
            SELECT
                actor_id,
                first_name,
                last_name,
                last_update
            FROM public.actor
            """;
        using var command = conn.CreateCommand();
        command.CommandText = selectQuery;
        using Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
        List<Actor> actors = [];
        while (reader.Read())
        {
            Actor actor = new Actor();
            actors.Add(actor);
            actor.Id = (int) reader["actor_id"];
            actor.FirstName = (string) reader["first_name"];
            actor.LastName = (string) reader["last_name"];
            actor.LastUpdate = (DateTime) reader["last_update"];
        }
        return actors;
    }

    public Actor? GetById(int id)
    {
        using var conn = new Npgsql.NpgsqlConnection(_connectionString);
        conn.Open();
        string selectQuery = """
            SELECT
                actor_id,
                first_name,
                last_name,
                last_update
            FROM public.actor
            WHERE actor_id = @id
            """;
        using var command = conn.CreateCommand();
        command.CommandText = selectQuery;
        command.Parameters.AddWithValue("@id", id);
        using Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
        Actor? actor = null;
        if (reader.Read())
        {
            actor = new Actor();
            actor.Id = (int) reader["actor_id"];
            actor.FirstName = (string) reader["first_name"];
            actor.LastName = (string) reader["last_name"];
            actor.LastUpdate = (DateTime) reader["last_update"];
        }
        return actor;
    }

    public void Insert(Actor actor)
    {
        using var conn = new Npgsql.NpgsqlConnection(_connectionString);
        conn.Open();
        string insertQuery = """
            INSERT INTO public.actor
                (first_name, last_name)
            VALUES
                (@firstName, @lastName)
            RETURNING actor_id
            """;
        using var command = conn.CreateCommand();
        command.CommandText = insertQuery;
        command.Parameters.AddWithValue("@firstName", actor.FirstName);
        command.Parameters.AddWithValue("@lastName", actor.LastName);
        actor.Id = (int)command.ExecuteScalar()!;
    }
}