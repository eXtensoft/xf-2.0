// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

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
