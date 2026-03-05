using System;
using System.Collections.Generic;

namespace DocumentAutomation.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual ICollection<DocumentTemplate> Templates { get; set; } = new List<DocumentTemplate>();
        public virtual ICollection<GeneratedDocument> GeneratedDocuments { get; set; } = new List<GeneratedDocument>();
    }
}