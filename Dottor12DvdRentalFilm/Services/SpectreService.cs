using Dottor12DvdRentalFilm.Models;
using Spectre.Console;

namespace Dottor12DvdRentalFilm.Services;

internal class SpectreService
{
    public static void AddToTable(Film film, Table table)
    {
        table.AddRow(
            film.Id.ToString(),
            film.Title,
            film.Description,
            film.ReleaseYear.ToString(),
            film.LanguageId.ToString(),
            film.RentalDuration.ToString(),
            film.RentalRate.ToString(),
            film.Length.ToString(),
            film.ReplacementCost.ToString(),
            film.Rating,
            film.LastUpdate.ToString("yyyy-MM-dd HH:mm:ss"),
            string.Join(", ", film.SpecialFeatures)
        );
    }

    public static Table GetFilmView()
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Title");
        table.AddColumn("Description");
        table.AddColumn("Release Year");
        table.AddColumn("Language Id");
        table.AddColumn("Rental Duration");
        table.AddColumn("Rental Rate");
        table.AddColumn("Length");
        table.AddColumn("Replacement Cost");
        table.AddColumn("Rating");
        table.AddColumn("Last Update");
        table.AddColumn("Special Features");
        table.ShowRowSeparators = true;
        return table;
    }
}
