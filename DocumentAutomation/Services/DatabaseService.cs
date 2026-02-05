using Microsoft.EntityFrameworkCore;
using DocumentAutomation.Data;
using DocumentAutomation.Models;

namespace DocumentAutomation.Services;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    private DocumentDbContext CreateContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DocumentDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);
        return new DocumentDbContext(optionsBuilder.Options);
    }

    public async Task InitializeDatabaseAsync()
    {
        using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task<List<DocumentTemplate>> GetAllTemplatesAsync()
    {
        using var context = CreateContext();
        return await context.Templates
            .Include(t => t.Category)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<DocumentTemplate?> GetTemplateByIdAsync(int id)
    {
        using var context = CreateContext();
        return await context.Templates
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<int> AddTemplateAsync(DocumentTemplate template)
    {
        using var context = CreateContext();
        template.CreatedDate = DateTime.UtcNow;
        context.Templates.Add(template);
        await context.SaveChangesAsync();
        return template.Id;
    }

    public async Task UpdateTemplateAsync(DocumentTemplate template)
    {
        using var context = CreateContext();
        template.ModifiedDate = DateTime.UtcNow;
        context.Templates.Update(template);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTemplateAsync(int id)
    {
        using var context = CreateContext();
        var template = await context.Templates.FindAsync(id);
        if (template != null)
        {
            context.Templates.Remove(template);
            await context.SaveChangesAsync();
        }
    }

    public async Task<int> SaveGeneratedDocumentAsync(GeneratedDocument document)
    {
        using var context = CreateContext();
        document.GeneratedDate = DateTime.UtcNow;
        context.GeneratedDocuments.Add(document);
        await context.SaveChangesAsync();
        return document.Id;
    }

    public async Task<List<GeneratedDocument>> GetGeneratedDocumentsAsync(int? templateId = null)
    {
        using var context = CreateContext();
        var query = context.GeneratedDocuments.Include(d => d.Template).AsQueryable();
        
        if (templateId.HasValue)
        {
            query = query.Where(d => d.TemplateId == templateId.Value);
        }
        
        return await query.OrderByDescending(d => d.GeneratedDate).ToListAsync();
    }

    public async Task SeedSampleTemplatesAsync()
    {
        using var context = CreateContext();
        
        // Check if templates already exist
        if (await context.Templates.AnyAsync())
        {
            return;
        }

        // Create sample contract template
        var contractTemplate = new DocumentTemplate
        {
            Name = "Договор",
            Description = "Шаблон договора между исполнителем и заказчиком",
            Content = @"ДОГОВОР №{{CONTRACT_NUMBER}}

Дата: {{DATE}}

Между {{COMPANY_NAME}} (Исполнитель) и {{CLIENT_NAME}} (Заказчик)

Предмет договора: {{CONTRACT_SUBJECT}}

Срок выполнения: {{DEADLINE}}

Стоимость: {{PRICE}} руб.

Подпись Исполнителя: ________________
Подпись Заказчика: ________________",
            CreatedDate = DateTime.UtcNow
        };

        // Create sample invoice template
        var invoiceTemplate = new DocumentTemplate
        {
            Name = "Счет",
            Description = "Шаблон счета на оплату",
            Content = @"СЧЕТ НА ОПЛАТУ №{{INVOICE_NUMBER}}

Дата выставления: {{DATE}}

Плательщик: {{CLIENT_NAME}}
ИНН: {{INN}}

Поставщик: {{COMPANY_NAME}}
ИНН: {{COMPANY_INN}}

Наименование товара/услуги: {{SERVICE_NAME}}
Количество: {{QUANTITY}}
Цена: {{UNIT_PRICE}} руб.
Сумма: {{TOTAL_PRICE}} руб.

Итого к оплате: {{TOTAL_PRICE}} руб.",
            CreatedDate = DateTime.UtcNow
        };

        // Create sample letter template
        var letterTemplate = new DocumentTemplate
        {
            Name = "Письмо",
            Description = "Шаблон делового письма",
            Content = @"{{SENDER_NAME}}
{{SENDER_ADDRESS}}

{{DATE}}

Уважаемый(ая) {{RECIPIENT_NAME}},

{{LETTER_BODY}}

С уважением,
{{SENDER_NAME}}
{{SENDER_POSITION}}",
            CreatedDate = DateTime.UtcNow
        };

        context.Templates.AddRange(contractTemplate, invoiceTemplate, letterTemplate);
        await context.SaveChangesAsync();
    }

    // Category methods
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        using var context = CreateContext();
        return await context.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        using var context = CreateContext();
        return await context.Categories.FindAsync(id);
    }

    public async Task<int> AddCategoryAsync(Category category)
    {
        using var context = CreateContext();
        category.CreatedDate = DateTime.UtcNow;
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category.Id;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        using var context = CreateContext();
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        using var context = CreateContext();
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    // TemplateVariable methods
    public async Task<List<TemplateVariable>> GetAllTemplateVariablesAsync()
    {
        using var context = CreateContext();
        return await context.TemplateVariables.OrderBy(v => v.Name).ToListAsync();
    }

    public async Task<int> AddTemplateVariableAsync(TemplateVariable variable)
    {
        using var context = CreateContext();
        variable.CreatedDate = DateTime.UtcNow;
        context.TemplateVariables.Add(variable);
        await context.SaveChangesAsync();
        return variable.Id;
    }
}
