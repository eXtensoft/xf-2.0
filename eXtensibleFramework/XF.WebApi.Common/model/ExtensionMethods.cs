using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XF.WebApi
{
    public static class ExtensionMethods
    {

        public static T Get<T>(this Dictionary<string,object> items, string key)
        {
            T t = default(T);

            if (items.ContainsKey(key))
            {
                try
                {

                    object o = items[key];
                    t = (T)o;

                }
                catch {}
            }

            return t;
        }
        public static string GetAsString(this Dictionary<string,object> items, string key)
        {
            string s = String.Empty;
            if (items.ContainsKey(key))
            {
                s = items[key].ToString();
            }

            return s;
        }

        public static void TrySet<T>(this Dictionary<string, object> items, string key, T t)
        {
            if (items.ContainsKey(key) && items[key].GetType().Equals(typeof(T)))
            {
                t = (T)items[key];
            }
        }
        public static string ToXmlString(this ApiRequest model)
        {
            Type t = model.GetType();
            XmlDocument xml = new XmlDocument();

            xml.PreserveWhitespace = false;
            XmlSerializer serializer = new XmlSerializer(t);
            using (MemoryStream stream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(stream))
            using (StreamReader reader = new StreamReader(stream))
            {
                serializer.Serialize(writer, model);
                stream.Position = 0;
                xml.Load(stream);
            }
            XmlDeclaration declaration;
            if (xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            {
                declaration = (XmlDeclaration)xml.FirstChild;
                declaration.Encoding = "UTF-16";
            }
            return xml.InnerXml;
        }
        //public static T Get<T>(this Dictionary<string, object> d, string key)
        //{
        //    T t = default(T);
        //    if (d.ContainsKey(key))
        //    {
        //        object o = d[key];
        //        if (o != null)
        //        {
        //            try
        //            {
        //                t = (T)o;
        //            }
        //            catch { }
        //        }
        //    }

        //    return t;
        //}

    }
}
