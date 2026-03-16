<h1 align="center">ReportManager Library</h1>

<h2>Описание</h2>
Библиотека ReportManager предназначена для управления отчетами. Предоставляет функциональность для создания, редактирования, удаления и поиска отчетов по различным критериям. Реализована в виде .NET DLL.

<h2>Классы модели данных</h2>

<h3>Report</h3>
Класс, представляющий сущность отчета.

<p><strong>Свойства:</strong></p>
<ul>
    <li><code>string Type</code> — тип отчета</li>
    <li><code>string User</code> — пользователь, создавший отчет</li>
    <li><code>string Description</code> — описание отчета</li>
    <li><code>DateTime CreatedDate</code> — дата создания (автоматическая, не редактируется)</li>
    <li><code>string Status</code> — статус отчета</li>
</ul>

<h3>ReportState</h3>
Класс, представляющий результат выполнения операций с отчетами.

<p><strong>Свойства:</strong></p>
<ul>
    <li><code>bool Success</code> — флаг успешности операции</li>
    <li><code>string Report</code> — сообщение о результате</li>
    <li><code>List&lt;Report&gt; DataList</code> — список отчетов (для поиска)</li>
    <li><code>int Count</code> — количество отчетов (для подсчета)</li>
</ul>

<h2>Класс ReportManager</h2>

<h3>Конструктор</h3>
<p><code>ReportManager(Dictionary&lt;int, Report&gt; report)</code> — инициализирует экземпляр класса с словарем отчетов.</p>

<h3>Методы</h3>

<h4>postReport(string Type, string User, string Description, string Status)</h4>
<p>Создает новый отчет с автоматической генерацией ID и даты создания.</p>
<p><strong>Параметры:</strong> Type, User, Description, Status</p>
<p><strong>Возвращает:</strong> ReportState с результатом создания</p>
<p><strong>Исключения:</strong> ArgumentException при пустых параметрах</p>

<h4>deleteReport(int id)</h4>
<p>Удаляет отчет по идентификатору.</p>
<p><strong>Параметры:</strong> id — идентификатор отчета</p>
<p><strong>Возвращает:</strong> ReportState с результатом удаления</p>
<p><strong>Исключения:</strong> ArgumentException при id ≤ 0</p>

<h4>patchReport(int id, string Type, string User, string Description, string Status)</h4>
<p>Обновляет существующий отчет (дата создания не изменяется).</p>
<p><strong>Параметры:</strong> id, Type, User, Description, Status</p>
<p><strong>Возвращает:</strong> ReportState с результатом редактирования</p>
<p><strong>Исключения:</strong> ArgumentException при id ≤ 0 или пустых параметрах</p>

<h4>getReportId(int id)</h4>
<p>Возвращает отчет по идентификатору.</p>
<p><strong>Параметры:</strong> id — идентификатор отчета</p>
<p><strong>Возвращает:</strong> ReportState с найденным отчетом в DataList</p>
<p><strong>Исключения:</strong> ArgumentException при id ≤ 0</p>

<h4>getReports()</h4>
<p>Возвращает список всех отчетов.</p>
<p><strong>Возвращает:</strong> ReportState со списком всех отчетов в DataList</p>

<h4>getReportsUser(string UserId)</h4>
<p>Возвращает все отчеты указанного пользователя.</p>
<p><strong>Параметры:</strong> UserId — идентификатор пользователя</p>
<p><strong>Возвращает:</strong> ReportState с отфильтрованным списком отчетов</p>

<h4>getReportsType(string Type)</h4>
<p>Возвращает все отчеты указанного типа.</p>
<p><strong>Параметры:</strong> Type — тип отчета</p>
<p><strong>Возвращает:</strong> ReportState с отфильтрованным списком отчетов</p>

<h4>getReportsStatus(string Status)</h4>
<p>Возвращает все отчеты с указанным статусом.</p>
<p><strong>Параметры:</strong> Status — статус отчета</p>
<p><strong>Возвращает:</strong> ReportState с отфильтрованным списком отчетов</p>

<h4>getReportsDate(DateTime dateCreate)</h4>
<p>Возвращает все отчеты, созданные в указанную дату.</p>
<p><strong>Параметры:</strong> dateCreate — дата создания</p>
<p><strong>Возвращает:</strong> ReportState с отфильтрованным списком отчетов</p>

<h4>getReportsCount()</h4>
<p>Возвращает общее количество отчетов.</p>
<p><strong>Возвращает:</strong> ReportState с количеством отчетов в свойстве Count</p>

