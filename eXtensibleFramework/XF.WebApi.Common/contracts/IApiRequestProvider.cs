// <copyright company="eXtensoft, LLC" file="IApiRequestProvider.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using XF.Common;

    public interface IApiRequestProvider
    {
        string Key { get; }
        void Post(ApiRequest model);

        void Post(IEnumerable<ApiRequest> models);

        Page<ApiRequest> Get(int pageIndex, int pageSize);

        IEnumerable<ApiRequest> Get(int id);

        ApiRequest Get(string id);

        ApiRequest Get(Guid id);


    }

}
