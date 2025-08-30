using Microsoft.EntityFrameworkCore;
using ResumeTracker.Models;

namespace ResumeTracker.Data;

public class ResumeContext : DbContext
{
    public DbSet<ResumeEntry> ResumeEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // SQLite 数据库，单文件保存
        optionsBuilder.UseSqlite("Data Source=resumes.db");
    }
}