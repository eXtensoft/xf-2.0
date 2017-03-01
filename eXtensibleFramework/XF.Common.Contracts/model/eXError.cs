// <copyright company="eXtensible Solutions LLC" file="eXError.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/schemas/2014/04")]
    public class eXError : eXBase
    {
        [DataMember]
        public string AppContextInstance { get; set; }
        [DataMember]
        public string ErrorId { get; set; }
        [DataMember]
        public string TicketId { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Severity { get; set; }
        [DataMember]
        public string Verb { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string UICulture { get; set; }
        [DataMember]
        public string Dbtype { get; set; }
        [DataMember]
        public string Server { get; set; }
        [DataMember]
        public string Database { get; set; }
        [DataMember]
        public string CommandType { get; set; }
        [DataMember]
        public string Command { get; set; }
        [DataMember]
        public DateTime RequestBegin { get; set; }
        [DataMember]
        public DateTime DataRequestBegin { get; set; }
        [DataMember]
        public DateTime DataAccessBegin { get; set; }
        [DataMember]
        public DateTime DataAccessEnd { get; set; }
        [DataMember]
        public DateTime DataRequestEnd { get; set; }
        [DataMember]
        public DateTime RequestEnd { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public int Month { get; set; }
        [DataMember]
        public int Day { get; set; }
        [DataMember]
        public string DayOfWeek { get; set; }
        [DataMember]
        public int Hour { get; set; }
        [DataMember]
        public int Minute { get; set; }
        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string Uri { get; set; }


        public eXError() { }

        public eXError(List<TypedItem> list)
        {
            this.Items = list;
            this.Tds = DateTime.Now;
            CreatedAt = Tds.ToString(XFConstants.DateTimeFormat);
            if (list.ContainsKey<Guid>(XFConstants.EventWriter.MessageId))
            {
                MessageId = list.Get<Guid>(XFConstants.EventWriter.MessageId);
            }
            if(list.ContainsKey<string>(XFConstants.EventWriter.Message))
            {
                Message = list.Get<string>(XFConstants.EventWriter.Message);
            }
            if (list.ContainsKey<string>(XFConstants.Context.ZONE))
            {
                Zone = list.Get<string>(XFConstants.Context.ZONE);
            }
            if (list.ContainsKey<string>(XFConstants.Context.Application))
            {
                ApplicationKey = list.Get<string>(XFConstants.Context.Application);
            }
            if (list.ContainsKey<string>(XFConstants.EventWriter.ErrorSeverity))
            {
                Severity = list.Get<string>(XFConstants.EventWriter.ErrorSeverity).ToString();
            }
            if (list.ContainsKey<string>(XFConstants.EventWriter.Category))
            {
                Category = list.Get<string>(XFConstants.EventWriter.Category).ToString();
            }
            if (list.ContainsKey<string>(XFConstants.Message.Verb))
            {
                Verb = list.Get<string>(XFConstants.Message.Verb).ToString();
            }
            if (list.ContainsKey<string>(XFConstants.Context.Model))
            {
                Model = list.Get<string>(XFConstants.Context.Model).ToString();
            }
            if (list.ContainsKey<string>(XFConstants.EventWriter.StackTrace))
            {
                StackTrace = list.Get<string>(XFConstants.EventWriter.StackTrace);
            }

            if (list.ContainsKey<string>(XFConstants.Context.UICULTURE))
            {
                UICulture = list.Get<string>(XFConstants.Context.UICULTURE);
            }
            if (list.ContainsKey<DateTime>(XFConstants.Context.RequestBegin))
            {
                RequestBegin = list.Get<DateTime>(XFConstants.Context.RequestBegin);
            }
            if (list.ContainsKey<DateTime>(XFConstants.Metrics.Scope.DataRequestService.Begin))
            {
                DataRequestBegin = list.Get<DateTime>(XFConstants.Metrics.Scope.DataRequestService.Begin);
            }
            if (list.ContainsKey<DateTimeOffset>(XFConstants.Metrics.Scope.DataRequestService.End))
            {
                DataRequestEnd = list.Get<DateTime>(XFConstants.Metrics.Scope.DataRequestService.End);
            }
            if (list.ContainsKey<DateTimeOffset>(XFConstants.Context.RequestEnd))
            {
                RequestEnd = list.Get<DateTime>(XFConstants.Context.RequestEnd);
            }
            if (list.ContainsKey<string>(XFConstants.Metrics.DbType))
            {
                Dbtype = list.Get<string>(XFConstants.Metrics.DbType);
                if (Dbtype.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
                {
                    if (list.ContainsKey<string>(XFConstants.Metrics.Database.Datasource))
                    {
                        string datastore = list.Get<string>(XFConstants.Metrics.Database.Datasource);
                        string[] cn = datastore.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (cn != null && cn.Length == 2)
                        {
                            string[] server = cn[0].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (server != null && server.Length == 2)
                            {
                                Server = server[1];
                            }
                            string[] catalog = cn[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (catalog != null && catalog.Length == 2)
                            {
                                Database = catalog[1];
                            }
                        }
                    }
                }
            }
            if (list.ContainsKey<string>(XFConstants.Metrics.Database.Command))
            {
                var found = list.FirstOrDefault(x => x.Key.Equals(XFConstants.Metrics.Database.Command));
                Command = found.Value.ToString();
                CommandType = found.Scope;
            }
            if (list.ContainsKey<DateTime>(XFConstants.Metrics.Scope.Command.Begin))
            {
                DataAccessBegin = list.Get<DateTime>(XFConstants.Metrics.Scope.Command.Begin);
                if (list.ContainsKey<DateTime>(XFConstants.Metrics.Scope.Command.End))
                {
                    DataAccessEnd = list.Get<DateTime>(XFConstants.Metrics.Scope.Command.End);
                    
                }
            }
            if (list.ContainsKey<string>("request.uri"))
            {
                Uri = list.Get<string>("request.uri");
            }
            if (list.ContainsKey<Guid>("app.context.instance"))
            {
                AppContextInstance = list.Get<Guid>("app.context.instance").ToString();
            }
            if (list.ContainsKey<string>("app.context.instance"))
            {
                AppContextInstance = list.Get<string>("app.context.instance");
            }
            if (list.ContainsKey<string>(XFConstants.Context.Ticket))
            {
                TicketId = list.Get<string>(XFConstants.Context.Ticket);
            }


            Year = Tds.Year;
            Month = Tds.Month;
            Day = Tds.Day;
            DayOfWeek = Tds.DayOfWeek.ToString();
            Hour = Tds.Hour;
            Minute = Tds.Minute;
        }
    }
}
