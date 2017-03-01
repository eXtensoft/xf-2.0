// <copyright company="eXtensible Solutions, LLC" file="SqlParameter.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common.Discovery
{
    using System;
    using System.Data;
    using System.Xml.Serialization;

    public class SqlParameter
    {
        public string Schema { get; set; }

        public string StoredProcedureName { get; set; }

        public int OrdinalPosition { get; set; }

        public string ParamName { get; set; }

        public string Mode { get; set; }

        public string Datatype { get; set; }

        public int MaxLength { get; set; }

        [XmlIgnore]
        public string ToDisplay { get { return ToString(); } }

        public SqlParameter(IDataReader reader)
        {
            Schema = reader.GetString(0);
            StoredProcedureName = reader.GetString(1);
            OrdinalPosition = reader.GetInt32(2);
            if (OrdinalPosition > 0)
            {
                Mode = reader.GetString(3);
                ParamName = reader.GetString(4);
                Datatype = reader.GetString(5);
                if (reader[6] != DBNull.Value)
                {
                    MaxLength = reader.GetInt32(6);
                }
            }
        }

        public override string ToString()
        {
            return ParamName;
        }
    }
}