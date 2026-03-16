<h1 align="center">ReportManager Library</h1>

<h2>Описание</h2>
Библиотека ReportManager предназначена для управления отчетами. Предоставляет функциональность для создания, редактирования, удаления и поиска отчетов по различным критериям. Реализована в виде .NET DLL.

<h2>Классы модели данных</h2>

<h3>Report</h3>
Класс, представляющий сущность отчета.

**Свойства:**
- `string Type` — тип отчета ("финансовый", "аналитический" и т.п.)
- `string User` — пользователь, создавший отчет
- `string Description` — описание отчета
- `DateTime CreatedDate` — дата создания (автоматическая, не редактируется)
- `string Status` — статус отчета ("Завершен", "в процессе" и т.п.)

<h3>ReportState</h3>
Класс, представляющий результат выполнения операций с отчетами.

**Свойства:**
- `bool Success` — флаг успешности операции
- `string Report` — сообщение о результате
- `List<Report> DataList` — список отчетов (для поиска), по умолчанию `null`
- `int Count` — количество отчетов (для подсчета), по умолчанию `-1`

<h2>Класс ReportManager</h2>

<h3>Конструктор</h3>
`ReportManager(Dictionary<int, Report> report)` — инициализирует экземпляр класса со словарем отчетов.

<h3>Методы</h3>

<h4>postReport(string Type, string User, string Description, string Status)</h4>
Создает новый отчет с автоматическим подставлением ID (текущее количество записей + 1) и даты создания.

- **Параметры:** `Type`, `User`, `Description`, `Status`
- **Возвращает:** `ReportState` с результатом создания и сообщением `"created report {id}"`
- **Исключения:** `ArgumentException` при пустых параметрах

<h4>deleteReport(int id)</h4>
Удаляет отчет по идентификатору.

- **Параметры:** `id` — идентификатор отчета
- **Возвращает:** `ReportState` с результатом удаления и сообщением `"delete report {id}"` или `"Report id = {id} NotFound"`
- **Исключения:** `ArgumentException` при id ≤ 0

<h4>patchReport(int id, string Type, string User, string Description, string Status)</h4>
Обновляет существующий отчет (дата создания не изменяется).

- **Параметры:** `id`, `Type`, `User`, `Description`, `Status`
- **Возвращает:** `ReportState` с результатом редактирования и сообщением `"patch report {id} complete"` или `"Report id = {id} NotFound"`
- **Исключения:** `ArgumentException` при id ≤ 0 или пустых параметрах

<h4>getReportId(int id)</h4>
Возвращает отчет по идентификатору.

- **Параметры:** `id` — идентификатор отчета
- **Возвращает:** `ReportState` с найденным отчетом в `DataList` и сообщением `"getReport report {id} complete"` или `"Report id = {id} NotFound"`
- **Исключения:** `ArgumentException` при id ≤ 0

<h4>getReports()</h4>
Возвращает список всех отчетов.

- **Возвращает:** `ReportState` со списком всех отчетов в `DataList` и сообщением `"getReport reports complete"` или `"NotFound reports"`

<h4>getReportsUser(string UserId)</h4>
Возвращает все отчеты указанного пользователя.

- **Параметры:** `UserId` — идентификатор пользователя
- **Возвращает:** `ReportState` с отфильтрованным списком отчетов в `DataList` и сообщением `"getReport reports complete"` или `"Reports UserId = {UserId} NotFound"`

<h4>getReportsType(string Type)</h4>
Возвращает все отчеты указанного типа.

- **Параметры:** `Type` — тип отчета
- **Возвращает:** `ReportState` с отфильтрованным списком отчетов в `DataList` и сообщением `"getReport reports complete"` или `"Reports Type = {Type} NotFound"`

<h4>getReportsStatus(string Status)</h4>
Возвращает все отчеты с указанным статусом.

- **Параметры:** `Status` — статус отчета
- **Возвращает:** `ReportState` с отфильтрованным списком отчетов в `DataList` и сообщением `"getReport reports complete"` или `"Reports Status = {Status} NotFound"`

<h4>getReportsDate(DateTime dateCreate)</h4>
Возвращает все отчеты, созданные в указанную дату.

- **Параметры:** `dateCreate` — дата создания
- **Возвращает:** `ReportState` с отфильтрованным списком отчетов в `DataList` и сообщением `"getReport reports complete"` или `"Reports CreatedDate = {dateCreate} NotFound"`

<h4>getReportsCount()</h4>
Возвращает общее количество отчетов.

- **Возвращает:** `ReportState` с количеством отчетов в свойстве `Count` и сообщением `"найдено {count} записей"`

<h2>Тестирование</h2>
Набор автоматизированных тестов (NUnit) в классе `TestReportManager`. Перед каждым тестом создается словарь с 5 тестовыми отчетами.

- `ReportManager_postReport_Success` — успешное создание отчета
- `ReportManager_postReport_EmptyType_Failure` — исключение при пустом типе
- `ReportManager_postReport_EmptyUser_Failure` — исключение при пустом пользователе
- `ReportManager_postReport_EmptyDescription_Failure` — исключение при пустом описании
- `ReportManager_deleteReport_Success` — успешное удаление отчета
- `ReportManager_deleteReport_NotFoundId_Failure` — удаление несуществующего отчета
- `ReportManager_deleteReport_NegativeId_Failure` — исключение при отрицательном ID
- `ReportManager_getReports_Success` — получение всех отчетов (5 шт.)
- `ReportManager_getReports_EmptyResponse_Failure` — получение отчетов при пустом словаре
- `ReportManager_getReport_Success` — поиск по существующему ID
- `ReportManager_getReportId_NotFoundId_Failure` — поиск по несуществующему ID
- `ReportManager_getReportId_NegativeId_Failure` — исключение при отрицательном ID
- `ReportManager_getReportType_Success` — поиск по существующему типу (3 отчета)
- `ReportManager_getReportType_NotFoundType_Failure` — поиск по несуществующему типу
- `ReportManager_patchReport_Success` — успешное редактирование отчета
- `ReportManager_patchReport_NotFoundId_Failure` — редактирование несуществующего отчета
- `ReportManager_getReportsUser_Success` — поиск по существующему пользователю (2 отчета)
- `ReportManager_getReportsUser_NotFoundUser_Failure` — поиск по несуществующему пользователю
- `ReportManager_getReportsStatus_Success` — поиск по существующему статусу (3 отчета)
- `ReportManager_getReportsStatus_NotFoundStatus_Failure` — поиск по несуществующему статусу
- `ReportManager_getReportsDate_Success` — поиск по существующей дате
- `ReportManager_getReportsDate_NotFoundDate_Failure` — поиск по несуществующей дате
- `ReportManager_getReportsCount_Success` — подсчет количества отчетов (5 шт.)
- `ReportManager_getReportsCount_EmptyResponse_Success` — подсчет при пустом словаре (0 шт.)