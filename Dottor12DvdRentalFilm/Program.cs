using Dottor12DvdRentalFilm.Models;
using Dottor12DvdRentalFilm.Services;
using Spectre.Console;
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
            AnsiConsole.MarkupLine("[green]Hai scelto di visualizzare tutti i film[/]");
            List<Film> films = [.. filmService.GetList()];
            var table = SpectreService.GetFilmView();
            foreach (var film in films)
            {
                SpectreService.AddToTable(film, table);
            }
            AnsiConsole.Write(table);
            break;
        case GetById:
            AnsiConsole.MarkupLine("[green]Hai scelto di visualizzare un film in base all'id[/]");
            break;
        case Insert:
            AnsiConsole.MarkupLine("[green]Hai scelto di aggiungere un nuovo film[/]");
            break;
        case Update:
            AnsiConsole.MarkupLine("[green]Hai scelto di modificare un film[/]");
            break;
        case Delete:
            AnsiConsole.MarkupLine("[green]Hai scelto di eliminare un film[/]");
            break;
        case Count:
            AnsiConsole.MarkupLine("[green]Hai scelto di vedere quanti film ci sono[/]");
            break;
        case GetListFiltered:
            AnsiConsole.MarkupLine("[green]Hai scelto di cercare un film in base ai campi[/]");
            break;
        case Exit:
            AnsiConsole.MarkupLine("[green]Hai scelto di uscire[/]");
            running = false;
            break;
    }
}