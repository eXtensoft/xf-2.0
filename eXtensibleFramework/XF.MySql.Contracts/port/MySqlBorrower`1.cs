// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;

    public class MySqlBorrower<T> : IMySqlListBorrower<T>
    {
        private MySqlBuilder<T> _Builder;

        public MySqlBorrower()
        {
            _Builder = new MySqlBuilder<T>();
        }

        public MySqlBorrower(List<DataMap> maps)
        {
            if (maps != null && maps.Count > 0)
            {
                _Builder = new MySqlBuilder<T>(maps);
            }
            else
            {
                _Builder = new MySqlBuilder<T>();
            }
        }

        void IMySqlBorrower<List<T>>.BorrowReader(MySqlDataReader reader, List<T> t)
        {
            while (reader.Read())
            {
                MySqlParser<T> parser = new MySqlParser<T>(_Builder);
                parser.Parse(reader);
                _Builder.Build(t);
            }
        }
    }
}
