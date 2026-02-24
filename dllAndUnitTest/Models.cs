using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllAndUnitTest
{
    // Класс OrderResult представляет результат действий с Отчетами
    public class ReportState
    {
        // Свойство, указывающее на успешность обработки отчетов
        public bool Succes {  get; }
        // Свойство, содержащее сообщение о результате обработки отчетов
        public string Report { get; }
        // Свойство, содержащее список отчетов
        public List<Report> DataList { get; } = null;
        // Свойство, содержащее количество, применимо для функции возвращающее кол-во отчетов
        public int Count { get; } = -1;



        /// <summary>
        /// Конструктор класса, который инициализирует свойства Success и Message
        /// </summary>
        /// <param name="succes">Указывает, был ли отчет успешно обработан (true) или нет (false)</param>
        /// <param name="report">Сообщение, содержащее дополнительную информацию о результате обработки отчета</param>
        public ReportState(bool succes, string report)
        {
            Succes = succes;
            Report = report;

            //DataList и Count значения по умолчанию
            DataList = null;
            Count = -1;

        }


        /// <summary>
        /// Перегрузка конструктора класса, который инициализирует свойства Success, Message и DataList
        /// </summary>
        /// <param name="succes">Указывает, был ли отчет успешно обработан (true) или нет (false)</param> 
        /// <param name="report">Сообщение, содержащее дополнительную информацию о результате обработки отчета</param>
        /// <param name="data">Рузультат поиска или сортировки по отчетам</param>
        public ReportState(bool succes, string report, List<Report> data)//перегрузка для функций которые осуществляют поиск
        {
            Succes = succes;
            Report = report;
            DataList = data;  

            //Count значение по умолчанию
            Count = -1;
        }


        /// <summary>
        /// Перегрузка конструктора класса, который инициализирует свойства Success, Message и Count
        /// </summary>
        /// <param name="succes">Указывает, был ли отчет успешно обработан (true) или нет (false)</param>
        /// <param name="report">Сообщение, содержащее дополнительную информацию о результате обработки отчета</param>
        /// <param name="count">Число, кол-во найденных отчетов</param>
        public ReportState(bool succes, string report, int count)//перегрузка для функций которая возвращает число
        {
            Succes = succes;
            Report = report;
            Count = count;

            //DataList значение по умолчанию
            DataList = null;
        }
    }

    //аналог дата класса для удобного хранения отчентов
    /// <summary>
    /// Модель данных, хранящая информацию об отчетах 
    /// </summary>
    public class Report
    {
        // Свойство, тип отчета ( "финансовый", "аналитический" и тп)
        public string Type { get; set; }
        // Свойство, пользователь создавший отчет 
        public string User { get; set; }
        // Свойство, описание отчета 
        public string Description { get; set; }
        // Свойство, дата создания отчета, формируется автоматически при создании отчета,
        // не редактируется при изменении
        public DateTime CreatedDate { get; set; }
        // Свойство, статус отчета ("Завершен", "в процессе" и тп)
        public string Status { get; set; }

        // Создание записи
        /// <summary>
        /// Создает одну запись отчета
        /// </summary>
        /// <param name="type">Тип отчета</param>
        /// <param name="description">Описание отчета</param>
        /// <param name="user">Пользователь создавший отчет</param>
        /// <param name="createdDate">Дата создания</param>
        /// <param name="status">Статус отчета</param>
        public Report( string type, string description, string user, DateTime createdDate, string status)
        {
            Type = type;
            Description = description;
            User = user;
            CreatedDate = createdDate;
            Status = status;
        }
    }

}
