// <copyright company="eXtensible Solutions LLC" file="ProfileItem.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class ProfileItem 
    {
        [XmlAttribute("appCtx")]
        [DataMember]
        public string AppContext { get; set; }
        [DataMember]
        public string AppInstance { get; set; }
        [XmlAttribute("zone")]
        [DataMember]
        public string Zone { get; set; }
        [DataMember]
        public string AuthKey { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string UserAgent { get; set; }
        [DataMember]
        public string RequestPath { get; set; }
        [DataMember]
        public string RequestMethod { get; set; }

        [XmlAttribute("id")]
        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public Int32 Index { get; set; }

        [XmlAttribute("scope")]
        [DataMember]
        public string Scope { get; set; }

        [XmlAttribute("key")]
        [DataMember]
        public string Key { get; set; }

        [XmlAttribute("src")]
        [DataMember]
        public string Source { get; set; }

        [XmlAttribute("tag")]
        [DataMember]
        public string Tag { get; set; }

        [XmlAttribute("grp")]
        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public string Activity { get; set; }
        [DataMember]
        public string Outcome { get; set; }
        [DataMember]
        public double Elapsed { get; set; }

        [DataMember]
        public string[] Data { get; set; }

        [DataMember]
        public string[] KpiLog { get; set; }

        public ProfileItem()
        {

        }

        public List<TypedItem> ToTypedItems()
        {
            List<TypedItem> list = new List<TypedItem>();
            list.Add(new TypedItem("kpi.context", AppContext));
            list.Add(new TypedItem("kpi.instance", AppInstance));
            list.Add(new TypedItem("kpi.zone", Zone));
            list.Add(new TypedItem("kpi.authkey", AuthKey));
            list.Add(new TypedItem("kpi.ip", IP));
            list.Add(new TypedItem("kpi.useragent", UserAgent));
            list.Add(new TypedItem("kpi.requestpath", RequestPath));
            list.Add(new TypedItem("kpi.requestmethod", RequestMethod));
            list.Add(new TypedItem("kpi.session", SessionId));
            list.Add(new TypedItem("kpi.index", Index.ToString()));
            list.Add(new TypedItem("kpi.scope", Scope));
            list.Add(new TypedItem("kpi.key", Key));
            list.Add(new TypedItem("kpi.source", Source));

            if (!String.IsNullOrWhiteSpace(Tag))
            {
                list.Add(new TypedItem("kpi.tag", Tag));
            }
            if (!String.IsNullOrWhiteSpace(Group))
            {
                list.Add(new TypedItem("kpi.group", Group));
            }
            if (!String.IsNullOrWhiteSpace(Activity))
            {
                list.Add(new TypedItem("kpi.activity", Activity));
            }
            if (!String.IsNullOrWhiteSpace(Outcome))
            {
                list.Add(new TypedItem("kpi.outcome", Outcome));
            }
            if (KpiLog != null)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (var item in KpiLog)
                {
                    if (i++ > 0)
                    {
                        sb.Append(";");
                    }
                    sb.Append(item);
                }
                list.Add(new TypedItem("kpi.log", sb.ToString()));
            }
            if (Elapsed > 0.00)
            {
                list.Add(new TypedItem("kpi.elapsed", Elapsed));
            }

            if (Data != null)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (var item in Data)
                {
                    if (i++ > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(item);
                }
                list.Add(new TypedItem("kpi.data", sb.ToString()));
            }

            return list;
        }
    }
}
