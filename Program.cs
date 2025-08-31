using Microsoft.EntityFrameworkCore;
using ResumeTracker.Data;
using ResumeTracker.Models;

using var db = new ResumeContext();
db.Database.Migrate(); 

while (true)
{
    Console.WriteLine("\n=== Resume Tracker ===");
    Console.WriteLine("1) Add Resume");
    Console.WriteLine("2) List Resumes");
    Console.WriteLine("3) Update Status");
    Console.WriteLine("4) Update JobLevel");
    Console.WriteLine("q) Quit");
    
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

        case "3":
            UpdateStatus(db);
            break;

        case "4":
            UpdateJobLevel(db);
            break;

        case "q":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

static void UpdateStatus(ResumeContext db)
{
    Console.Write("Enter Resume Id: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid Id");
        return;
    }

    var resume = db.ResumeEntries.FirstOrDefault(r => r.Id == id);
    if (resume is null)
    {
        Console.WriteLine("❌ Resume not found");
        return;
    }

    var options = new[] { "Pending", "Interview", "Offer", "Rejected" };
    Console.WriteLine("Select new Status:");
    for (int i = 0; i < options.Length; i++)
        Console.WriteLine($"{i + 1}) {options[i]}");

    Console.Write("> ");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > options.Length)
    {
        Console.WriteLine("❌ Invalid choice");
        return;
    }

    resume.Status = options[choice - 1];
    db.SaveChanges();
    Console.WriteLine($"✅ Status updated to {resume.Status}");
}

static void UpdateJobLevel(ResumeContext db)
{
    Console.Write("Enter Resume Id: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid Id");
        return;
    }

    var resume = db.ResumeEntries.FirstOrDefault(r => r.Id == id);
    if (resume is null)
    {
        Console.WriteLine("❌ Resume not found");
        return;
    }

    var levels = new[] { "Unknown", "Junior", "Mid", "Senior", "Lead" };
    Console.WriteLine("Select new JobLevel:");
    for (int i = 0; i < levels.Length; i++)
        Console.WriteLine($"{i + 1}) {levels[i]}");

    Console.Write("> ");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > levels.Length)
    {
        Console.WriteLine("❌ Invalid choice");
        return;
    }

    resume.JobLevel = levels[choice - 1];
    db.SaveChanges();
    Console.WriteLine($"✅ JobLevel updated to {resume.JobLevel}");
    Console.WriteLine($"[{resume.Id}] {resume.Company} - {resume.Position} ({resume.Status}, {resume.JobLevel}) on {resume.AppliedOn:d}");
}
