// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;
    using System.Data;


    public interface IRequest<T>
    {
        string Verb { get; set; }

        T Model { get; set; }

        IEnumerable<T> Content { get; set; }

        ICriterion Criterion { get; }

        IEnumerable<IProjection> Display { get; set; }

        DataSet Data { get; set; }
    }
}
