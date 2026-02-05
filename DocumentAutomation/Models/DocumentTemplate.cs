namespace DocumentAutomation.Models;

public class DocumentTemplate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? Description { get; set; }
    
    // Foreign keys
    public int? CategoryId { get; set; }
    public int? CreatedByUserId { get; set; }
    
    // Navigation properties
    public Category? Category { get; set; }
    public User? CreatedBy { get; set; }
    public ICollection<GeneratedDocument> GeneratedDocuments { get; set; } = new List<GeneratedDocument>();
}
