// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.WebApi
{

    using System;

    public static class RegexPattern
    {
        public const string Numeric = @"^[0-9]{2,10}$";
        public const string Guid = @"^[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}?$";
        public const string Url = @"^(http(?:s)?\:\/\/[a-zA-Z0-9]+(?:(?:\.|\-)[a-zA-Z0-9]+)+(?:\:\d+)?(?:\/[\w\-]+)*(?:\/?|\/\w+\.[a-zA-Z]{2,4}(?:\?[\w]+\=[\w\-]+)?)?(?:\&[\w]+\=[\w\-]+)*)$";
        public const string Email = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public const string MongoId = "^[0-9a-fA-F]{24}$";

        public static bool IsValid(string pattern, string candidate)
        {
            bool b = false;
            if (!String.IsNullOrWhiteSpace(pattern) && !String.IsNullOrWhiteSpace(candidate))
            {
                b = System.Text.RegularExpressions.Regex.IsMatch(candidate, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            return b;
        }

    }
}
