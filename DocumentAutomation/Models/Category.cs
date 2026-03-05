using System;
using System.Collections.Generic;

namespace DocumentAutomation.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Color { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
        public virtual ICollection<DocumentTemplate> Templates { get; set; } = new List<DocumentTemplate>();
    }
}