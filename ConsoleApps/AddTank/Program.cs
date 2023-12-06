using AddTank.Domain;
using AddDivePlan.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AddTank.Domain;
class Program
{

    private static ScubaContext _context = new ScubaContext();

    static async Task Main(string[] args)
    {
        var gear = new Gear();
        var tank = new Tank();

        Console.Write("Title ");
        gear.Title = Console.ReadLine();

        Console.Write("Tank Volume: ");
        tank.Volume = Int32.Parse(Console.ReadLine());

        gear.UserId = 3;
        AddTank(gear, tank);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

    }

    private static void AddTank(Gear gear, Tank tank)
    {
        gear.Tank = tank;
        _context.Gears.Add(gear);
        _context.SaveChanges();

        _context.Gears.Remove(gear);
        _context.SaveChanges();
    }

}