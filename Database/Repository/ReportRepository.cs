using AuthenticationWithClie.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Repository
{
    public class ReportRepository : Repository<Report, Guid>
    {
        public static List<Report> Reports { get; set; } = new List<Report>();

        public static void AddReport(User sender, string reason, User target)
        {
            Report report = new Report(sender, reason, target);
            Reports.Add(report);
            target.Reportinbox.Add(report);
        }
    }
}
