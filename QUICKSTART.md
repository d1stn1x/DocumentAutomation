# Краткая инструкция по запуску

## Для пользователя Visual Studio

1. **Откройте проект**
   - Дважды кликните на файл `DocumentAutomation.sln`
   - Visual Studio откроет проект автоматически

2. **Запустите приложение**
   - Нажмите клавишу `F5`
   - Или нажмите кнопку ▶️ "Start" на панели инструментов

3. **При первом запуске**
   - База данных создастся автоматически (используется LocalDB)
   - Будут добавлены 3 примера шаблонов

## Если LocalDB не установлен

LocalDB входит в состав Visual Studio. Если возникла ошибка:

1. Откройте **Visual Studio Installer**
2. Нажмите **Modify** (Изменить)
3. Перейдите в **Individual Components** (Отдельные компоненты)
4. Найдите и включите: **SQL Server Express LocalDB**
5. Нажмите **Modify** для установки

## Альтернатива: SQL Server Express

Если хотите использовать SQL Server Express вместо LocalDB:

1. Установите [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
2. Откройте файл `DocumentAutomation\appsettings.json`
3. Измените строку подключения:
   ```json
   "DefaultConnection": "Server=.\\SQLEXPRESS;Database=DocumentAutomationDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   ```

## Устранение проблем

### Ошибка: "Не удается подключиться к базе данных"
- Убедитесь, что LocalDB или SQL Server установлен и запущен
- Проверьте строку подключения в `appsettings.json`

### Ошибка при сборке
- В Visual Studio: **Build → Rebuild Solution**
- Убедитесь, что установлен .NET 10 SDK

## Возможности приложения

После запуска вы можете:
- ✅ Выбирать шаблоны из списка слева
- ✅ Заполнять переменные в центральной панели
- ✅ Генерировать документы (автоматически сохраняются в БД)
- ✅ Экспортировать документы в файлы
- ✅ Просматривать историю в базе данных

---

**Версия**: 2.0  
**Технологии**: .NET 10.0, Windows Forms, Entity Framework Core, MS SQL Server
