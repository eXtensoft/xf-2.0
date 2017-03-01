

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Parser
    {

        public static T Parse<T>(string valueToConvert) where T : IConvertible
        {
            T t = default(T);
            try
            {
                t = (T)Convert.ChangeType(valueToConvert, typeof(T));
            }
            catch 
            {
            }
            return t;
        }

        public static bool Parse<T>(string valueToConvert, out T t) where T : IConvertible
        {
            bool b = false;
            t = default(T);
            try
            {
                t = (T)Convert.ChangeType(valueToConvert, typeof(T));
            }
            catch
            {

            }
            return b;
        }



    }
}
