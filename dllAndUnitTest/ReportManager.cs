using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllAndUnitTest
{
    public class ReportManager
    {
        private readonly Dictionary<int, Report> _report;

        public ReportManager(Dictionary<int, Report> report)
        {
            _report = report;
        }

        public ReportState postReport(string Type, string User, string Description, string Status)
        {

            int id = _report.Keys.Count + 1;

            if (User.Length <= 0)
            {
                throw new ArgumentException("User id is null");
            }
            if (Type.Length <= 0)
            {
                throw new ArgumentException("Type id is null");
            }
            if (Description.Length <= 0)
            {
                throw new ArgumentException("description is null");
            }

            DateTime CreateDate = DateTime.Now;

            Report report = new Report(
                Type,
                Description,
                User,
                CreateDate,
                Status
                );

            _report.Add(id, report);

            return new ReportState(true, $"created report {id}");
        }

        public ReportState deleteReport(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id < 0");
            }

            foreach (var item in _report)
            {
                if (id == item.Key)
                {
                    _report.Remove(item.Key);
                    return new ReportState(true, $"delete report {id}");
                }
            }
            throw new ArgumentException($"Запись {id} не найдена");
        }

        public ReportState patchReport(int id, string Type, string User, string Description, string Status)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id < 0");
            }
            foreach (var item in _report)
            {
                if (id == item.Key)
                {

                    if (User.Length <= 0)
                    {
                        throw new ArgumentException("User id is null");
                    }
                    if (Type.Length <= 0)
                    {
                        throw new ArgumentException("Type id is null");
                    }
                    if (Description.Length <= 0)
                    {
                        throw new ArgumentException("description is null");
                    }

                    item.Value.User = User;
                    item.Value.Type = Type;
                    item.Value.Description = Description;
                    item.Value.Status = Status;
                    return new ReportState(true, $"patch report {id} complete");
                }
            }
            throw new ArgumentException($"Запись {id} не найдена");
        }

        public ReportState getReportId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id < 0");
            }
            foreach (var item in _report)
            {
                if (id == item.Key)
                {
                    List<Report> reportList = new List<Report> { item.Value };
                    return new ReportState(true, $"getReport report {id} complete", reportList);
                }
            }
            throw new ArgumentException($"Запись {id} не найдена");
        }


        public ReportState getReports()
        {
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                reportList.Add(item.Value);
            }
            if (reportList.Count <= 0)
            {
                throw new ArgumentException("Не найдено не одной записи");
            }

            return new ReportState(true, $"getReport reports complete", reportList);
        }

        public ReportState getReportsUser(string UserId)
        {
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                if (item.Value.User == UserId)
                {
                    reportList.Add(item.Value);
                }
            }
            if (reportList.Count <= 0)
            {
                throw new ArgumentException($"Не найдено не одной записи c UserId = {UserId}");
            }

            return new ReportState(true, $"getReport reports complete", reportList);
        }

        public ReportState getReportsType(string Type)
        {
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                if (item.Value.Type == Type)
                {
                    reportList.Add(item.Value);
                }
            }
            if (reportList.Count <= 0)
            {
                throw new ArgumentException($"Не найдено не одной записи c Type = {Type}");
            }

            return new ReportState(true, $"getReport reports complete", reportList);
        }

        public ReportState getReportsCount()
        {
            int count = 0;
            foreach (var item in _report)
            {
                count++;
            }

            return new ReportState(true, $"найдено {count} записей", count);
        }

        public ReportState getReportsDate(DateTime dateCreate)
        {
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                if (item.Value.CreatedDate == dateCreate)
                {
                    reportList.Add(item.Value);
                }
            }
            if (reportList.Count <= 0)
            {
                throw new ArgumentException($"Не найдено не одной записи c CreatedDate = {dateCreate}");
            }
            return new ReportState(true, $"getReport reports complete", reportList);
        }

        public ReportState getReportsStatus(string Status)
        {
            List<Report> reportList = new List<Report>();
            foreach (var item in _report)
            {
                if (item.Value.Status == Status)
                {
                    reportList.Add(item.Value);
                }
            }
            if (reportList.Count <= 0)
            {
                throw new ArgumentException($"Не найдено не одной записи c Status = {Status}");
            }
            return new ReportState(true, $"getReport reports complete", reportList);
        }
    }
}
