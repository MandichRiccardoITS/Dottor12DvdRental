
namespace Dottor12DvdRental.Services;
using Npgsql;
using Dottor12DvdRental.Models;

class ActorsService
{
    private readonly string _connectionString = "Server=127.0.0.1;port=5433;Database=dvdrental;User Id=admin;Password=adminpass;";

    public IEnumerable<Actor> GetList()
    {
        using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string selectQuery = """
            SELECT 
                actor_id, 
                first_name, 
                last_name, 
                last_update
            FROM public.actor;
            """;

        //using NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection);
        using NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = selectQuery;

        using NpgsqlDataReader reader = command.ExecuteReader();

        List<Actor> actors = [];

        while (reader.Read())
        {
            Actor actor = new Actor();
            actors.Add(actor);

            //actor.Id = reader.GetInt32(0);
            actor.Id = (int)reader["actor_id"];
            //actor.Id = reader.GetFieldValue<int>(0);
            //actor.Id = reader.GetInt32(reader.GetOrdinal("actor_id"));
            //actor.Id = reader.GetFieldValue<int>(reader.GetOrdinal("actor_id"));
            actor.FirstName = (string)reader["first_name"];
            actor.LastName = (string)reader["last_name"];
            actor.LastUpdate = (DateTime)reader["last_update"];
        }

        return actors;
    }

    public Actor? GetById(int id)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string selectQuery = $"""
              SELECT 
                  actor_id, 
                  first_name, 
                  last_name, 
                  last_update
              FROM public.actor
              WHERE
                actor_id = @id
              """;

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection);
        command.Parameters.AddWithValue("@id", id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            Actor actor = new Actor();
            actor.Id = (int)reader["actor_id"];
            actor.FirstName = (string)reader["first_name"];
            actor.LastName = (string)reader["last_name"];
            actor.LastUpdate = (DateTime)reader["last_update"];

            return actor;
        }

        return null;
    }


    public void Insert(Actor actor)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        const string query = """
            INSERT INTO public.actor (first_name, last_name)
            VALUES (@firstName, @lastName)
            RETURNING actor_id;
            """;

        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@firstName", actor.FirstName);
        command.Parameters.AddWithValue("@lastName", actor.LastName);

        // Query che non ritorna nessun dato
        //int affectedRows = command.ExecuteNonQuery();

        // Query che ritorna un unico valore
        int newId = (int)command.ExecuteScalar()!;
        actor.Id = newId;
    }

}
