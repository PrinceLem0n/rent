Проект представляет собой информационную систему для управления прокатом спортивного инвентаря. Она автоматизирует процессы учёта, проката, оплаты и управления клиентами.

Основные функции системы:
- Хранение информации о спортивном инвентаре, клиентах и их корзинах, сотрудниках, поставщиках, заявках, счетов за ущерб.
- Управление прокатом инвентаря.

Технологии:
- **Язык программирования:** C#
- **База данных:** PostgreSQL
- **Фреймворк для интерфейса:** WPF (Windows Presentation Foundation)

Основные компоненты
1. **База данных**
   - Таблицы: Baskets, Bids, Bills, Clients, Employees, Equipments, Suppliers.
   - SQL-запросы для создания таблиц и управления данными.

2. **Интерфейс приложения**
   - Множество окон и страниц, включая:
     - Главное окно
     - Окна для работы с заявками (Bids_page)
     - Окна для добавления, изменения, удаления записей

3. **Функциональность**
   - Регистрация и авторизация пользователей.
   - Просмотр и управление инвентарём.
   - Работа с корзиной и заказами.
   - Управление пользователями и поставщиками.

Установка
1. Убедитесь, что у вас установлены:
   - **PostgreSQL** (для базы данных).
   - **Visual Studio** с установленным расширением для работы с WPF.

2. Склонируйте репозиторий:
   https://github.com/PrinceLem0n/rent.git

3. Настройте подключение к базе данных:
   - Отредактируйте строку подключения в файле `App.config`:
     ```xml
     <connectionStrings>
         <add name="DB" connectionString="Host=localhost;Port=5432;Username=your_username;Password=your_password;Database=rent_db" providerName="Npgsql" />
     </connectionStrings>
     ```

5. Соберите и запустите проект в Visual Studio.
