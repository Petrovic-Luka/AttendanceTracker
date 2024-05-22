using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceTracker.DataAccess.JSON
{
    public class AppsettingsWrapper
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string DatabaseInUse { get; set; }
        public string MongoDatabaseName { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }

    }
    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class ConnectionStrings
    {
        public string SqlConnection { get; set; }
        public string MongoDbConnection { get; set; }
    }
}
