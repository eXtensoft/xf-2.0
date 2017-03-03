// <copyright file="SolutionZone.cs" company="eXtensoft, LLC">
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

    public class SolutionZoneMDG : SqlServerModelDataGateway<SolutionZone>
    {

        #region local fields

        private const string SolutionZoneIdParamName = "@solutionzoneid";
        private const string SolutionIdParamName = "@solutionid";
        private const string ZoneIdParamName = "@zoneid";
        private const string DomainIdParamName = "@domainid";
        private const string NameParamName = "@name";
        private const string DescriptionParamName = "@description";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, SolutionZone model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [dbo].[SolutionZone] ([SolutionId],[ZoneId],[DomainId], [Name],[Description] ) values (" + SolutionIdParamName + "," + ZoneIdParamName + "," + DomainIdParamName + "," + NameParamName + "," + DescriptionParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionIdParamName, model.SolutionId);
            cmd.Parameters.AddWithValue(ZoneIdParamName, model.ZoneId);
            cmd.Parameters.AddWithValue(DomainIdParamName, model.DomainId);
            cmd.Parameters.AddWithValue(NameParamName, (object)model.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue(DescriptionParamName, (object)model.Description ?? DBNull.Value);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, SolutionZone model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [dbo].[SolutionZone] set [Name] = " + NameParamName + " , [Description] = " + DescriptionParamName + " where [SolutionZoneId] = " + SolutionZoneIdParamName;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionZoneIdParamName, model.SolutionZoneId);
            cmd.Parameters.AddWithValue(SolutionIdParamName, model.SolutionId);
            cmd.Parameters.AddWithValue(ZoneIdParamName, model.ZoneId);
            cmd.Parameters.AddWithValue(DomainIdParamName, model.DomainId);
            cmd.Parameters.AddWithValue(NameParamName, (object)model.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue(DescriptionParamName, (object)model.Description ?? DBNull.Value);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [dbo].[SolutionZone] where [SolutionZoneId] = " + SolutionZoneIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionIdParamName, criterion.GetValue<int>("SolutionZoneId"));

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [SolutionZoneId],[SolutionId], [ZoneId], [DomainId], [Name], [Description] from [dbo].[SolutionZone] +  where = [SolutionZoneId] = " + SolutionZoneIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SolutionZoneIdParamName, criterion.GetValue<int>("SolutionZoneId"));

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [SolutionZoneId],[SolutionId], [ZoneId], [DomainId], [Name], [Description] from [dbo].[SolutionZone]";
            if (criterion != null && criterion.Contains("SolutionId"))
            {
                sql += " where [SolutionId] =  " + SolutionIdParamName;
                cmd.Parameters.AddWithValue(SolutionIdParamName, criterion.GetValue<int>("SolutionId"));
            }
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

        public override void BorrowReader(SqlDataReader reader, List<SolutionZone> list)
        {
            while (reader.Read())
            {
                var model = new SolutionZone();
                model.SolutionZoneId = reader.GetInt32(reader.GetOrdinal("SolutionZoneId"));
                model.SolutionId = reader.GetInt32(reader.GetOrdinal("SolutionId"));
                model.ZoneId = reader.GetInt32(reader.GetOrdinal("ZoneId"));
                model.DomainId = reader.GetInt32(reader.GetOrdinal("DomainId"));
                if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                {
                    model.Name = reader.GetString(reader.GetOrdinal("Name"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                {
                    model.Description = reader.GetString(reader.GetOrdinal("Description"));
                }
                list.Add(model);

            }
        }

        #endregion Borrower overrides

    }
}