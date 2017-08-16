// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public static class ParamsParser 
    {
        public static IDictionary<string, string> NormalizeArgs(string[] args, IDictionary<string, string> paramkeys)
        {
            Dictionary<string, string> programArgs = new Dictionary<string, string>();
            HashSet<string> uniqueParams = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
            foreach (var item in args)
            {
                string[] kvp = item.Split(new char[] { '=' });
                if (kvp.Length == 2)
                {
                    string key = kvp[0].Trim().ToLower();
                    if (paramkeys.ContainsKey(key) && uniqueParams.Add(key))
                    {
                        string normalizedkey = paramkeys[key];
                        programArgs.Add(normalizedkey, kvp[1]);
                    }
                }
            }
            return programArgs;
        }
    }
}
