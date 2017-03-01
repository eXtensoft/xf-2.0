// <copyright company="eXtensible Solutions LLC" file="KpiLog.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class KpiLog 
    {
        public Stopwatch Timekeeper { get; set; }


        private List<TypedItem> _Items = new List<TypedItem>();
        public List<TypedItem> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public static KpiLog Generate(string activity)
        {
            KpiLog log = new KpiLog() { Timekeeper = new Stopwatch() };
            log.Timekeeper.Start();
            log.Items.Add(new TypedItem("activity", activity));
            return log;
        }
        public static KpiLog Generate()
        {
            KpiLog log = new KpiLog() { Timekeeper = new Stopwatch() };
            log.Timekeeper.Start();
            return log;
        }

    }
}
