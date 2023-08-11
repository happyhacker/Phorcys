
using AddDivePlan.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


class Program
{

    private static ScubaContext _context = new ScubaContext();

    static async Task Main(string[] args)
    {
        var tanksOnDive = new TanksOnDive();
        Console.Write("DivePlanId ");
        tanksOnDive.DivePlanId = Int32.Parse(Console.ReadLine());
        Console.Write("GearId ");
        tanksOnDive.GearId = Int32.Parse(Console.ReadLine());
        Console.Write("Content Title ");
        tanksOnDive.GasContentTitle = Console.ReadLine();
        Console.Write("Starting Pressure ");
        tanksOnDive.StartingPressure = Int32.Parse(Console.ReadLine());
        Console.Write("Ending Pressure ");
        tanksOnDive.EndingPressure = Int32.Parse(Console.ReadLine());
        AddTank(tanksOnDive);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

    }

    private static void AddTank(TanksOnDive tanksOnDive) { 
   
        _context.TanksOnDives.Add(tanksOnDive);
        _context.SaveChanges();
    }

}