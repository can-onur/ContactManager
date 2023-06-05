using MassTransit;

namespace Report.Application.Common
{
    public static class Constants
    {
        public static class RabbitMQ
        {
            public static class Exchange
            {
                public static readonly string Root = "report.locationreport.request";
            }

            public static class Queue
            {
                public static readonly string ReportRequest = "report.locationreport.request";
            }
        }
    }

    public class NameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
        {
            return Constants.RabbitMQ.Exchange.Root;
        }
    }
}
