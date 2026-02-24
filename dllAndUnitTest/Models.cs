using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllAndUnitTest
{
    public class ReportState
    {
        public bool Succes {  get; }
        public string Report { get; }
        public List<Report> DataList { get; } = null;
        public int Count { get; } = -1;


        public ReportState(bool succes, string report, List<Report> data)
        {
            Succes = succes;
            Report = report;
            DataList = data;  //для функци которые осуществляют поиск

            // сброс
            Count = -1;
        }

        public ReportState(bool succes, string report)
        {
            Succes = succes;
            Report = report;


            //сброс
            DataList = null;
            Count = -1;

        }
        public ReportState(bool succes, string report, int count)
        {
            Succes = succes;
            Report = report;
            Count = count;

            //сброс
            DataList = null;
        }


    }

    //аналог дата класса для удобного хранения отчентов
    public class Report
    {
        public string Type { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        // Создание записи
        public Report( string type, string description, string user, DateTime createdDate, string status)
        {
            Type = type;
            Description = description;
            User = user;
            CreatedDate = createdDate;
            Status = status;
        }

        // Вывод
        public override string ToString()
        {
            return $"Отчет: Тип {Type},описание {Description}, пользователь: {User}, дата: {CreatedDate}, статус: {Status}";
        }
    }

}
