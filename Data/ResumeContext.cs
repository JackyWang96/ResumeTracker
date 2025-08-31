using Microsoft.EntityFrameworkCore;
using ResumeTracker.Models;

namespace ResumeTracker.Data;

public class ResumeContext : DbContext
{
    public DbSet<ResumeEntry> ResumeEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // SQLite 数据库，单文件保存
        var dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "resumes.db"));
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResumeEntry>().HasData(
            new ResumeEntry { Id = 1, Company = "Microsoft", Position = "Software Engineer", TechStack = "C#, Azure", AppliedOn = DateTime.UtcNow.AddDays(-20), Source = "LinkedIn", Status = "Pending", JobLevel = "Junior" },
            new ResumeEntry { Id = 2, Company = "Google", Position = "Backend Developer", TechStack = "Go, Kubernetes", AppliedOn = DateTime.UtcNow.AddDays(-10), Source = "Website", Status = "Interview", JobLevel = "Mid" },
            new ResumeEntry { Id = 3, Company = "Amazon", Position = "Cloud Engineer", TechStack = "AWS, Python", AppliedOn = DateTime.UtcNow.AddDays(-5), Source = "Referral", Status = "Rejected", JobLevel = "Senior" },
            new ResumeEntry { Id = 4, Company = "Atlassian", Position = "Fullstack Developer", TechStack = "React, Node.js", AppliedOn = DateTime.UtcNow.AddDays(-2), Source = "LinkedIn", Status = "Pending", JobLevel = "Mid" }
        );

    }
}