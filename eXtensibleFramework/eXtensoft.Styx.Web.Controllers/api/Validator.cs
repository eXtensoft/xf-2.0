// <copyright company="eXtensoft, LLC" file="Validator.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Styx.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using XF.Common;
    using XF.WebApi;

    public static class Validator
    {
        public static bool IsId(string idName, string id, out ICriterion criterion)
        {
            bool b = false;
            criterion = new Criterion();
            if (!String.IsNullOrWhiteSpace(id))
            {
                int intId;
                if (Int32.TryParse(id, out intId))
                {
                    criterion.AddItem(idName, intId);
                }
                else if (RegexPattern.IsValid(RegexPattern.Guid, id))
                {
                    criterion.AddItem(idName, id);
                }
            }

            return b;
        }

        public static void AddParams(HttpRequestMessage request, ICriterion c)
        {
            // add querystring params
        }

        public static void AddParams<T>(HttpRequestMessage request,ICriterion c)
        {
            // add request body params AND querystring params
        }
    }

}
