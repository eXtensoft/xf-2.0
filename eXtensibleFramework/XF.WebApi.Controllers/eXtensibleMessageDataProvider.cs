// <copyright company="Recorded Books, Inc" file="eXtensibleMessageDataProvider.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XF.Common;

    public static class eXtensibleMessageDataProvider
    {
        public static void Write(IDictionary<string, object> items)
        {
            EventWriter.Write(EventTypeOption.Custom, items.ToTypedItems());
        }


    }

}
