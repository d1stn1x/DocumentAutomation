using System;

namespace DocumentAutomation.Models
{
    public class TemplateVariable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DefaultValue { get; set; }
        public string DataType { get; set; } = "string";
        public DateTime CreatedDate { get; set; }

        // Внешний ключ для связи с шаблоном
        public int? TemplateId { get; set; }

        // Навигационное свойство - ЭТОГО НЕ ХВАТАЛО!
        public virtual DocumentTemplate? Template { get; set; }
    }
}