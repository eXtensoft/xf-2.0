// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Replacer
    {
        #region maps
        private List<ReplacementMap> _Maps = new List<ReplacementMap>(){
            {new ReplacementMap(){Id = 1, TargetText = ", ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "\"", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "?", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "!", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "$", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "%", ReplacementText = " percent "}},
            {new ReplacementMap(){Id = 1, TargetText = "#", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "”", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "“", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "'", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "...", ReplacementText = " "}},
            {new ReplacementMap(){Id = 1, TargetText = "(k)", ReplacementText = "k "}},
            {new ReplacementMap(){Id = 1, TargetText = " + ", ReplacementText = " and "}},
            {new ReplacementMap(){Id = 1, TargetText = "a+", ReplacementText = "a-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "c++", ReplacementText = "cpp"}},
            {new ReplacementMap(){Id = 1, TargetText = " = ", ReplacementText = " equals "}},
            {new ReplacementMap(){Id = 1, TargetText = "x2+ny2", ReplacementText = "x2-plus-ny2"}},
            {new ReplacementMap(){Id = 1, TargetText = "e=mc2", ReplacementText = "e-mc2"}},
            {new ReplacementMap(){Id = 1, TargetText = " - ", ReplacementText = "-"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.b.c.", ReplacementText = "abc"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.d.d", ReplacementText = "add"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.i.m.", ReplacementText = "aim"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.k.a.", ReplacementText = "aka"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.m.", ReplacementText = "am"}},
            {new ReplacementMap(){Id = 1, TargetText = "p.m.", ReplacementText = "pm"}},
            {new ReplacementMap(){Id = 1, TargetText = "l.a", ReplacementText = "la"}},
            {new ReplacementMap(){Id = 1, TargetText = "n.y.", ReplacementText = "ny"}},
            {new ReplacementMap(){Id = 1, TargetText = "m.d.", ReplacementText = "md"}},
            {new ReplacementMap(){Id = 1, TargetText = "ph.d", ReplacementText = "phd"}},
            {new ReplacementMap(){Id = 1, TargetText = "m.i.t.", ReplacementText = "mit"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwii.", ReplacementText = "ww2"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwii", ReplacementText = "ww2"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwi.", ReplacementText = "ww1"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwi", ReplacementText = "ww1"}},
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
            {new ReplacementMap(){Id = 1, TargetText = " / ", ReplacementText = "`"}},
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
            {new ReplacementMap(){Id = 1, TargetText = "co.", ReplacementText = "company"}},
            {new ReplacementMap(){Id = 1, TargetText = "pub.", ReplacementText = "pub"}},
            {new ReplacementMap(){Id = 1, TargetText = "ed.", ReplacementText = "ed"}},
            {new ReplacementMap(){Id = 1, TargetText = " : ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " :", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ": ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ":", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " , ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " ,", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ", ReplacementText =  ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " . ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " .", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = ". ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "''", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = ".", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "-", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "``", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "`", ReplacementText = "-"}},
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
            {new ReplacementMap(){Id = 1, TargetText = " = ", ReplacementText = " equals "}},
            {new ReplacementMap(){Id = 1, TargetText = "x2+ny2", ReplacementText = "x2-plus-ny2"}},
            {new ReplacementMap(){Id = 1, TargetText = "e=mc2", ReplacementText = "e-mc2"}},
            {new ReplacementMap(){Id = 1, TargetText = " - ", ReplacementText = "-"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.b.c.", ReplacementText = "abc"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.d.d", ReplacementText = "add"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.i.m.", ReplacementText = "aim"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.k.a.", ReplacementText = "aka"}},
            {new ReplacementMap(){Id = 1, TargetText = "a.m.", ReplacementText = "am"}},
            {new ReplacementMap(){Id = 1, TargetText = "p.m.", ReplacementText = "pm"}},
            {new ReplacementMap(){Id = 1, TargetText = "l.a", ReplacementText = "la"}},
            {new ReplacementMap(){Id = 1, TargetText = "n.y.", ReplacementText = "ny"}},
            {new ReplacementMap(){Id = 1, TargetText = "m.d.", ReplacementText = "md"}},
            {new ReplacementMap(){Id = 1, TargetText = "ph.d", ReplacementText = "phd"}},
            {new ReplacementMap(){Id = 1, TargetText = "m.i.t.", ReplacementText = "mit"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwii.", ReplacementText = "ww2"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwii", ReplacementText = "ww2"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwi.", ReplacementText = "ww1"}},
            {new ReplacementMap(){Id = 1, TargetText = "wwi", ReplacementText = "ww1"}},
            {new ReplacementMap(){Id = 1, TargetText = "…", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = "---", ReplacementText = "-"}},
            {new ReplacementMap(){Id = 1, TargetText = "0+", ReplacementText = "0-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "1+", ReplacementText = "1-plus"}},								
            {new ReplacementMap(){Id = 1, TargetText = "google+", ReplacementText = "google-plus"}},
            {new ReplacementMap(){Id = 1, TargetText = "network+",ReplacementText = "network-plus"}},
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
            {new ReplacementMap(){Id = 1, TargetText = " / ", ReplacementText = "`"}},
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
            {new ReplacementMap(){Id = 1, TargetText = "''", ReplacementText = ""}},
            {new ReplacementMap(){Id = 1, TargetText = ".", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "-", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = " ", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "``", ReplacementText = "`"}},
            {new ReplacementMap(){Id = 1, TargetText = "`", ReplacementText = "-"}}
        };

        #endregion

        public List<ReplacementMap> Maps { get { return _Maps; } }

        public Replacer()
        {

        }
        public Replacer(List<ReplacementMap> maps)
        {
            _Maps = maps;
        }

        public string Replace(string input)
        {
            string returnValue = String.Empty;
            if (!String.IsNullOrWhiteSpace(input))
            {
                string s = input.Trim().ToLower();
                returnValue = Replace(new StringBuilder(s, input.Length * 2));
            }

            return !String.IsNullOrEmpty(returnValue) ? Cleanse(returnValue) : String.Empty;

        }

        public string Replace(StringBuilder sb)
        {
            foreach (var map in _Maps)
            {
                sb.Replace(map.TargetText, map.ReplacementText);
            }
            return sb.ToString().Trim();
        }

        private static string Cleanse(string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            char[] t = input.ToCharArray();
            foreach (var c in t)
            {
                if (whitelist.Contains(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Trim(new char[] { '-' });
        }

        #region char whitelist
        private static List<char> whitelist = new List<char>{
            '-',
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
            'g',
            'h',
            'i',
            'j',
            'k',
            'l',
            'm',
            'n',
            'o',
            'p',
            'q',
            'r',
            's',
            't',
            'u',
            'v',
            'w',
            'x',
            'y',
            'z',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z',
        };
        #endregion

    }
}
