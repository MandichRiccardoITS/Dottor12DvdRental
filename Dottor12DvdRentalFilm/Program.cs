using Dottor12DvdRentalFilm.Models;
using Dottor12DvdRentalFilm.Services;
using NpgsqlTypes;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        const string GetList = "Visualizzare tutti i film";
        const string GetById = "Visualizzare un film in base all'id";
        const string Insert = "aggiungere un nuovo film";
        const string Update = "modificare un film";
        const string Delete = "eliminare un film";
        const string Count = "vedere quanti film ci sono";
        const string GetListFiltered = "cercare un film in base ai campi";
        const string Exit = "uscire";

        string[] choices = [
            GetList,
    GetById,
    Insert,
    Update,
    Delete,
    Count,
    GetListFiltered,
    Exit
        ];

        var menu = new SelectionPrompt<string>()
            .Title("Cosa vuoi fare?")
            .AddChoices(choices);

        var filmService = new FilmService();

        bool running = true;

        while (running)
        {
            var selection = AnsiConsole.Prompt(menu);

            switch (selection)
            {
                case GetList:
                    {

                        var films = filmService.GetList().ToList();

                        var table = SpectreService.GetFilmView();

                        foreach (var film in films)
                        {
                            SpectreService.AddToTable(film, table);
                        }

                        AnsiConsole.Write(table);

                        break;
                    }
                case GetById:
                    {

                        int id = AnsiConsole.Ask<int>("Inserisci l'id del film:");

                        var film = filmService.GetById(id);

                        if (film == null)
                        {
                            AnsiConsole.MarkupLine("[red]Film non trovato[/]");
                        }
                        else
                        {
                            var table = SpectreService.GetFilmView();
                            SpectreService.AddToTable(film, table);
                            AnsiConsole.Write(table);
                        }

                        break;
                    }
                case Insert:
                    {

                        var newFilm = new Film();

                        newFilm.Title = AnsiConsole.Ask<string>("Titolo:");
                        newFilm.Description = AnsiConsole.Ask<string>("Descrizione:");
                        newFilm.ReleaseYear = AnsiConsole.Ask<int>("Anno di uscita:");
                        newFilm.LanguageId = AnsiConsole.Ask<short>("Language id:");
                        newFilm.RentalDuration = AnsiConsole.Ask<short>("Durata noleggio:");
                        newFilm.RentalRate = AnsiConsole.Ask<decimal>("Costo noleggio:");
                        newFilm.Length = AnsiConsole.Ask<short>("Durata film:");
                        newFilm.ReplacementCost = AnsiConsole.Ask<decimal>("Costo sostituzione:");
                        newFilm.Rating = AnsiConsole.Ask<string>("Rating:");

                        newFilm.SpecialFeatures = [];
                        newFilm.FullText = null!;

                        filmService.Insert(newFilm);

                        AnsiConsole.MarkupLine($"[green]Film inserito con id {newFilm.Id}[/]");

                        break;
                    }
                case Update:
                    {
                        int updateId = AnsiConsole.Ask<int>("Id del film da modificare:");

                        var filmToUpdate = filmService.GetById(updateId);

                        if (filmToUpdate == null)
                        {
                            AnsiConsole.MarkupLine("[red]Film non trovato[/]");
                            break;
                        }

                        filmToUpdate.Title = AnsiConsole.Ask<string>($"Titolo ({filmToUpdate.Title}):");
                        filmToUpdate.Description = AnsiConsole.Ask<string>($"Descrizione ({filmToUpdate.Description}):");
                        filmToUpdate.ReleaseYear = AnsiConsole.Ask<int>($"Anno ({filmToUpdate.ReleaseYear}):");
                        filmToUpdate.LanguageId = AnsiConsole.Ask<short>($"Language id ({filmToUpdate.LanguageId}):");
                        filmToUpdate.RentalDuration = AnsiConsole.Ask<short>($"Durata noleggio ({filmToUpdate.RentalDuration}):");
                        filmToUpdate.RentalRate = AnsiConsole.Ask<decimal>($"Costo noleggio ({filmToUpdate.RentalRate}):");
                        filmToUpdate.Length = AnsiConsole.Ask<short>($"Durata film ({filmToUpdate.Length}):");
                        filmToUpdate.ReplacementCost = AnsiConsole.Ask<decimal>($"Costo sostituzione ({filmToUpdate.ReplacementCost}):");
                        filmToUpdate.Rating = AnsiConsole.Ask<string>($"Rating ({filmToUpdate.Rating}):");

                        filmService.Update(filmToUpdate);

                        AnsiConsole.MarkupLine("[green]Film aggiornato[/]");

                        break;
                    }
                case Delete:
                    {
                        int deleteId = AnsiConsole.Ask<int>("Id del film da eliminare:");

                        filmService.Delete(deleteId);

                        AnsiConsole.MarkupLine("[green]Film eliminato[/]");

                        break;
                    }
                case Count:
                    {
                        long count = filmService.Count();

                        AnsiConsole.MarkupLine($"[yellow]Numero totale film: {count}[/]");

                        break;
                    }
                case GetListFiltered:
                    {
                        string? title = AnsiConsole.Ask("Titolo (invio per saltare):", "");

                        if (string.IsNullOrWhiteSpace(title))
                            title = null;

                        var filteredFilms = filmService.GetList(title: title).ToList();

                        var filteredTable = SpectreService.GetFilmView();

                        foreach (var f in filteredFilms)
                        {
                            SpectreService.AddToTable(f, filteredTable);
                        }

                        AnsiConsole.Write(filteredTable);

                        break;
                    }
                case Exit:
                    {
                        running = false;

                        break;
                    }
            }
        }
    }
}