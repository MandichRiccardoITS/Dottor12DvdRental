using Dottor12DvdRental.Models;
using Dottor12DvdRental.Services;

ActorsService actorsService = new ActorsService();

Actor a = new Actor
{
    FirstName = "Pippo",
    LastName = "Pluto"
};
actorsService.Insert(a);

var actors = actorsService.GetList();

foreach (Actor item in actors)
{
    Console.WriteLine($"{item.FirstName} {item.LastName}");
}

Actor? actor = actorsService.GetById(1);

if(actor != null)
{
    Console.WriteLine($"TROVATO {actor.FirstName} {actor.LastName}");
}
else
{
    Console.WriteLine("NON TROVATO");
}