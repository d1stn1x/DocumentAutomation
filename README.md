# test

## Проекты

### DocumentAutomation

Графическое приложение на Windows Forms для автоматизации создания документов на основе шаблонов с интеграцией MS SQL Server.

**Возможности:**
- Управление шаблонами документов в базе данных MS SQL Server
- Автоматическое определение переменных в шаблонах
- Генерация документов на основе введенных данных
- Автоматическое сохранение истории документов в БД
- Сохранение готовых документов в файлы

**Подробнее:** [DocumentAutomation/README.md](DocumentAutomation/README.md)

**Открытие в Visual Studio:**
```
Откройте файл DocumentAutomation.sln в Visual Studio 2022 или новее
```

**Сборка и запуск из командной строки:**
```bash
cd DocumentAutomation
dotnet restore
dotnet build
dotnet run
```

## Технологии

- .NET 10.0
- Windows Forms
- Entity Framework Core 10.0
- MS SQL Server / LocalDB

