// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Data;
    using System.Reflection;
    using System.Xml;

    public class Parser<T>
    {
        private Builder<T> _builder;

        public Parser(Builder<T> builder)
        {
            _builder = builder;
        }

        public void Parse(IDataReader reader)
        {
            if (!_builder.IsInitialized)
            {
                _builder.InitializeIndices(reader);
            }
            _builder.Model = default(T);
            _builder.Model = Activator.CreateInstance<T>();
            Type t = _builder.Model.GetType();
            PropertyInfo[] arr = t.GetProperties();
            foreach (PropertyInfo p in arr)
            {
                if (p.CanWrite)
                {
                    if (_builder.Indices.ContainsKey(p.Name) && reader.GetValue(_builder.Indices[p.Name]) != System.DBNull.Value)
                    {
                        if (p.PropertyType.BaseType.FullName.Equals("System.Enum", StringComparison.OrdinalIgnoreCase))
                        {
                            object o = null;
                            Type type = Type.GetType(p.PropertyType.AssemblyQualifiedName);
                            o = Enum.Parse(type, reader.GetValue(_builder.Indices[p.Name]).ToString());
                            if (o != null)
                            {
                                p.SetValue(_builder.Model, o, null);
                            }
                        }
                        else if (p.PropertyType.Name.ToLower() != "xmldocument")
                        {
                            p.SetValue(_builder.Model, reader.GetValue(_builder.Indices[p.Name]), null);
                        }
                        else
                        {
                            XmlDocument xdoc = StringToXml(reader.GetValue(_builder.Indices[p.Name]).ToString());
                            p.SetValue(_builder.Model, xdoc, null);
                        }
                    }
                }
            }
        }

        private static XmlDocument StringToXml(string p)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.LoadXml(p);
            }
            catch (Exception ex)
            {
                string s = ex.Message;

                //Logger.LogEvent(FrameworkConstants.DEFAULTAPPNAME, ex.Message, LogEventType.Error, CATEGORY, 0);
            }
            return xdoc;
        }
    }
}
