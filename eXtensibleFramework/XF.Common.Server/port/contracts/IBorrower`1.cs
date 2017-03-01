// <copyright company="eXtensible Solutions, LLC" file="IBorrower`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Data.SqlClient;

    public interface IBorrower<T>
    {
        void BorrowReader(SqlDataReader reader, T t);
    }
}
