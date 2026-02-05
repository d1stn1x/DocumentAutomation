namespace DocumentAutomation.Models;

public class GeneratedDocument
{
    public int Id { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime GeneratedDate { get; set; }
    public int TemplateId { get; set; }
    public DocumentTemplate? Template { get; set; }
}
