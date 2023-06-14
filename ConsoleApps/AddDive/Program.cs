
using AddDivePlan.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


class Program
{

    private static ScubaContext _context = new ScubaContext();

    static async Task Main(string[] args)
    {
        var dive = new Dive();
        Console.Write("DivePlanId ");
        dive.DivePlanId = Int32.Parse(Console.ReadLine());
        Console.Write("Minutes ");
        dive.Minutes = Int32.Parse(Console.ReadLine());
        Console.Write("Descent date & time ");
        dive.DescentTime = DateTime.Parse(Console.ReadLine());
        Console.Write("Average Depth ");
        dive.AvgDepth = Int32.Parse(Console.ReadLine());
        Console.Write("Max Depth ");
        dive.MaxDepth = Int32.Parse(Console.ReadLine());
        Console.Write("Temperature ");
        dive.Temperature = Int32.Parse(Console.ReadLine());
        Console.Write("Additional Weight ");
        dive.AdditionalWeight = Int32.Parse(Console.ReadLine());
        Console.Write("Notes ");
        dive.Notes = Console.ReadLine();
        Console.Write("Dive # ");
        dive.DiveNumber = Int32.Parse(Console.ReadLine());
        Console.WriteLine("User ID ");
        dive.UserId = Int32.Parse(Console.ReadLine());
        dive.Created = DateTime.Now;
        dive.LastModified = DateTime.Now;

        AddDive(dive);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

    }

    private static void AddDive(Dive dive) { 
   
        _context.Dives.Add(dive);
        _context.SaveChanges();
    }

}