using dllAndUnitTest;

namespace TestReportManager;

// Класс OrderProcessorTests содержит тесты для проверки функциональности класса OrderProcessor
public class TestReportManager
{
    // Поля для хранения экземпляра ReportManager и словаря запасов
    private ReportManager _reportManager;
    private Dictionary<int, Report> _reports;

    // Настройка, данный метод осуществляется перед каждым тестом
    [SetUp]
    public void SetUp()
    {

        //Создание записей для словаря отчетов
        Report report = new Report(
            "финансовый",
            "test description 1",
            "Ivan",
            DateTime.Now.Date,
            "Завершен"
            );

        Report report1 = new Report(
            "финансовый",
            "test description 2",
            "Petr",
            DateTime.Now.Date,
            "Завершен"
            );
        Report report2 = new Report(
            "аналитический",
            "test description 3",
            "Evgeniy",
            DateTime.Now.Date,
            "в процессе"
            );
        Report report3 = new Report(
            "аналитический",
            "test description 4",
            "Ivan",
            DateTime.Now.Date,
            "в процессе"
            );
        Report report4 = new Report(
            "финансовый",
            "test description 5",
            "Petr",
            DateTime.Now.Date,
            "в процессе"
            );


        //Инициализация словаря отчетов с 5-ю записями
        _reports = new Dictionary<int, Report>
        {
            {1, report},
            {2, report1},
            {3, report2},
            {4, report3},
            {5, report4}
        };
        // Создание экземпляра ReportManager с инициализированным словарем запасов
        _reportManager = new ReportManager(_reports);
    }

    //-------------------------Создание отчета----------------------------------------------------------------
    //Создание отчета
    [Test]
    public void ReportManager_postOrder_Success()
    {
        var result = _reportManager.postReport(
            "тестовый",
            "TestUser",
            "это описание тестового отчета",
            "в процессе");

        Assert.IsTrue(result.Succes);//Проверка, что функция выполнилась успешно
    }

    //Создание отчета с пустым типом
    [Test]
    public void ReportManager_postOrder_EmptyType_Failure()
    {

        Assert.Throws<ArgumentException>(() => 
        _reportManager.postReport(
            "",
            "TestUser",
            "это описание тестового отчета",
            "в процессе"));

    }

    //Создание отчета с пустым User
    [Test]
    public void ReportManager_postOrder_EmptyUser_Failure()
    {

        Assert.Throws<ArgumentException>(() =>
        _reportManager.postReport(
            "тестовый",
            "",
            "это описание тестового отчета",
            "в процессе")); // Проверка, что выбрасывается исключение

    }

    //Создание отчета с пустым описанием
    [Test]
    public void ReportManager_postOrder_EmptyDescription_Failure()
    {

        Assert.Throws<ArgumentException>(() =>
        _reportManager.postReport(
            "тестовый",
            "TestUser",
            "",
            "в процессе")); // Проверка, что выбрасывается исключение

    }



    //-------------------------Удаление отчета----------------------------------------------------------------
    //Удаление отчета
    [Test]
    public void ReportManager_deleteOrder_Success()
    {
        var result = _reportManager.deleteReport(1);
        Assert.IsTrue(result.Succes);//Проверка, что функция выполнилась успешно
    }

    //Удаление несуществующего отчета
    [Test]
    public void ReportManager_deleteOrder_NotFoundId_Failure()
    {
        Assert.Throws<ArgumentException>(() =>
            _reportManager.deleteReport(6)); // Проверка, что выбрасывается исключение
    }
    //Удаление отчета по отрицательному id
    [Test]
    public void ReportManager_deleteOrder_NegativeId_Failure()
    {
        Assert.Throws<ArgumentException>(() =>
            _reportManager.deleteReport(-1));// Проверка, что выбрасывается исключение
    }



    //----------------------------------Вывод всех записей отчетов-------------------------------------------------------
    //Вывод всех записей отчетов
    [Test]
    public void ReportManager_getReports_Success()
    {
        var result = _reportManager.getReports();
        Assert.IsTrue(result.Succes); //Проверка, что функция выполнилась успешно
        Assert.AreEqual(5, result.DataList.Count); //Проверка что возвращено 5 отчетов
                                                   //(Так как в данном тесте 5 отчетов)
    }
    //Вывод всех записей отчетов если их нет
    [Test]
    public void ReportManager_getReports_EmptyResponse_Failure()
    {
        // удаление всех созданых Setup тестов
        _reportManager.deleteReport(1); //удаление теста с id 1
        _reportManager.deleteReport(2); //удаление теста с id 2
        _reportManager.deleteReport(3); //удаление теста с id 3
        _reportManager.deleteReport(4); //удаление теста с id 4
        _reportManager.deleteReport(5); //удаление теста с id 5


        Assert.Throws<ArgumentException>(() =>
        _reportManager.getReports());// Проверка, что выбрасывается исключение

    }


    //----------------------------------Поиск по id-------------------------------------------------------
    //Вывод записи отчета с определенным id
    [Test]
    public void ReportManager_getReport_Success()
    {
        var result = _reportManager.getReportId(2);
        Assert.IsTrue(result.Succes);//Проверка, что функция выполнилась успешно
        Assert.AreEqual(1, result.DataList.Count); //Проверка что возвращен только 1 отчет
    }

