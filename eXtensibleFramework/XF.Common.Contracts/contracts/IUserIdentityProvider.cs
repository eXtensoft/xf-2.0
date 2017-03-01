// <copyright company="eXtensible Solutions, LLC" file="IUserIdentityProvider.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{

    public interface IUserIdentityProvider
    {
        string Username { get; }

        string Culture { get; }

        string UICulture { get; }
    }
}
