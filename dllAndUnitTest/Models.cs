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
        public bool Succes {  get; }
        public string Report { get; }
        public List<Report> DataList { get; } = null;
        public int Count { get; } = -1;

        public ReportState(bool succes, string report)
        {
            Succes = succes;
            Report = report;

            //DataList и Count значения по умолчанию
            DataList = null;
            Count = -1; 
        }

        public ReportState(bool succes, string report, List<Report> data)//перегрузка для функций которые осуществляют поиск
        {
            Succes = succes;
            Report = report;
            DataList = data;  

            //Count значение по умолчанию
            Count = -1;
        }
        public ReportState(bool succes, string report, int count)//перегрузка для функций которая возвращает число
        {
            Succes = succes;
            Report = report;
            Count = count;

            //DataList значение по умолчанию
            DataList = null;
        }
    }

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
