// <copyright file="ServerApp.cs" company="eXtensoft, LLC">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Data;
    using System.Data.SqlClient;
    using XF.Common;
    using XF.DataServices;

    public class FavoriteMDG : SqlServerModelDataGateway<Favorite>
    {
        private const string UserParamname = "@user";
        private const string ModelParamname = "@model";
        private const string ModelIdParamname = "@modelid";

        public override SqlCommand PostSqlCommand(SqlConnection cn, Favorite t, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string sql = "if exists (select 1 from dbo.Favorite where Model = " + ModelParamname + " and Username = " + UserParamname +
                " and ModelId = " + ModelIdParamname + ") begin delete from dbo.Favorite where Model = " + ModelParamname +
                " and Username = " + UserParamname + " and ModelId = " + ModelIdParamname + " end else begin " +
                "insert into [dbo].[Favorite]([Username],[Model],[ModelId]) values (" + UserParamname +
                "," + ModelParamname + "," + ModelIdParamname + ") end";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue(UserParamname, t.Username);
            cmd.Parameters.AddWithValue(ModelParamname, t.Model);
            cmd.Parameters.AddWithValue(ModelIdParamname, t.ModelId);

            return cmd;

        }

        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sb = new StringBuilder("select[FavoriteId],[Username],[Model],[ModelId],[Tds] from[dbo].[Favorite] ");
            if (criterion.ContainsStrategy())
            {
                int i = 0;
                if (criterion.Contains("Username"))
                {
                    i++;
                    sb.Append("where Username = " + UserParamname);
                    cmd.Parameters.AddWithValue(UserParamname, criterion.GetValue<string>("Username"));
                }
                if (criterion.Contains("Model"))
                {
                    if (i >0)
                    {
                        sb.Append(" and ");
                    }
                    sb.Append("Model = " + ModelParamname);
                    cmd.Parameters.AddWithValue(ModelParamname, criterion.GetValue<string>("Model"));
                }
            }

            cmd.CommandText = sb.ToString();

            return cmd;
        }



    }

}
