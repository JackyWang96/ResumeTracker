using ResumeTracker.Data;
using ResumeTracker.Models;

using var db = new ResumeContext();
db.Database.EnsureCreated();

Console.WriteLine("=== Resume Tracker ===");
Console.WriteLine("1) Add Resume");
Console.WriteLine("2) List Resumes");
Console.WriteLine("q) Quit");

while (true)
{
    Console.Write("\nSelect an option: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Company: ");
            var company = Console.ReadLine() ?? "";

            Console.Write("Position: ");
            var position = Console.ReadLine() ?? "";

            Console.Write("Tech stack: ");
            var tech = Console.ReadLine() ?? "";

            db.ResumeEntries.Add(new ResumeEntry
            {
                Company = company,
                Position = position,
                TechStack = tech,
                AppliedOn = DateTime.UtcNow,
                Source = "Manual",
                Status = "Pending"
            });
            db.SaveChanges();
            Console.WriteLine("✅ Resume added!");
            break;

        case "2":
            var resumes = db.ResumeEntries.ToList();
            foreach (var r in resumes)
            {
                Console.WriteLine($"[{r.Id}] {r.Company} - {r.Position} ({r.Status}) on {r.AppliedOn:d}");
            }
            break;

        case "q":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}