// <copyright file="Solution.cs" company="eXtensoft, LLC">
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

    public class SolutionMDG : SqlServerModelDataGateway<Solution>
    {

        #region local fields

        private const string SolutionIdParamName = "@solutionid";
        private const string ScopeIdParamName = "@scopeid";
        private const string NameParamName = "@name";
        private const string AliasParamName = "@alias";
        private const string DescriptionParamName = "@description";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Solution model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[Solution] ( [ScopeId],[Name],[Alias],[Description] ) values (" + ScopeIdParamName + "," + NameParamName + "," + AliasParamName + "," + DescriptionParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ScopeIdParamName, model.ScopeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Solution model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[Solution] set [ScopeId] = " + ScopeIdParamName + " , [Name] = " + NameParamName + " , [Alias] = " + AliasParamName + " , [Description] = " + DescriptionParamName  + " where [SolutionId] = " + SolutionIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ScopeIdParamName, model.ScopeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );
            cmd.Parameters.AddWithValue(SolutionIdParamName, model.SolutionId);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[Solution] where [SolutionId] = " + SolutionIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( SolutionIdParamName, criterion.GetValue<int>("SolutionId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [SolutionId], [Name], [Description],[ScopeId] from [arc].[Solution] where [SolutionId] = " + SolutionIdParamName +
                " SELECT distinct [SolutionId] ,[ZoneId] FROM [arc].[SolutionZone] where [SolutionId] = " + SolutionIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionIdParamName, criterion.GetValue<int>("SolutionId"));

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [SolutionId], [Name], [Description],[ScopeId] from [arc].[Solution] " +
                "SELECT distinct [SolutionId] ,[ZoneId] FROM [arc].[SolutionZone] ";
            cmd.CommandText = sql;

            return cmd;
        }
        public override SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "";

            cmd.CommandText = sql;

            return cmd;
        }

        #endregion SqlCommand overrides

        #region Borrower overrides

        public override void BorrowReader(SqlDataReader reader, List<Solution> list)
        {
            while (reader.Read())
            {
                var model = new Solution();
                model.SolutionId = reader.GetInt32(reader.GetOrdinal("SolutionId"));
                model.Name = reader.GetString(reader.GetOrdinal("Name"));
                model.Description = reader.GetString(reader.GetOrdinal("Description"));
                model.ScopeId = reader.GetInt32(reader.GetOrdinal("ScopeId"));
                list.Add(model);

            }
            if (reader.NextResult())
            {
                Dictionary<int, List<int>> d = new Dictionary<int, List<int>>();

                while (reader.Read())
                {
                    int zone = reader.GetInt32(reader.GetOrdinal("ZoneId"));
                    int solutionid = reader.GetInt32(reader.GetOrdinal("SolutionId"));
                    if (!d.ContainsKey(solutionid))
                    {
                        d.Add(solutionid, new List<int>());
                    }
                    d[solutionid].Add(zone);
                }
                foreach (int id in d.Keys)
                {
                    var found = list.Find(x => x.SolutionId.Equals(id));
                    if (found != null)
                    {
                        found.Zones = d[id].ToArray();
                    }
                }

            }
        }

        #endregion Borrower overrides
    }
}
