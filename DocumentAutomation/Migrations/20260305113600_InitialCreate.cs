using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocumentAutomation.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Templates_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    GeneratedByUserId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedDocuments_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneratedDocuments_Users_GeneratedByUserId",
                        column: x => x.GeneratedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GeneratedDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemplateVariables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "string"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateVariables_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "CreatedDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "#FF5733", new DateTime(2026, 3, 5, 14, 36, 0, 95, DateTimeKind.Local).AddTicks(2749), "Шаблоны договоров", "Договоры" },
                    { 2, "#33FF57", new DateTime(2026, 3, 5, 14, 36, 0, 95, DateTimeKind.Local).AddTicks(2930), "Шаблоны счетов", "Счета" },
                    { 3, "#3357FF", new DateTime(2026, 3, 5, 14, 36, 0, 95, DateTimeKind.Local).AddTicks(2933), "Шаблоны писем", "Письма" },
                    { 4, "#F033FF", new DateTime(2026, 3, 5, 14, 36, 0, 95, DateTimeKind.Local).AddTicks(2935), "Шаблоны актов", "Акты" }
                });

            migrationBuilder.InsertData(
                table: "TemplateVariables",
                columns: new[] { "Id", "CreatedDate", "DataType", "DefaultValue", "Description", "Name", "TemplateId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 5, 14, 36, 0, 95, DateTimeKind.Local).AddTicks(3834), "string", "ООО Ромашка", "Название компании", "COMPANY_NAME", null },
                    { 2, new DateTime(2026, 3, 5, 14, 36, 0, 95, DateTimeKind.Local).AddTicks(3951), "string", "", "Имя клиента", "CLIENT_NAME", null },
                    { 3, new DateTime(2026, 3, 5, 14, 36, 0, 100, DateTimeKind.Local).AddTicks(1393), "date", "05.03.2026", "Дата", "DATE", null },
                    { 4, new DateTime(2026, 3, 5, 14, 36, 0, 100, DateTimeKind.Local).AddTicks(1405), "decimal", "0", "Сумма", "PRICE", null },
                    { 5, new DateTime(2026, 3, 5, 14, 36, 0, 100, DateTimeKind.Local).AddTicks(1407), "string", "001", "Номер договора", "CONTRACT_NUMBER", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FullName", "Username" },
                values: new object[] { 1, new DateTime(2026, 3, 5, 14, 36, 0, 66, DateTimeKind.Local).AddTicks(595), "admin@example.com", "Администратор", "admin" });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedByUserId", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Договор аренды №{{CONTRACT_NUMBER}} от {{DATE}}\n\nКомпания {{COMPANY_NAME}} в лице...", 1, new DateTime(2026, 3, 5, 14, 36, 0, 100, DateTimeKind.Local).AddTicks(3529), "Шаблон договора аренды помещения", null, "Договор аренды" },
                    { 2, 2, "Счет №{{INVOICE_NUMBER}} от {{DATE}}\n\nПлательщик: {{CLIENT_NAME}}\nСумма: {{PRICE}} руб.", 1, new DateTime(2026, 3, 5, 14, 36, 0, 100, DateTimeKind.Local).AddTicks(3637), "Шаблон счета на оплату услуг", null, "Счет на оплату" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocuments_GeneratedByUserId",
                table: "GeneratedDocuments",
                column: "GeneratedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocuments_TemplateId",
                table: "GeneratedDocuments",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDocuments_UserId",
                table: "GeneratedDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CategoryId",
                table: "Templates",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatedByUserId",
                table: "Templates",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateVariables_TemplateId",
                table: "TemplateVariables",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedDocuments");

            migrationBuilder.DropTable(
                name: "TemplateVariables");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
