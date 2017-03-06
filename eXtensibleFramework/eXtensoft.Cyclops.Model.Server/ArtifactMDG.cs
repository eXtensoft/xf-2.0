// <copyright file="Artifact.cs" company="eXtensoft, LLC">
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

    public class ArtifactMDG : SqlServerModelDataGateway<Artifact>
    {

        #region local fields

        private const string ArtifactIdParamName = "@artifactid";
        private const string IdParamName = "@id";
        private const string ArtifactTypeIdParamName = "@artifacttypeid";
        private const string MimeParamName = "@mime";
        private const string ContentLengthParamName = "@contentlength";
        private const string OriginalFilenameParamName = "@originalfilename";
        private const string LocationParamName = "@location";
        private const string TitleParamName = "@title";
        private const string ArtifactScopeTypeIdParamName = "@scopetypeid";
        private const string ArtifactScopeIdParamName = "@scopeid";
        private const string TdsParamName = "@tds";
        private const string DocumentIdParamName = "@docid";


        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Artifact model, IContext context)
        {
            
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = " BEGIN TRAN declare @artifactId int insert into [arc].[Artifact] ( [Id],[ArtifactTypeId],[Mime],[ContentLength],[OriginalFilename],[Location],[Title] ,[Tds]) values (" + IdParamName + "," + ArtifactTypeIdParamName + "," + MimeParamName + "," + ContentLengthParamName + "," + OriginalFilenameParamName + "," + LocationParamName + "," + TitleParamName + "," + TdsParamName +  ")" +
                " select @artifactId = SCOPE_IDENTITY()   IF @@ERROR <> 0 BEGIN ROLLBACK TRAN return  END " +
                " insert into [arc].[documentation] ([ArtifactId],[ArtifactScopeTypeId],[ArtifactScopeId]) values ( @artifactId ," + ArtifactScopeTypeIdParamName + "," + ArtifactScopeIdParamName + " ) " +
                " IF @@ERROR <> 0 BEGIN ROLLBACK TRAN return  END COMMIT TRAN";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(IdParamName, model.Id);
            cmd.Parameters.AddWithValue(ArtifactTypeIdParamName, model.ArtifactTypeId);
            cmd.Parameters.AddWithValue(MimeParamName, model.Mime);
            cmd.Parameters.AddWithValue(ContentLengthParamName, model.ContentLength);
            cmd.Parameters.AddWithValue(OriginalFilenameParamName, model.OriginalFilename);
            cmd.Parameters.AddWithValue(LocationParamName, model.Location);
            cmd.Parameters.AddWithValue(TitleParamName, model.Title);
            cmd.Parameters.AddWithValue(ArtifactScopeTypeIdParamName, model.ArtifactScopeTypeId);
            cmd.Parameters.AddWithValue(ArtifactScopeIdParamName, model.ArtifactScopeId);
            cmd.Parameters.AddWithValue(TdsParamName, DateTimeOffset.Now);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Artifact model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [arc].[Artifact] set [Id] = " + IdParamName + " , [ArtifactTypeId] = " + ArtifactTypeIdParamName + " , [Mime] = " + MimeParamName + " , [ContentLength] = " + ContentLengthParamName + " , [OriginalFilename] = " + OriginalFilenameParamName + " , [Location] = " + LocationParamName + " , [Title] = " + TitleParamName + " where [ArtifactId] = " + ArtifactIdParamName;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(IdParamName, model.Id);
            cmd.Parameters.AddWithValue(ArtifactTypeIdParamName, model.ArtifactTypeId);
            cmd.Parameters.AddWithValue(MimeParamName, model.Mime);
            cmd.Parameters.AddWithValue(ContentLengthParamName, model.ContentLength);
            cmd.Parameters.AddWithValue(OriginalFilenameParamName, model.OriginalFilename);
            cmd.Parameters.AddWithValue(LocationParamName, model.Location);
            cmd.Parameters.AddWithValue(TitleParamName, model.Title);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [arc].[Artifact] where [ArtifactId] = " + ArtifactIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(ArtifactIdParamName, criterion.GetValue<int>("ArtifactId"));

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string sql = String.Empty;
            if (criterion.ContainsStrategy())
            {
                sql = "select a.[ArtifactId], a.[Id], a.[ArtifactTypeId], a.[Mime], a.[ContentLength], a.[OriginalFilename],a.[Location], a.[Title], a.[Tds],d.[DocumentId]," +
                "d.[ArtifactScopeTypeId],d.[ArtifactScopeId] from [arc].[Artifact] as a INNER JOIN [arc].[Documentation] as d ON a.[ArtifactId] = d.[ArtifactId] " +
                "where d.[DocumentId] =  " + DocumentIdParamName;
                cmd.Parameters.AddWithValue(DocumentIdParamName, criterion.GetValue<int>("DocumentId"));
            }
            else
            {

                //sql = "select [ArtifactId], [Id], [ArtifactTypeId], [Mime], [ContentLength], [OriginalFilename], [Location], [Title],[Tds] from [arc].[Artifact] where [ArtifactId] = " + ArtifactIdParamName;
                sql = "select a.[ArtifactId], a.[Id], a.[ArtifactTypeId], a.[Mime], a.[ContentLength], a.[OriginalFilename],a.[Location], a.[Title], a.[Tds],d.[DocumentId]," +
                "d.[ArtifactScopeTypeId],d.[ArtifactScopeId] from [arc].[Artifact] as a INNER JOIN [arc].[Documentation] as d ON a.[ArtifactId] = d.[ArtifactId] " +
                "where a.[ArtifactId] =  " + ArtifactIdParamName;
                //cmd.Parameters.AddWithValue(ArtifactIdParamName, criterion.GetValue<int>("ArtifactIdId"));
            }

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(ArtifactIdParamName, criterion.GetValue<int>("ArtifactId"));

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (criterion != null && criterion.ContainsStrategy())
            {
                string key = criterion.GetStrategyKey();
                string sql = "select a.[ArtifactId], a.[Id], a.[ArtifactTypeId], a.[Mime], a.[ContentLength], a.[OriginalFilename],a.[Location], a.[Title], a.[Tds],d.[DocumentId]," +
                    "d.[ArtifactScopeTypeId],d.[ArtifactScopeId] from [arc].[Artifact] as a INNER JOIN [arc].[Documentation] as d ON a.[ArtifactId] = d.[ArtifactId] " +
                "where d.[ArtifactScopeTypeId] = " + ArtifactScopeTypeIdParamName + " and  d.[ArtifactScopeId] = " + ArtifactScopeIdParamName;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue(ArtifactScopeTypeIdParamName, criterion.GetValue<int>("ScopeTypeId"));
                cmd.Parameters.AddWithValue(ArtifactScopeIdParamName, criterion.GetValue<int>("ScopeId"));
            }
            else
            {
                string sql = "select a.[ArtifactId], a.[Id], a.[ArtifactTypeId], a.[Mime], a.[ContentLength], a.[OriginalFilename],a.[Location], a.[Title], a.[Tds],d.[DocumentId]," +
                    "d.[ArtifactScopeTypeId],d.[ArtifactScopeId] from [arc].[Artifact] as a INNER JOIN [arc].[Documentation] as d ON a.[ArtifactId] = d.[ArtifactId] ";
                cmd.CommandText = sql;
            }
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

        public override void BorrowReader(SqlDataReader reader, List<Artifact> list)
        {
            while (reader.Read())
            {
                var model = new Artifact();
                model.ArtifactId = reader.GetInt32(reader.GetOrdinal("ArtifactId"));
                model.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                model.ArtifactTypeId = reader.GetInt32(reader.GetOrdinal("ArtifactTypeId"));
                model.Mime = reader.GetString(reader.GetOrdinal("Mime"));
                model.ContentLength = reader.GetInt64(reader.GetOrdinal("ContentLength"));
                model.OriginalFilename = reader.GetString(reader.GetOrdinal("OriginalFilename"));
                model.Location = reader.GetString(reader.GetOrdinal("Location"));
                model.Title = reader.GetString(reader.GetOrdinal("Title"));
                model.Tds = reader.GetDateTimeOffset(reader.GetOrdinal("Tds")).LocalDateTime;
                model.ArtifactScopeTypeId = reader.GetInt32(reader.GetOrdinal("ArtifactScopeTypeId"));
                model.ArtifactScopeId = reader.GetInt32(reader.GetOrdinal("ArtifactScopeId"));
                if (reader.FieldExists("DocumentId"))
                {
                    model.DocumentId = reader.GetInt32(reader.GetOrdinal("DocumentId"));
                }
                
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
