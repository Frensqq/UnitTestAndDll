using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllAndUnitTest
{
    // Класс ReportManager отвечает за совершения действий с отчетами
    public class ReportManager
    {
        // Словарь для хранения всей информации о отчетах.
        private readonly Dictionary<int, Report> _report;

        // Конструктор класса, который принимает словарь запасов
        // и инициализирует поле _report.
        public ReportManager(Dictionary<int, Report> report)
        {
            _report = report;
        }

        /// <summary>
        /// Метод для создания отчета
        /// </summary>
        /// <param name="Type">Тип отчета</param>
        /// <param name="User">Пользователь создваший отчет</param>
        /// <param name="Description">Описание отчета</param>
        /// <param name="Status">Статус отчета</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState postReport(string Type, string User, string Description, string Status)
        {
            // id новой записи = кол-во записей до создания + 1
            int id = _report.Keys.Count + 1;

            // проверка что User не пустой
            if (User.Length <= 0)
            {
                throw new ArgumentException("User id is null");
            }
            // проверка что Type не пустой
            if (Type.Length <= 0)
            {
                throw new ArgumentException("Type id is null");
            }
            // проверка что Description не пустой
            if (Description.Length <= 0)
            {
                throw new ArgumentException("description is null");
            }

            //При сваивание текущего времени на момент создания отчета
            DateTime CreateDate = DateTime.Now;

            //Создание отчета
            Report report = new Report(
                Type,
                Description,
                User,
                CreateDate,
                Status
                );

            //Добавление в словарь созданной записи
            _report.Add(id, report);

            // Возврат результата обработки отчета
            return new ReportState(true, $"created report {id}");
        }

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState deleteReport(int id)
        {
            //Проверка на то что идентификатор отчета положительный
            if (id <= 0)
            {
                throw new ArgumentException("id < 0");
            }

            //поиск необходимого идентификатора
            foreach (var item in _report)
            {
                //при успешном нахождении id происходит удаление и 
                //возврат результата обработки отчета
                if (id == item.Key)
                {
                    _report.Remove(item.Key);
                    return new ReportState(true, $"delete report {id}");
                }
            }
            //Если запись не найдена 
            return new ReportState(false, $"Report id = {id} NotFound");
        }

        /// <summary>
        /// Редактирование отчета
        /// </summary>
        /// <param name="id">Идентификатор редактируемого отчета</param>
        /// <param name="Type">Новый тип редактируемого отчета</param>
        /// <param name="User">Новый пользватель редактируемого отчета</param>
        /// <param name="Description">Новое описание редактируемого отчета</param>
        /// <param name="Status">Новый статус редактируемого отчета</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState patchReport(int id, string Type, string User, string Description, string Status)
        {
            //Проверка на то что идентификатор отчета положительный
            if (id <= 0)
            {
                throw new ArgumentException("id < 0");
            }
            //проверка что User не пустой
            if (User.Length <= 0)
            {
                throw new ArgumentException("User id is null");
            }
            //проверка что Type не пустой
            if (Type.Length <= 0)
            {
                throw new ArgumentException("Type id is null");
            }
            //проверка что Description не пустой
            if (Description.Length <= 0)
            {
                throw new ArgumentException("description is null");
            }

            //поиск необходимого идентификатора
            foreach (var item in _report)
            {
                //если найден происходит редактирвоание
                if (id == item.Key)
                {   
                    item.Value.User = User;
                    item.Value.Type = Type;
                    item.Value.Description = Description;
                    item.Value.Status = Status;
                    return new ReportState(true, $"patch report {id} complete"); //возврат успешного результата обработки отчета
                }
            }
            //возврат не успешного результата обработки отчета
            return new ReportState(false, $"Report id = {id} NotFound");
        }

        /// <summary>
        /// Получение информации об отчете по ID
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState getReportId(int id)
        {
            //Проверка на то что идентификатор отчета положительный
            if (id <= 0)
            {
                throw new ArgumentException("id < 0");
            }
            //поиск необходимого по идентификатору
            foreach (var item in _report)
            {
                //если найден происходит возврат успешного результата вместе с найденой записью
                if (id == item.Key)
                {
                    List<Report> reportList = new List<Report> { item.Value };//найденная запись сохраняется в переменную
                    return new ReportState(true, $"getReport report {id} complete", reportList);
                }
            }
            //возврат не успешного результата обработки отчета
            return new ReportState(false, $"Report id = {id} NotFound");
        }

        /// <summary>
        /// Вывод всех отчетов
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState getReports()
        {
            //Создание листа для сохранении записей
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                //Сохранение каждой из записей в лист
                reportList.Add(item.Value);
            }
            //Если в кол-во записей в листе возврат не успешного результата обработки отчета
            if (reportList.Count <= 0)
            {
                return new ReportState(false, $"NotFound reports");
            }
            //Иначе возврат успешного результата обработки отчета
            return new ReportState(true, $"getReport reports complete", reportList);
        }

        /// <summary>
        /// Получение отчетов по пользователю
        /// </summary>
        /// <param name="UserId">идентификатор пользоваетель по которому осуществляется поиск</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState getReportsUser(string UserId)
        {
            //Создание листа для сохранении записей
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                //Сохранение всех записей с заданым UserId
                if (item.Value.User == UserId)
                {
                    reportList.Add(item.Value);
                }
            }
            //Если в кол-во записей в листе возврат не успешного результата обработки отчета
            if (reportList.Count <= 0)
            {
                return new ReportState(false, $"Reports UserId = {UserId} NotFound");
            }
            //Иначе возврат успешного результата обработки отчета
            return new ReportState(true, $"getReport reports complete", reportList);
        }

        /// <summary>
        ///  Получение отчетов по типу
        /// </summary>
        /// <param name="Type">Тип отчета по которому осуществляется поиск</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState getReportsType(string Type)
        {
            //Создание листа для сохранении записей
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                //Сохранение всех записей с заданым Type
                if (item.Value.Type == Type)
                {
                    reportList.Add(item.Value);
                }
            }
            //Если в кол-во записей в листе возврат не успешного результата обработки отчета
            if (reportList.Count <= 0)
            {
                return new ReportState(false, $"Reports Type = {Type} NotFound");
            }
            //Иначе возврат успешного результата обработки отчета
            return new ReportState(true, $"getReport reports complete", reportList);
        }

        /// <summary>
        /// Получение количества отчетов
        /// </summary>
        /// <returns></returns>
        public ReportState getReportsCount()
        {
           //Создание пременной со значением 0
            int count = 0;
            foreach (var item in _report)//перебор всех отчетов 
            {
                count++;//прибавление 1 переменной за каждый отчет
            }
            //Возврат успешного результата обработки отчета и кол-ва отчетов
            return new ReportState(true, $"найдено {count} записей", count); 
        }

        /// <summary>
        /// Получение отчетов по дате создания
        /// </summary>
        /// <param name="dateCreate">Дата создания проекта</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState getReportsDate(DateTime dateCreate)
        {
            //Создание листа для сохранении записей
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                //Сохранение всех записей с заданым dateCreate
                if (item.Value.CreatedDate == dateCreate)
                {
                    reportList.Add(item.Value);
                }
            }
            //Если в кол-во записей в листе возврат не успешного результата обработки отчета
            if (reportList.Count <= 0)
            {
                return new ReportState(false, $"Reports CreatedDate = {dateCreate} NotFound");
            }
            //Иначе возврат успешного результата обработки отчета
            return new ReportState(true, $"getReport reports complete", reportList);
        }

        /// <summary>
        /// Получение отчетов по статусу
        /// </summary>
        /// <param name="Status">Статус проекта</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ReportState getReportsStatus(string Status)
        {
            //Создание листа для сохранении записей
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                //Сохранение всех записей с заданым Status
                if (item.Value.Status == Status)
                {
                    reportList.Add(item.Value);
                }
            }
            //Если в кол-во записей в листе возврат не успешного результата обработки отчета
            if (reportList.Count <= 0)
            {
                return new ReportState(false, $"Reports Status = {Status} NotFound");
            }
            //Иначе возврат успешного результата обработки отчета
            return new ReportState(true, $"getReport reports complete", reportList);
        }
    }
}
