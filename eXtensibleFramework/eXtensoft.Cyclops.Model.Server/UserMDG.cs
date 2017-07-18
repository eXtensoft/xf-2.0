// <copyright company="eXtensoft, LLC" file="UserMDG.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using XF.Common;
    using XF.DataServices;

    public sealed class UserMDG : SqlServerModelDataGateway<User>
    {
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select [id],[username],[email] from [dbo].[aspnetusers] order by [username]";
            return cmd;
        }

        public override void BorrowReader(SqlDataReader reader, List<User> list)
        {
            while (reader.Read())
            {
                User item = new User();
                item.Id = reader.GetString(reader.GetOrdinal("Id"));
                item.Name = reader.GetString(reader.GetOrdinal("UserName"));
                item.Email = reader.GetString(reader.GetOrdinal("Email"));

                list.Add(item);
            }
        }



    }

}
