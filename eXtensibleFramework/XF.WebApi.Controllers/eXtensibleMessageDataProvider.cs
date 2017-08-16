// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System.Collections.Generic;
    using XF.Common;

    public static class eXtensibleMessageDataProvider
    {
        public static void Write(IDictionary<string, object> items)
        {
            EventWriter.Write(EventTypeOption.Custom, items.ToTypedItems());
        }


    }

}