    //Вывод записи отчета с несуществующим id
    [Test]
    public void ReportManager_getReportId_NotFoundId_Failure()
    {
        Assert.Throws<ArgumentException>(() => _reportManager.getReportId(6)); // Проверка, что выбрасывается исключение
    }

    //Вывод записи отчета с отрицательным id
    [Test]
    public void ReportManager_getReportId_NegativeId_Failure()
    {
        Assert.Throws<ArgumentException>(() => _reportManager.getReportId(-1)); // Проверка, что выбрасывается исключение
    }


    //----------------------------------Поиск по Типу-------------------------------------------------------
    //Вывод записей отчетов с существующим типом
    [Test]
    public void ReportManager_getReportType_Success()
    {
        var result = _reportManager.getReportsType("финансовый");
        Assert.IsTrue(result.Succes);//Проверка, что функция выполнилась успешно
        Assert.AreEqual(3, result.DataList.Count); //Проверка что возвращено 3 отчета
    }
    //Вывод записей отчетов с несуществующим типом (с типом с которым кол-во записей = 0)
    [Test]
    public void ReportManager_getReportType_NotFoundType_Failure()
    {
        Assert.Throws<ArgumentException>(() => _reportManager.getReportsType("мобильный")); // Проверка, что выбрасывается исключение
    }




    //----------------------------------Редактирование отчета-------------------------------------------------------
    //Обновление отчета
    [Test]
    public void ReportManager_patchReport_Success()
    {
        var result = _reportManager.patchReport(
            2,
            "обновленный",
            "UpdatedUser",
            "это описание тестового отчета",
            "завершен");
        Assert.IsTrue(result.Succes);//Проверка, что функция выполнилась успешно

    }

    //Обновление несуществующего отчета
    public void ReportManager_patchReport_NotFoundId_Failure()
    {
         Assert.Throws<ArgumentException>(() => _reportManager.patchReport(
            6,
            "обновленный",
            "UpdatedUser",
            "это описание тестового отчета",
            "завершен"));
        // Проверка, что выбрасывается исключение
    }



    //----------------------------------Поиск по Пользователю-------------------------------------------------------
    //Вывод записей отчетов по пользователю
    [Test]
    public void ReportManager_getReportsUser_Success()
    {
        var result = _reportManager.getReportsUser("Ivan");
        Assert.IsTrue(result.Succes);//Проверка, что функция выполнилась успешно
        Assert.AreEqual(2, result.DataList.Count); //Проверка что возвращено 2 отчета
                                                   //(Так как только 2 отчета в данном тесте имеют подходящие данные)
    }

    //Поиск по несуществующему пользователю
    [Test]
    public void ReportManager_getReportsUser_NotFoundUser_Failure()
    {
        Assert.Throws<ArgumentException>(() => 
        _reportManager.getReportsUser("Nikolay"));  // Проверка, что выбрасывается исключение

    }



    //----------------------------------Поиск по Статусу-------------------------------------------------------
    //Вывод записей отчетов по статусу
    [Test]
    public void ReportManager_getReportsStatus_Success()
    {
        var result = _reportManager.getReportsStatus("в процессе");
        Assert.IsTrue(result.Succes); //Проверка, что функция выполнилась успешно
        Assert.AreEqual(3, result.DataList.Count); //Проверка что возвращено 3 отчета
                                                   //(Так как только 3 отчета в данном тесте имеют подходящие данные)
    }

    //Поиск по несуществующему статусу
    [Test]
    public void ReportManager_getReportsStatus_NotFoundSataus_Failure()
    {
        Assert.Throws<ArgumentException>(() =>
        _reportManager.getReportsStatus("Устал"));  // Проверка, что выбрасывается исключение

    }



    //----------------------------------Поиск по дате-------------------------------------------------------
    //Вывод записей отчетов по дате создания
    [Test]
    public void ReportManager_getReportsDate_Success()
    {
        var result = _reportManager.getReportsDate(DateTime.Now.Date); //.Date ставит дату с временем 0:00, чтобы поиск осуществлялся только по дате
        Assert.IsTrue(result.Succes); //Проверка, что функция выполнилась успешно
        Assert.IsNotNull(result.DataList); //Проверка что функция вернула не нулевой список отчетов
    }


    //----------------------------------Вывод сумарного кол-ва отчетов-------------------------------------------------------
    //Получение количества отчетов
    [Test]
    public void ReportManager_getReportsCount_Success()
    {
        var result = _reportManager.getReportsCount();
        Assert.IsTrue(result.Succes);  //Проверка, что функция выполнилась успешно
        Assert.AreEqual(5, result.Count); //Проверка что возвращенное число 5
        //(Так как в данном тесте 5 отчетов)
    }

    //Получение количества отчетов при их отсутсвии
    [Test]
    public void ReportManager_getReportsCount_EmptyResponse_Success()
    {
        // удаление всех созданых Setup тестов
        _reportManager.deleteReport(1); //удаление теста с id 1
        _reportManager.deleteReport(2); //удаление теста с id 2
        _reportManager.deleteReport(3); //удаление теста с id 3
        _reportManager.deleteReport(4); //удаление теста с id 4
        _reportManager.deleteReport(5); //удаление теста с id 5


        var result = _reportManager.getReportsCount();
        Assert.IsTrue(result.Succes);  //Проверка, что функция выполнилась успешно
        Assert.AreEqual(0, result.Count); //Проверка что возвращенное число 0
        //(Так как вcе отчеты удалены)
    }
}