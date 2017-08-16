// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public static class eXtensiblePrincipal
    {

        private const string CallerIdKey = "xf-id";

        private static readonly string KeyPattern = "xf-custom-{0}";

        static eXtensiblePrincipal()
        {
            try
            {
                string customKeyCandidate = ConfigurationProvider.AppSettings["xf-custom-key"];
                if (!String.IsNullOrWhiteSpace(customKeyCandidate))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in customKeyCandidate.ToCharArray())
                    {
                        if (Char.IsLetterOrDigit(item))
                        {
                            sb.Append(item.ToString().ToLower());
                        }
                    }
                    KeyPattern = String.Format("xf-{0}-{1}", sb.ToString().Trim(), "{0}");
                }
                else
                {
                    KeyPattern = "xf-custom-{0}";
                }
            }
            catch
            {
                KeyPattern = "xf-custom-{0}";

            }
        }

        public static T GetValue<T>(string key)
        {
            return default(T);
        }

        public static Guid GetCallerId()
        {
            Guid id;
            if (!TryGetValueInternal<Guid>(CallerIdKey, false, out id))
            {
                id = Guid.NewGuid();
                SetValueInternal(CallerIdKey, false, id);
            }
            return id;
        }


        public static bool TryGetValue<T>(string key, out T t)
        {
            return TryGetValueInternal<T>(key, true, out t);
        }




        public static void SetValue(string key, object o)
        {
            SetValueInternal(key, true, o);

        }

        private static bool TryGetValueInternal<T>(string key, bool isCustom, out T t)
        {
            bool b = false;
            t = default(T);
            string internalKey = isCustom ? String.Format(KeyPattern, key.Trim().ToLower()) : key.Trim().ToLower();
            var principal = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal;
            if (principal != null)
            {
                if (principal.Items != null)
                {
                    List<TypedItem> items = principal.Items.ToList();
                    for (int i = 0; !b && i < items.Count; i++)
                    {
                        if (items[i].Key.Equals(internalKey, StringComparison.OrdinalIgnoreCase))
                        {
                            TypedItem item = principal.Items.First(x => x.Key.Equals(internalKey, StringComparison.OrdinalIgnoreCase));

                            if (item != null)
                            {
                                try
                                {
                                    t = (T)item.Value;
                                    b = true;
                                }
                                catch (Exception ex)
                                {
                                    string s = ex.Message;
                                }
                            }

                        }
                    }
                }
            }




            return b;
        }

        private static void SetValueInternal(string key, bool isCustom, object o)
        {
            var principal = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal;
            if (principal != null)
            {
                string internalKey = isCustom ? String.Format(KeyPattern, key.Trim().ToLower()) : key.Trim().ToLower();
                principal.Items.Add(new TypedItem(internalKey, o));
            }
        }

    }
}
