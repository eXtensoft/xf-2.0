﻿// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;


    public interface IMessage<T> where T : class, new()
    {
        IEnumerable<TypedItem> Context { get; set; }

        string ModelTypename { get; }

        string Verb { get; }
    }

}
