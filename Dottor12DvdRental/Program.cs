using Dottor12DvdRentalActor.Models;
using Dottor12DvdRentalActor.Services;

ActorsService actorsService = new ActorsService();

var newActor = new Actor()
{
    FirstName = "Andrea",
    LastName = "Dottor"
};
actorsService.Insert(newActor);
Console.WriteLine($"Attore inserito con id {newActor.Id}");

// Lista
var actors = actorsService.GetList();

foreach (var item in actors)
{
    Console.WriteLine($"{item.FirstName} {item.LastName}");
}

// Dettaglio
Actor? actor = actorsService.GetById(1);
if (actor is not null)
    Console.WriteLine($"TROVATO: {actor.FirstName} {actor.LastName}");
else
    Console.WriteLine("Attore con id 1 non trovato.");

// Update
if (actor is not null)
{
    actor.FirstName = "TEST";
    actor.LastName = "ITS";
    actorsService.Update(actor);
}

// Count
long count = actorsService.Count();
Console.WriteLine($"Actor count pre-delete: {count}");

// Delete
actorsService.Delete(207);

// Count
count = actorsService.Count();
Console.WriteLine($"Actor count post-delete: {count}");

var filteredList = actorsService.GetList("And", "Dot");
Console.WriteLine($"Filtered list: {filteredList.Count()} attori trovati");


Console.WriteLine("END");