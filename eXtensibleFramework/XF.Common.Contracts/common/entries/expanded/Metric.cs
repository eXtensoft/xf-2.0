// <copyright company="Extensible Solutions LLC" file="Metric.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Linq;

    /// <summary>
    /// This class...
    /// </summary>
    [Serializable]
    public class Metric
    {
        public string IId { get; set; }
        public string UIC { get; set; }
        public string User { get; set; }
        public string Zone { get; set; }
        public string AppKey { get; set; }
        public string Model { get; set; }
        public string Verb { get; set; }
        public string Dbtype {get;set;}
        public string Server {get;set;}
        public string Database {get;set;}
        public string CommandType { get; set; }
        public string Command {get;set;}
        public int Count { get; set; }
        public string Criteria { get; set; }
        public double RequestElapsed {get;set;}
        public double ResolveMdgElapsed { get; set; }
        public double DataAccessElapsed {get;set;}
        public DateTime RequestBegin {get;set;}
        public DateTime DataRequestBegin {get;set;}
        public DateTime DataAccessBegin {get;set;}
        public DateTime DataAccessEnd {get;set;}
        public DateTime DataRequestEnd { get; set; }
        public DateTime RequestEnd { get; set; }
        public DateTimeOffset Tds { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public Metric(){}

        public Metric(eXMetric item)
        {
            AppKey = !String.IsNullOrWhiteSpace(item.ApplicationKey) ? item.ApplicationKey : item.Items.GetValueAs<string>(XFConstants.Context.Application);
            Zone = !String.IsNullOrWhiteSpace(item.Zone) ? item.Zone : item.Items.GetValueAs<string>(XFConstants.Context.ZONE);
            Tds = item.Tds;

            Model = item.Items.GetValueAs<string>(XFConstants.Context.Model);
            Verb = item.Items.GetValueAs<string>(XFConstants.Message.Verb);
            User = item.Items.GetValueAs<string>(XFConstants.Context.USERIDENTITY);
            UIC = item.Items.GetValueAs<string>(XFConstants.Context.UICULTURE);
            IId = item.Items.GetValueAs<string>(XFConstants.Context.INSTANCEIDENTIFIER);
            Dbtype = item.Items.GetValueAs<string>(XFConstants.Metrics.DbType);
            Criteria = item.Items.GetValueAs<string>(XFConstants.Metrics.Criteria);
            Count = item.Items.GetValueAs<int>(XFConstants.Metrics.Count);
            RequestBegin = item.Items.GetValueAs<DateTime>(XFConstants.Context.RequestBegin);
            DataRequestBegin = item.Items.GetValueAs<DateTime>(XFConstants.Metrics.Scope.DataRequestService.Begin);
            DataRequestEnd = item.Items.GetValueAs<DateTime>(XFConstants.Metrics.Scope.DataRequestService.End);
            RequestEnd = item.Items.GetValueAs<DateTime>(XFConstants.Context.RequestEnd);
            if (!String.IsNullOrWhiteSpace(Dbtype) && Dbtype.Equals("sqlserver",StringComparison.OrdinalIgnoreCase))
            {
                string datastore = item.Items.GetValueAs<string>(XFConstants.Metrics.Database.Datasource);
                if (!String.IsNullOrWhiteSpace(datastore))
	            {
		             string[] cn = datastore.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);
                    if (cn != null && cn.Length == 2)
                    {
                        string[] server = cn[0].Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);
                        if (server != null && server.Length == 2)
                        {
                            Server = server[1];
                        }
                        string[] catalog = cn[1].Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);
                        if (catalog != null && catalog.Length == 2)
                        {
                            Database = catalog[1];
                        }
                    }	            
                }

                var cnt = item.Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Metrics.Count));
                int i;
                if (cnt != null && Int32.TryParse(cnt.Value.ToString(), out i))
                {
                    Count = i;
                }
                                
                var found = item.Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Metrics.Scope.Command.Text));
                if (found != null)
                {
                    Command = found.Value.ToString();
                    CommandType = found.Scope;
                }
                DataAccessBegin = item.Items.GetValueAs<DateTime>(XFConstants.Metrics.Scope.Command.Begin);
                DataAccessEnd = item.Items.GetValueAs<DateTime>(XFConstants.Metrics.Scope.Command.End);
                TimeSpan ts = DataAccessEnd - DataAccessBegin;
                DataAccessElapsed = ts.TotalMilliseconds;// Convert.ToDecimal(ts.TotalMilliseconds); 
            }
            else
            {
                var list = item.Items.ToList();
                if (list.ContainsKey(XFConstants.Metrics.Database.Datasource))
                {
                    string datastore = item.Items.GetValueAs<string>(XFConstants.Metrics.Database.Datasource);
                    Database = datastore;
                }
                if (list.ContainsKey(XFConstants.Metrics.Scope.Command.Begin) && list.ContainsKey(XFConstants.Metrics.Scope.Command.End))
                {
                    DataAccessBegin = item.Items.GetValueAs<DateTime>(XFConstants.Metrics.Scope.Command.Begin);
                    DataAccessEnd = item.Items.GetValueAs<DateTime>(XFConstants.Metrics.Scope.Command.End);
                }
                var found = item.Items.GetValueAs<string>(XFConstants.Metrics.Scope.Command.Text);
                if (found != null)
                {
                    Command = found;
                }

                DataAccessBegin = DataRequestBegin;
                DataAccessEnd = DataRequestEnd;
                
                TimeSpan ts = DataAccessEnd - DataAccessBegin;
                DataAccessElapsed = ts.TotalMilliseconds;// Convert.ToDecimal(ts.TotalMilliseconds);
            }

            TimeSpan request = RequestEnd - RequestBegin;
            RequestElapsed = request.TotalMilliseconds;// Convert.ToDecimal(request.TotalMilliseconds);

            TimeSpan resolver = DataAccessBegin - DataRequestBegin;
            ResolveMdgElapsed = resolver.TotalMilliseconds;
            Year = item.Tds.Year;
            Month = item.Tds.Month;
            Day = item.Tds.Day;
            DayOfWeek = item.Tds.DayOfWeek.ToString();
            Hour = item.Tds.Hour;
            Minute = item.Tds.Minute;
        }

    }

}
