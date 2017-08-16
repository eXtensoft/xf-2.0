﻿// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Db
{
    using System.Collections.ObjectModel;


    public class DbConfigCollection : KeyedCollection<string,DbConfig>
    {
        protected override string GetKeyForItem(DbConfig item)
        {
            return item.AppContextKey;
        }
    }
}
