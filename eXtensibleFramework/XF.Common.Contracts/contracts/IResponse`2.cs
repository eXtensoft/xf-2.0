// <copyright company="eXtensible Solutions LLC" file="IResponse`2.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{

    public interface IResponse<T,U> : IResponse<T> where T : class, new()
    {
        U ActionResult { get; set; }
    }
}
