// <copyright file="App.cs" company="eXtensoft, LLC">
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

    public class AppMDG : SqlServerModelDataGateway<App>
    {

        #region local fields

        private const string AppIdParamName = "@appid";
        private const string AppTypeIdParamName = "@apptypeid";
        private const string NameParamName = "@name";
        private const string AliasParamName = "@alias";
        private const string DescriptionParamName = "@description";
        private const string TagsParamName = "@tags";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, App model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [dbo].[App] ( [AppTypeId],[Name],[Alias],[Description],[Tags] ) values (" + AppTypeIdParamName + "," + NameParamName + "," + AliasParamName + "," + DescriptionParamName + "," + TagsParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppTypeIdParamName, model.AppTypeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );
            cmd.Parameters.AddWithValue(TagsParamName, String.IsNullOrWhiteSpace(model.Tags) ? DBNull.Value : (object)model.Tags);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, App model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [dbo].[App] set [AppTypeId] = " + AppTypeIdParamName + " , [Name] = " + NameParamName + 
                " , [Alias] = " + AliasParamName + " , [Description] = " + DescriptionParamName + " , [Tags] = " + TagsParamName  + 
                " where [AppId] = " + AppIdParamName ;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppTypeIdParamName, model.AppTypeId );
            cmd.Parameters.AddWithValue( NameParamName, model.Name );
            cmd.Parameters.AddWithValue( AliasParamName, model.Alias );
            cmd.Parameters.AddWithValue( DescriptionParamName, model.Description );
            cmd.Parameters.AddWithValue(TagsParamName, String.IsNullOrWhiteSpace(model.Tags) ? DBNull.Value : (object)model.Tags);
            cmd.Parameters.AddWithValue(AppIdParamName, model.AppId);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [dbo].[App] where [AppId] = " + AppIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppIdParamName, criterion.GetValue<int>("AppId") );

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [AppId], [AppTypeId], [Name], [Alias], [Description], [Tags] from [dbo].[App] where [AppId] = " + AppIdParamName ;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue( AppIdParamName, criterion.GetValue<int>("AppId") );

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [AppId], [AppTypeId], [Name], [Alias], [Description], [Tags] from [dbo].[App] ";
            cmd.CommandText = sql;

            return cmd;
        }
        public override SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "Select [a].[AppId] as [Id], [a].[Name] as [Display], COALESCE([s].[Icon] ,select Icon from [dbo].[selections] where [SelectionId] = [s].MasterId)as [DisplayAlt], [a].[AppId] as [IntVal] from [dbo].[App] as [a] inner join [dbo].[Selection] as [s] on " +
                "[a].[AppTypeId] = [s].[SelectionId]";

            cmd.CommandText = sql;

            return cmd;
        }

        #endregion SqlCommand overrides

        #region Borrower overrides

        public override void BorrowReader(SqlDataReader reader, List<App> list)
        {
            while (reader.Read())
            {
                var model = new App();
                model.AppId = reader.GetInt32(reader.GetOrdinal("AppId"));
                model.AppTypeId = reader.GetInt32(reader.GetOrdinal("AppTypeId"));
                model.Name = reader.GetString(reader.GetOrdinal("Name"));
                model.Alias = reader.GetString(reader.GetOrdinal("Alias"));
                model.Description = reader.GetString(reader.GetOrdinal("Description"));
                if (!reader.IsDBNull(reader.GetOrdinal("Tags")))
                {
                    model.Tags = reader.GetString(reader.GetOrdinal("Tags"));
                }
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
