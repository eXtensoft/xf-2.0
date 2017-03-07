// <copyright company="eXtensoft, LLC" file="NoteMDG.cs">
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

    public class NoteMDG : SqlServerModelDataGateway<Note>
    {

        #region local fields

        private const string NoteIdParamName = "@noteid";
        private const string SubjectParamName = "@subject";
        private const string BodyParamName = "@body";
        private const string UserIdentityParamName = "@useridentity";
        private const string TdsParamName = "@tds";

        #endregion local fields

        #region SqlCommand overrides

        public override SqlCommand PostSqlCommand(SqlConnection cn, Note model, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "insert into [dbo].[Note] ( [Subject],[Body],[UserIdentity],[Tds] ) values (" + SubjectParamName + "," + BodyParamName + "," + UserIdentityParamName + "," + TdsParamName + ")";

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SubjectParamName, model.Subject);
            cmd.Parameters.AddWithValue(BodyParamName, model.Body);
            cmd.Parameters.AddWithValue(UserIdentityParamName, model.UserIdentity);
            cmd.Parameters.AddWithValue(TdsParamName, model.Tds);

            return cmd;
        }
        public override SqlCommand PutSqlCommand(SqlConnection cn, Note model, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "update [dbo].[Note] set [Subject] = " + SubjectParamName + " , [Body] = " + BodyParamName + " , [UserIdentity] = " + UserIdentityParamName + " , [Tds] = " + TdsParamName + " where [NoteId] = " + NoteIdParamName;


            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(SubjectParamName, model.Subject);
            cmd.Parameters.AddWithValue(BodyParamName, model.Body);
            cmd.Parameters.AddWithValue(UserIdentityParamName, model.UserIdentity);
            cmd.Parameters.AddWithValue(TdsParamName, model.Tds);

            return cmd;
        }
        public override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "delete from [dbo].[Note] where [NoteId] = " + NoteIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(NoteIdParamName, criterion.GetValue<int>("NoteId"));

            return cmd;
        }
        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [NoteId], [Subject], [Body], [UserIdentity], [Tds] from [dbo].[Note] where [NoteId] = " + NoteIdParamName;

            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue(NoteIdParamName, criterion.GetValue<int>("NoteId"));

            return cmd;
        }
        public override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string sql = "select [NoteId], [Subject], [Body], [UserIdentity], [Tds] from [dbo].[Note] ";
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

        public override void BorrowReader(SqlDataReader reader, List<Note> list)
        {
            while (reader.Read())
            {
                var model = new Note();
                model.NoteId = reader.GetInt32(reader.GetOrdinal("NoteId"));
                model.Subject = reader.GetString(reader.GetOrdinal("Subject"));
                model.Body = reader.GetString(reader.GetOrdinal("Body"));
                model.UserIdentity = reader.GetString(reader.GetOrdinal("UserIdentity"));
                model.Tds = reader.GetDateTime(reader.GetOrdinal("Tds"));
                list.Add(model);

            }
        }

        #endregion Borrower overrides
    }
}
