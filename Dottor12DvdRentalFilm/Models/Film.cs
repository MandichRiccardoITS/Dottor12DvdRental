using NpgsqlTypes;
using System.Diagnostics;

namespace Dottor12DvdRentalFilm.Models;

[DebuggerDisplay("id:{Id} Title:{Title} Description:{Description} ReleaseYear:{ReleaseYear} LanguageId:{LanguageId} RentalDuration:{RentalDuration}")]
internal class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReleaseYear { get; set; }
    public short LanguageId { get; set; }
    public short RentalDuration { get; set; }
    public decimal RentalRate { get; set; }
    public short Length { get; set; }
    public decimal ReplacementCost { get; set; }
    public string Rating { get; set; }
    public DateTime LastUpdate { get; set; }
    public List<string> SpecialFeatures { get; set; }
    public NpgsqlTsVector FullText { get; set; }
}