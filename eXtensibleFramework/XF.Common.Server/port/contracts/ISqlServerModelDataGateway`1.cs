// <copyright company="eXtensible Solutions, LLC" file="ISqlServerModelDataGateway`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISqlServerModelDataGateway<T> : IModelDataGateway<T> where T : class, new()
    {
        SqlConnection DbConnection { get; set; }

    }
}
