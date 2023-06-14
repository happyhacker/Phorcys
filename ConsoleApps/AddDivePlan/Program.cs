
using AddDivePlan.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


class Program
{

    private static ScubaContext _context = new ScubaContext();

    static async Task Main(string[] args)
    {
        var divePlan = new DivePlan();
        Console.Write("DiveSiteId ");
        divePlan.DiveSiteId = Int32.Parse(Console.ReadLine());
        Console.Write("Title ");
        divePlan.Title = Console.ReadLine();
        Console.Write("Scheduled date & time ");
        divePlan.ScheduledTime = DateTime.Parse(Console.ReadLine());
        Console.Write("Max Depth ");
        divePlan.MaxDepth = Int32.Parse(Console.ReadLine());
        Console.Write("Notes ");
        divePlan.Notes = Console.ReadLine();
        Console.WriteLine("User ID ");
        divePlan.UserId = Int32.Parse(Console.ReadLine());
        divePlan.Created = DateTime.Now;
        divePlan.LastModified = DateTime.Now;


        AddDivePlan(divePlan );

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

    }

    private static void AddDivePlan(DivePlan divePlan)
    {
        _context.DivePlans.Add(divePlan);
        _context.SaveChanges();
    }

}