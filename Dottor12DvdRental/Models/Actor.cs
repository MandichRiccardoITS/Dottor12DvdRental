using System.Diagnostics;

namespace Dottor12DvdRentalActor.Models;

[DebuggerDisplay("{Id} {FirstName} {LastName}")]
class Actor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime LastUpdate { get; set; }
}
