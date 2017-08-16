// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;
    using System.Data;

    public interface IResponse<T> : IEnumerable<T>
    {
        bool IsOkay { get; }

        T Model { get; }

        RequestStatus Status { get; }

        IEnumerable<IProjection> Display { get; }

        DataSet Data { get; }

    }

}
