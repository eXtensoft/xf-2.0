// <copyright company="eXtensible Solutions, LLC" file="IMessage_1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
