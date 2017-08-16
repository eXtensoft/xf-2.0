// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Discovery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SqlStoredProcedure
    {
        private string _tooltip = "None";

        private static string[] verbs = {
                                            ModelActionOption.Post.ToString().ToLower(),
                                            ModelActionOption.Get.ToString().ToLower(),
                                            ModelActionOption.GetAll.ToString().ToLower(),
                                            ModelActionOption.Put.ToString().ToLower(),
                                            ModelActionOption.Delete.ToString().ToLower(),
                                        };

        public string Schema { get; private set; }

        public string Name { get; private set; }

        public string Model { get; private set; }

        public string ModelAction { get; private set; }

        public string Modifier { get; private set; }

        public string Hash { get; private set; }

        public bool IsModelAction { get; set; }

        public List<SqlParameter> Parameters { get; private set; }

        //public SqlStoredProcedure() { Parameters = new List<SqlParameter>(); }
        public SqlStoredProcedure(List<SqlParameter> list)
        {
            Parameters = list;
            if (list.Count > 0)
            {
                Schema = list[0].Schema;
                Name = list[0].StoredProcedureName;
                list.RemoveAt(0);

                //if (list.Count==1 && list[0].OrdinalPosition.Equals(0))
                //{
                //    Parameters.Clear();
                //}
            }
            ParseModelAction();
            if (IsModelAction)
            {
                GenerateHash();
            }
        }

        private void GenerateHash()
        {
            if (String.IsNullOrEmpty(Model))
            {
                int j = 0;
                j++;
            }
            if (!String.IsNullOrEmpty(Model))
            {
                StringBuilder sb = new StringBuilder(String.Format("{0}:{1}", Model, ModelAction));

                //if (!String.IsNullOrEmpty(Modifier))
                //{
                //    sb.Append(String.Format(".{0}", Modifier));
                //}

                sb.Append(":");

                //ModelActionOption option = (ModelActionOption)Enum.Parse(typeof(ModelActionOption), ModelAction, true);

                if (Parameters != null && Parameters.Count > 0)
                {
                    var q = from parm in Parameters
                            orderby parm.ParamName
                            select new { s = parm.ParamName, t = parm.Datatype };
                    foreach (var item in q)
                    {
                        sb.Append(item.s);
                    }
                }

                Hash = sb.ToString();
            }
        }

        private void ParseModelAction()
        {
            string[] t = Name.Split('_');
            if (t.Length >= 2)
            {
                string verb = t[1].ToLower();
                if (verbs.Contains(verb))
                {
                    bool b = false;
                    char[] array = t[0].ToCharArray();
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in array)
                    {
                        if (b)
                        {
                            sb.Append(c.ToString());
                        }
                        if (!b && Char.IsUpper(c))
                        {
                            sb.Append(c.ToString());
                            b = true;
                        }
                    }
                    string s = sb.ToString();
                    Model = sb.ToString();
                    ModelAction = ((ModelActionOption)Enum.Parse(typeof(ModelActionOption), verb, true)).ToString();
                    if (ModelAction == ModelActionOption.Get.ToString() && Model.EndsWith("s"))
                    {
                        Model = Model.TrimEnd('s');
                    }
                    if (t.Length == 3)
                    {
                        Modifier = t[2];
                        _tooltip = String.Format("{0}.{1} <{2}>", Model, ModelAction, Modifier);
                    }
                    else
                    {
                        _tooltip = String.Format("{0}.{1}", Model, ModelAction);
                    }
                }
            }
            else
            {
                Model = t[0].ToString();
            }
            if (!String.IsNullOrEmpty(Model) | !String.IsNullOrEmpty(ModelAction))
            {
                IsModelAction = true;
            }
        }

        public override string ToString()
        {
            return ToDisplay(1);
        }

        public string ToDisplay(int schemaSize)
        {
            return String.Format("[{0}].[{1}]", Schema, Name);
        }

        public string ToTooltip()
        {
            return _tooltip;
        }
    }
}
