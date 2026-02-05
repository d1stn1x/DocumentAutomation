namespace DocumentAutomation.Models;

public class TemplateVariable
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DefaultValue { get; set; }
    public string? DataType { get; set; } // Text, Number, Date, etc.
    public DateTime CreatedDate { get; set; }
}
