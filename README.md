# ReportManager Library

## Описание
Библиотека ReportManager предназначена для управления отчетами. Предоставляет функциональность для создания, редактирования, удаления и поиска отчетов по различным критериям. Реализована в виде .NET DLL.

## Классы модели данных

### Report
Класс, представляющий сущность отчета.

**Свойства:**
- `string Type` — тип отчета
- `string User` — пользователь, создавший отчет
- `string Description` — описание отчета
- `DateTime CreatedDate` — дата создания (автоматическая, не редактируется)
- `string Status` — статус отчета

### ReportState
Класс, представляющий результат выполнения операций с отчетами.

**Свойства:**
- `bool Success` — флаг успешности операции
- `string Report` — сообщение о результате
- `List<Report> DataList` — список отчетов (для поиска)
- `int Count` — количество отчетов (для подсчета)

## Класс ReportManager

### Конструктор
`ReportManager(Dictionary<int, Report> report)` — инициализирует экземпляр класса с словарем отчетов.

### Методы

#### postReport(string Type, string User, string Description, string Status)
Создает новый отчет с автоматической генерацией ID и даты создания.

- **Параметры:** `Type`, `User`, `Description`, `Status`

- **Возвращает:** `ReportState` с результатом создания

- **Исключения:** `ArgumentException` при пустых параметрах

#### deleteReport(int id)
Удаляет отчет по идентификатору.

- **Параметры:** `id` — идентификатор отчета

- **Возвращает:** `ReportState` с результатом удаления

- **Исключения:** `ArgumentException` при id ≤ 0

#### patchReport(int id, string Type, string User, string Description, string Status)
Обновляет существующий отчет (дата создания не изменяется).

- **Параметры:** `id`, `Type`, `User`, `Description`, `Status`

- **Возвращает:** `ReportState` с результатом редактирования

- **Исключения:** `ArgumentException` при id ≤ 0 или пустых параметрах

#### getReportId(int id)
Возвращает отчет по идентификатору.

- **Параметры:** `id` — идентификатор отчета

- **Возвращает:** `ReportState` с найденным отчетом в `DataList`

- **Исключения:** `ArgumentException` при id ≤ 0

#### getReports()
Возвращает список всех отчетов.

- **Возвращает:** `ReportState` со списком всех отчетов в `DataList`

#### getReportsUser(string UserId)
Возвращает все отчеты указанного пользователя.

- **Параметры:** `UserId` — идентификатор пользователя

- **Возвращает:** `ReportState` с отфильтрованным списком отчетов

#### getReportsType(string Type)
Возвращает все отчеты указанного типа.

- **Параметры:** `Type` — тип отчета

- **Возвращает:** `ReportState` с отфильтрованным списком отчетов

#### getReportsStatus(string Status)
Возвращает все отчеты с указанным статусом.

- **Параметры:** `Status` — статус отчета

- **Возвращает:** `ReportState` с отфильтрованным списком отчетов

#### getReportsDate(DateTime dateCreate)
Возвращает все отчеты, созданные в указанную дату.

- **Параметры:** `dateCreate` — дата создания

- **Возвращает:** `ReportState` с отфильтрованным списком отчетов

#### getReportsCount()
Возвращает общее количество отчетов.

- **Возвращает:** `ReportState` с количеством отчетов в свойстве `Count`

## Тестирование
Набор автоматизированных тестов (NUnit) в классе `TestReportManager`. Перед каждым тестом создается словарь с 5 тестовыми отчетами.

- `ReportManager_postOrder_Success` — успешное создание отчета
- `ReportManager_postOrder_EmptyType_Failure` — исключение при пустом типе
- `ReportManager_postOrder_EmptyUser_Failure` — исключение при пустом пользователе
- `ReportManager_postOrder_EmptyDescription_Failure` — исключение при пустом описании
- `ReportManager_deleteOrder_Success` — успешное удаление отчета
- `ReportManager_deleteOrder_NotFoundId_Failure` — удаление несуществующего отчета
- `ReportManager_deleteOrder_NegativeId_Failure` — исключение при отрицательном ID
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
- `ReportManager_getReportsStatus_NotFoundSataus_Failure` — поиск по несуществующему статусу
- `ReportManager_getReportsDate_Success` — поиск по существующей дате
- `ReportManager_getReportsDate_NotFoundDate_Failure` — поиск по несуществующей дате
- `ReportManager_getReportsCount_Success` — подсчет количества отчетов (5 шт.)
- `ReportManager_getReportsCount_EmptyResponse_Success` — подсчет при пустом словаре (0 шт.)