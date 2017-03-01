// <copyright company="eXtensible Solutions LLC" file="ReplacementMap.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// This class...
    /// </summary>
    [Serializable]
    public class ReplacementMap
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("target")]
        public string TargetText { get; set; }
        [XmlAttribute("text")]
        public string ReplacementText { get; set; }

        public static IEnumerable<ReplacementMap> GetMaps()
        {
            return _Maps;
        }

        private static IList<ReplacementMap> _Maps = new List<ReplacementMap>(){
            {new ReplacementMap(){Id = 1, TargetText = "\"", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "?", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "!", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "$", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "%", ReplacementText = " percent "}},
            {new ReplacementMap(){Id = 1, TargetText = "#", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "”", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "“", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "...", ReplacementText = " "}},
            {new ReplacementMap(){Id = 1, TargetText = "(k)", ReplacementText = "k "}},
            {new ReplacementMap(){Id = 1, TargetText = " + ", ReplacementText = " and "}},
            {new ReplacementMap(){Id = 1, TargetText = "a+", ReplacementText = "a-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "c++", ReplacementText = "cpp"}},
            {new ReplacementMap(){Id = 1, TargetText = " = ", ReplacementText = " equals"}},
            {new ReplacementMap(){Id = 1, TargetText = "x2+ny2", ReplacementText = "x2-plus-ny2"}},
            {new ReplacementMap(){Id = 1, TargetText = "e=mc2", ReplacementText = "e-mc2"}},
            {new ReplacementMap(){Id = 1, TargetText = " - ", ReplacementText = "-"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.b.c.", ReplacementText = "a-b-c"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.d.d.", ReplacementText = "add "}},
            {new ReplacementMap(){Id = 1, TargetText = "a.i.m.", ReplacementText = "aim"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.k.a.", ReplacementText = "aka"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.m.", ReplacementText = "am"}},
            {new ReplacementMap(){Id = 1, TargetText = "p.m.", ReplacementText = "pm"}},
            {new ReplacementMap(){Id = 1, TargetText = "l.a.", ReplacementText = "la"}},
            {new ReplacementMap(){Id = 1, TargetText = "n.y.", ReplacementText = "ny"}},
            {new ReplacementMap(){Id = 1, TargetText = "m.d.", ReplacementText = "md"}},
            {new ReplacementMap(){Id = 1, TargetText = "ph.d", ReplacementText = "phd"}},
            {new ReplacementMap(){Id = 1, TargetText = "m.i.t.", ReplacementText = "mit"}},
            {new ReplacementMap(){Id = 1, TargetText = "0+", ReplacementText = "0-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "1+", ReplacementText = "1-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "google+", ReplacementText = "google-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "network+", ReplacementText = "network-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = ",0", ReplacementText = "0"}},
            {new ReplacementMap(){Id = 1, TargetText = ",1", ReplacementText = "1"}},
            {new ReplacementMap(){Id = 1, TargetText = ",2", ReplacementText = "2"}},
            {new ReplacementMap(){Id = 1, TargetText = ",3", ReplacementText = "3"}},
            {new ReplacementMap(){Id = 1, TargetText = ",4", ReplacementText = "4"}},
            {new ReplacementMap(){Id = 1, TargetText = ",5", ReplacementText = "5"}},
            {new ReplacementMap(){Id = 1, TargetText = ",6", ReplacementText = "6"}},
            {new ReplacementMap(){Id = 1, TargetText = ",7", ReplacementText = "7"}},
            {new ReplacementMap(){Id = 1, TargetText = ",8", ReplacementText = "8"}},
            {new ReplacementMap(){Id = 1, TargetText = ",9", ReplacementText = "9"}},
            {new ReplacementMap(){Id = 1, TargetText = "sh*t", ReplacementText = "sh-t"}},
            {new ReplacementMap(){Id = 1, TargetText = "f***ed", ReplacementText = "f-ed"}},
            {new ReplacementMap(){Id = 1, TargetText = "f*ck", ReplacementText = "f-ck"}},
            {new ReplacementMap(){Id = 1, TargetText = "f**k", ReplacementText = "f-ck"}},
            {new ReplacementMap(){Id = 1, TargetText = "f**er", ReplacementText = "f-cker"}},
            {new ReplacementMap(){Id = 1, TargetText = "*", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = " & ", ReplacementText = " and "}},
            {new ReplacementMap(){Id = 1, TargetText = "& ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " &", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "&", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " /", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " /", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "/ ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "/", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ".com", ReplacementText = "`com"}},
            {new ReplacementMap(){Id = 1, TargetText = " @ ", ReplacementText = " at "}},
            {new ReplacementMap(){Id = 1, TargetText = "@ ", ReplacementText = "at "}},
            {new ReplacementMap(){Id = 1, TargetText = " @", ReplacementText = " at "}},
            {new ReplacementMap(){Id = 1, TargetText = "@", ReplacementText = " at "}},
            {new ReplacementMap(){Id = 1, TargetText = " (", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ") ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ")", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "inc.", ReplacementText = "inc"}},
            {new ReplacementMap(){Id = 1, TargetText = "ltd.", ReplacementText = "ltd"}},
            {new ReplacementMap(){Id = 1, TargetText = "corp.", ReplacementText = "corp"}},
            {new ReplacementMap(){Id = 1, TargetText = "co.", ReplacementText = "co"}},
            {new ReplacementMap(){Id = 1, TargetText = "pub.", ReplacementText = "pub"}},
            {new ReplacementMap(){Id = 1, TargetText = "ed.", ReplacementText = "ed"}},
            {new ReplacementMap(){Id = 1, TargetText = " – ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "– ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "– ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " : ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " :", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ": ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ":", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " , ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " ,", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ", ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " . ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " .", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ". ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "'", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = ".", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "-", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "``", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "`", ReplacementText = "`"}},
        };



    }
}
