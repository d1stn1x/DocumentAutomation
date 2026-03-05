using System;
using System.Collections.Generic;

namespace DocumentAutomation.Models
{
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
        public virtual Category? Category { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual ICollection<GeneratedDocument> GeneratedDocuments { get; set; } = new List<GeneratedDocument>();

        // ƒќЅј¬Ћя≈ћ коллекцию переменных шаблона
        public virtual ICollection<TemplateVariable> Variables { get; set; } = new List<TemplateVariable>();
    }
}