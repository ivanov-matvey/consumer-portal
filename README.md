# Consumer Portal

## Technologies

![C#](https://img.shields.io/badge/C%23-262626?style=flat-square)
![.NET 6](https://img.shields.io/badge/.NET-6-333333?style=flat-square&logo=dotnet&logoColor=white&labelColor=262626)
![ASP.NET Core 6](https://img.shields.io/badge/ASP.NET_Core-6-333333?style=flat-square&logo=dotnet&logoColor=white&labelColor=262626)
<br>
![SQL Server](https://img.shields.io/badge/SQL_Server-262626?style=flat-square)
<br>
![JavaScript](https://img.shields.io/badge/JavaScript-262626?style=flat-square&logo=javascript&logoColor=white)
![Vue.js 2](https://img.shields.io/badge/Vue.js-2-333333?style=flat-square&logo=vuedotjs&logoColor=white&labelColor=262626)

## Запуск и проверка

### Требования

- .NET SDK 6.0 или новее;
- Node.js и npm;
- локальный SQL Server, доступный по `localhost` с Windows-аутентификацией.

Строка подключения находится в `backend/ConsumerPortal.Api/appsettings.json`. Базу данных изменяют только миграции EF Core; SSMS используйте только для просмотра и проверки.

### Миграции и база данных

Перейдите в папку API:

```powershell
cd C:\consumer-portal\backend\ConsumerPortal.Api
```

Если `dotnet ef` ещё не установлен, установите версию для EF Core 6:

```powershell
dotnet tool install --global dotnet-ef --version 6.*
```

Примените все миграции к локальной базе:

```powershell
dotnet ef database update
```

При изменении модели создавайте новую миграцию и затем применяйте её:

```powershell
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

Полный сброс базы для финальной проверки удаляет все локальные данные:

```powershell
dotnet ef database drop --force
dotnet ef database update
```

После применения миграций в SSMS можно проверить таблицы `Roles`, `Users`, `Companies`, `Claims`, внешние ключи и индексы. Не изменяйте схему и данные вручную через SSMS.

### Запуск приложений

Запустите API в первом терминале:

```powershell
cd C:\consumer-portal\backend\ConsumerPortal.Api
dotnet run
```

Swagger: <https://localhost:7079/swagger>

Во втором терминале запустите фронтенд:

```powershell
cd C:\consumer-portal\frontend
npm install
npm run serve
```

Приложение: <http://localhost:8080>

## Проверка сценария недели 6

1. Откройте каталог. В нём отображается по 3 организации на странице; кнопка «Вперёд» открывает следующую страницу.
2. Введите `77` в поле поиска: фильтр выполняется по названию и ИНН, без ошибки валидации.
3. Откройте карточку организации. Жалобы также выдаются по 3 на странице; пагинация появится после создания четвёртой жалобы для одной организации.
4. Зарегистрируйте нового потребителя, войдите автоматически и создайте жалобу. Она должна появиться в профиле и в карточке выбранной организации.
5. Выйдите и войдите модератором:

   - email: `moderator@consumer-portal.local`
   - пароль: `Moderator123!`

   Откройте «Модерацию», отфильтруйте жалобы и смените статус созданной жалобы.
6. В SSMS убедитесь, что жалоба сохранилась в `Claims`, её `UserId` и `CompanyId` корректны, а статус изменён. Для финальной сдачи создайте стандартную Database Diagram с четырьмя таблицами.

При истёкшем или неверном JWT клиент очищает сессию и перенаправляет на страницу входа. Необработанная серверная ошибка возвращается API в JSON и отображается в приложении.
