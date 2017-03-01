// <copyright company="eXtensible Solutions LLC" file="IRpcHandler`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>



namespace XF.Common
{

    public interface IRpcHander<T> : ITypeMap where T : class, new()
    {
        IContext Context { get; set; }

        U Execute<U>(T t, ICriterion criterion, IContext context);
    }
}
