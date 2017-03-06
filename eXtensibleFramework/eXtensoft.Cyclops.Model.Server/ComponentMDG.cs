// <copyright file="Component.cs" company="eXtensoft, LLC">
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

    public class ComponentMDG : SqlServerModelDataGateway<Component>
    {

        #region local fields

        private const string ComponentIdParamName = "@componentid";
        private const string ComponentTypeIdParamName = "@componenttypeid";
        private const string NameParamName = "@name";
        private const string AliasParamName = "@alias";
        private const string DescriptionParamName = "@description";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Component model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [arc].[Component] ( [ComponentTypeId],[Name],[Alias],[Description] ) values (" + ComponentTypeIdParamName + "," + NameParamName + "," + AliasParamName + "," + DescriptionParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentTypeIdParamName, model.ComponentTypeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Component model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[Component] set [ComponentTypeId] = " + ComponentTypeIdParamName + " , [Name] = " + NameParamName + " , [Alias] = " + AliasParamName + " , [Description] = " + DescriptionParamName  + " where [ComponentId] = " + ComponentIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentTypeIdParamName, model.ComponentTypeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[Component] where [ComponentId] = " + ComponentIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentIdParamName, criterion.GetValue<int>("ComponentId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ComponentId], [ComponentTypeId], [Name], [Alias], [Description] from [arc].[Component] where [ComponentId] = " + ComponentIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( ComponentIdParamName, criterion.GetValue<int>("ComponentId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [ComponentId], [ComponentTypeId], [Name], [Alias], [Description] from [arc].[Component] ";
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

        public override void BorrowReader(SqlDataReader reader, List<Component> list)
        {
            while (reader.Read())
            {
                var model = new Component();
                model.ComponentId = reader.GetInt32(reader.GetOrdinal("ComponentId"));
                model.ComponentTypeId = reader.GetInt32(reader.GetOrdinal("ComponentTypeId"));
                model.Name = reader.GetString(reader.GetOrdinal("Name"));
                model.Alias = reader.GetString(reader.GetOrdinal("Alias"));
                model.Description = reader.GetString(reader.GetOrdinal("Description"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
