namespace ResumeTracker.Models;

public class ResumeEntry
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string TechStack { get; set; } = string.Empty;   // e.g. "C#, SQL, Azure"
    public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
    public string Source { get; set; } = string.Empty;     // LinkedIn / Website
    public string Status { get; set; } = "Pending";        // Pending / Interview / Offer / Rejected
    public string JobLevel { get; set; } = string.Empty;
}