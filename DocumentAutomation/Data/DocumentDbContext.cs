using Microsoft.EntityFrameworkCore;
using DocumentAutomation.Models;

namespace DocumentAutomation.Data;

public class DocumentDbContext : DbContext
{
    private readonly string? _connectionString;

    // Конструктор для использования в приложении (с опциями)
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
        : base(options)
    {
    }

    // Конструктор для ручного создания (с передачей строки подключения)
    public DocumentDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    // КОНСТРУКТОР БЕЗ ПАРАМЕТРОВ ДЛЯ МИГРАЦИЙ
    public DocumentDbContext()
    {
        // Строка подключения по умолчанию для миграций
        // ЗАМЕНИТЕ НА ВАШУ РЕАЛЬНУЮ СТРОКУ ПОДКЛЮЧЕНИЯ!
        _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DocumentAutomationDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
    }

    public DbSet<DocumentTemplate> Templates { get; set; }
    public DbSet<GeneratedDocument> GeneratedDocuments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TemplateVariable> TemplateVariables { get; set; }

    // Настройка подключения (важно для миграций)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Если подключение еще не настроено и есть строка подключения
        if (!optionsBuilder.IsConfigured && _connectionString != null)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // === НАСТРОЙКА ДЛЯ User ===
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()"); // Автоматическая дата

            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // === НАСТРОЙКА ДЛЯ Category ===
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");

            // Уникальное имя категории
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // === НАСТРОЙКА ДЛЯ TemplateVariable ===
        modelBuilder.Entity<TemplateVariable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DefaultValue).HasMaxLength(500);
            entity.Property(e => e.DataType).HasMaxLength(50).HasDefaultValue("string");
            entity.Property(e => e.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");

            // Связь с шаблоном (если нужно)
            entity.HasOne(e => e.Template)
                  .WithMany(t => t.Variables)
                  .HasForeignKey(e => e.TemplateId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // === НАСТРОЙКА ДЛЯ DocumentTemplate ===
        modelBuilder.Entity<DocumentTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Description).HasMaxLength(500);

            // Связь с категорией
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Templates)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.SetNull);

            // Связь с пользователем
            entity.HasOne(e => e.CreatedBy)
                  .WithMany(u => u.Templates) // Предполагаем, что у User есть коллекция Templates
                  .HasForeignKey(e => e.CreatedByUserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // === НАСТРОЙКА ДЛЯ GeneratedDocument ===
        modelBuilder.Entity<GeneratedDocument>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DocumentName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.GeneratedDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");

            // Связь с шаблоном
            entity.HasOne(e => e.Template)
                  .WithMany(t => t.GeneratedDocuments)
                  .HasForeignKey(e => e.TemplateId)
                  .OnDelete(DeleteBehavior.Restrict); // Не удалять документы при удалении шаблона

            // Связь с пользователем (кто сгенерировал)
            entity.HasOne(e => e.GeneratedBy)
                  .WithMany()
                  .HasForeignKey(e => e.GeneratedByUserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // === НАЧАЛЬНЫЕ ДАННЫЕ (SEED DATA) ===
        // Добавляем пользователя по умолчанию
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                FullName = "Администратор",
                Email = "admin@example.com",
                CreatedDate = DateTime.Now
            }
        );

        // Добавляем категории
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Договоры", Description = "Шаблоны договоров", Color = "#FF5733", CreatedDate = DateTime.Now },
            new Category { Id = 2, Name = "Счета", Description = "Шаблоны счетов", Color = "#33FF57", CreatedDate = DateTime.Now },
            new Category { Id = 3, Name = "Письма", Description = "Шаблоны писем", Color = "#3357FF", CreatedDate = DateTime.Now },
            new Category { Id = 4, Name = "Акты", Description = "Шаблоны актов", Color = "#F033FF", CreatedDate = DateTime.Now }
        );

        // Добавляем переменные
        modelBuilder.Entity<TemplateVariable>().HasData(
            new TemplateVariable { Id = 1, Name = "COMPANY_NAME", Description = "Название компании", DataType = "string", DefaultValue = "ООО Ромашка", CreatedDate = DateTime.Now },
            new TemplateVariable { Id = 2, Name = "CLIENT_NAME", Description = "Имя клиента", DataType = "string", DefaultValue = "", CreatedDate = DateTime.Now },
            new TemplateVariable { Id = 3, Name = "DATE", Description = "Дата", DataType = "date", DefaultValue = DateTime.Now.ToString("dd.MM.yyyy"), CreatedDate = DateTime.Now },
            new TemplateVariable { Id = 4, Name = "PRICE", Description = "Сумма", DataType = "decimal", DefaultValue = "0", CreatedDate = DateTime.Now },
            new TemplateVariable { Id = 5, Name = "CONTRACT_NUMBER", Description = "Номер договора", DataType = "string", DefaultValue = "001", CreatedDate = DateTime.Now }
        );

        // Добавляем шаблоны
        modelBuilder.Entity<DocumentTemplate>().HasData(
            new DocumentTemplate
            {
                Id = 1,
                Name = "Договор аренды",
                Description = "Шаблон договора аренды помещения",
                Content = "Договор аренды №{{CONTRACT_NUMBER}} от {{DATE}}\n\nКомпания {{COMPANY_NAME}} в лице...",
                CategoryId = 1,
                CreatedByUserId = 1,
                CreatedDate = DateTime.Now
            },
            new DocumentTemplate
            {
                Id = 2,
                Name = "Счет на оплату",
                Description = "Шаблон счета на оплату услуг",
                Content = "Счет №{{INVOICE_NUMBER}} от {{DATE}}\n\nПлательщик: {{CLIENT_NAME}}\nСумма: {{PRICE}} руб.",
                CategoryId = 2,
                CreatedByUserId = 1,
                CreatedDate = DateTime.Now
            }
        );
    }
}