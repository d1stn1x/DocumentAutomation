using System;

namespace DocumentAutomation.Models
{
    public class GeneratedDocument
    {
        public int Id { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime GeneratedDate { get; set; }

        // Foreign keys
        public int TemplateId { get; set; }
        public int? GeneratedByUserId { get; set; }

        // Navigation properties
        public virtual DocumentTemplate? Template { get; set; }
        public virtual User? GeneratedBy { get; set; }
    }
}