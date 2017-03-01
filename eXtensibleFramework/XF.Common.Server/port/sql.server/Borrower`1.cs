// <copyright company="eXtensible Solutions, LLC" file="Borrower`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class Borrower<T> : IListBorrower<T>
    {
        private Builder<T> _Builder;

        public Borrower()
        {
            _Builder = new Builder<T>();
        }

        public Borrower(List<DataMap> maps)
        {
            if (maps != null && maps.Count > 0)
            {
                _Builder = new Builder<T>(maps);
            }
            else
            {
                _Builder = new Builder<T>();
            }
        }

        void IBorrower<List<T>>.BorrowReader(SqlDataReader reader, List<T> t)
        {
            while (reader.Read())
            {
                Parser<T> parser = new Parser<T>(_Builder);
                parser.Parse(reader);
                _Builder.Build(t);
            }
        }
    }
}
