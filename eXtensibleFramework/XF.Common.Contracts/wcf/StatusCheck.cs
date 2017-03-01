// <copyright file="StatusCheck.cs" company="eXtensible Solutions LLC">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class StatusCheck
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("tds")]
        [DataMember]
        public DateTime Tds { get; set; }

        #region Items (List<ListItem>)

        private List<ListItem> _Items = new List<ListItem>();

        /// <summary>
        /// Gets or sets the List<ListItem> value for Items
        /// </summary>
        /// <value> The List<ListItem> value.</value>
        [DataMember]
        public List<ListItem> Items
        {
            get { return _Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                }
            }
        }

        #endregion Items (List<ListItem>)

        public static StatusCheck GenerateDefault()
        {
            return new StatusCheck();
        }

        public DataTable ToDataTable()
        {
            return new DataTable();
        }
    }
}
