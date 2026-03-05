using Dottor12DvdRental.Models;
using Dottor12DvdRental.Services;

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


Console.WriteLine("END");