<h2>Тестирование</h2>
<p>Набор автоматизированных тестов (NUnit) в классе <code>TestReportManager</code>. Перед каждым тестом создается словарь с 5 тестовыми отчетами.</p>

<ul>
    <li><strong>ReportManager_postOrder_Success</strong> — успешное создание отчета</li>
    <li><strong>ReportManager_postOrder_EmptyType_Failure</strong> — исключение при пустом типе</li>
    <li><strong>ReportManager_postOrder_EmptyUser_Failure</strong> — исключение при пустом пользователе</li>
    <li><strong>ReportManager_postOrder_EmptyDescription_Failure</strong> — исключение при пустом описании</li>
    <li><strong>ReportManager_deleteOrder_Success</strong> — успешное удаление отчета</li>
    <li><strong>ReportManager_deleteOrder_NotFoundId_Failure</strong> — удаление несуществующего отчета</li>
    <li><strong>ReportManager_deleteOrder_NegativeId_Failure</strong> — исключение при отрицательном ID</li>
    <li><strong>ReportManager_getReports_Success</strong> — получение всех отчетов (5 шт.)</li>
    <li><strong>ReportManager_getReports_EmptyResponse_Failure</strong> — получение отчетов при пустом словаре</li>
    <li><strong>ReportManager_getReport_Success</strong> — поиск по существующему ID</li>
    <li><strong>ReportManager_getReportId_NotFoundId_Failure</strong> — поиск по несуществующему ID</li>
    <li><strong>ReportManager_getReportId_NegativeId_Failure</strong> — исключение при отрицательном ID</li>
    <li><strong>ReportManager_getReportType_Success</strong> — поиск по существующему типу (3 отчета)</li>
    <li><strong>ReportManager_getReportType_NotFoundType_Failure</strong> — поиск по несуществующему типу</li>
    <li><strong>ReportManager_patchReport_Success</strong> — успешное редактирование отчета</li>
    <li><strong>ReportManager_patchReport_NotFoundId_Failure</strong> — редактирование несуществующего отчета</li>
    <li><strong>ReportManager_getReportsUser_Success</strong> — поиск по существующему пользователю (2 отчета)</li>
    <li><strong>ReportManager_getReportsUser_NotFoundUser_Failure</strong> — поиск по несуществующему пользователю</li>
    <li><strong>ReportManager_getReportsStatus_Success</strong> — поиск по существующему статусу (3 отчета)</li>
    <li><strong>ReportManager_getReportsStatus_NotFoundSataus_Failure</strong> — поиск по несуществующему статусу</li>
    <li><strong>ReportManager_getReportsDate_Success</strong> — поиск по существующей дате</li>
    <li><strong>ReportManager_getReportsDate_NotFoundDate_Failure</strong> — поиск по несуществующей дате</li>
    <li><strong>ReportManager_getReportsCount_Success</strong> — подсчет количества отчетов (5 шт.)</li>
    <li><strong>ReportManager_getReportsCount_EmptyResponse_Success</strong> — подсчет при пустом словаре (0 шт.)</li>
</ul>

<h2>Использование</h2>

<pre>
// Создание словаря отчетов
var reports = new Dictionary&lt;int, Report&gt;
{
    {1, new Report("финансовый", "описание", "Ivan", DateTime.Now, "Завершен")}
};

// Создание экземпляра ReportManager
var reportManager = new ReportManager(reports);

// Создание нового отчета
var createResult = reportManager.postReport("аналитический", "Petr", "новый отчет", "в процессе");

// Получение отчета по ID
var getResult = reportManager.getReportId(1);

// Получение всех отчетов пользователя
var userReports = reportManager.getReportsUser("Ivan");
</pre>

<h2>Инструкция по запуску</h2>

<h3>Требования</h3>
<ul>
    <li>.NET SDK 6.0 или выше</li>
    <li>NUnit (устанавливается автоматически)</li>
</ul>

<h3>Запуск тестов</h3>

<pre>
# Перейти в директорию проекта
cd path/to/project

# Запуск всех тестов
dotnet test

# Запуск конкретного теста
dotnet test --filter "Name=ReportManager_postOrder_Success"
</pre>

<h3>Сборка библиотеки</h3>

<pre>
dotnet build -c Release
</pre>

<p>Библиотека <code>dllAndUnitTest.dll</code> будет в папке <code>bin/Release</code>.</p>

<h3>Структура проекта</h3>

<pre>
проект/
├── Models.cs                    # Классы данных
├── ReportManager.cs             # Основная логика
├── TestReportManager.cs         # Модульные тесты
└── dllAndUnitTest.csproj        # Файл проекта
</pre>