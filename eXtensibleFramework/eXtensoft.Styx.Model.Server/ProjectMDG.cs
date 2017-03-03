using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.ProjectManagement;
using XF.Common;
using XF.DataServices;

namespace Styx
{
    public class ProjectMDG : SqlServerModelDataGateway<Project>
    {
        private const string ItemTypeTokenParamName = "@itemTypeToken";
        private const string itemTokenParamName = "@itemToken";
        private const string pseudoTokenParamName = "@pseudoToken";
        private const string titleParamName = "@title";
        private const string groupNameParamName = "@grp";
        private const string groupTokenParamName = "@grpToken";
        private const string xmlParamName = "@xml";
        private const string upsertedParamName = "@upserted";
        private const string idParamName = "@id";

        public override SqlCommand PostSqlCommand(SqlConnection cn, Project t, IContext context)
        {
            string token = t.Title.ToLower().Replace(" ","-");

            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;



            StringBuilder sql = new StringBuilder();
            sql.Append(" declare @id int declare @token nvarchar(50) declare @guid uniqueidentifier");
            sql.Append(" IF EXISTS(SELECT * FROM [styx].[item] where [itemtypetoken] = " + ItemTypeTokenParamName + " and [itemtoken] = " + itemTokenParamName + ")");
            sql.Append(" BEGIN SET @token = " + pseudoTokenParamName + " END ELSE BEGIN SET @token = " + @itemTokenParamName);
            sql.Append(" INSERT INTO [styx].[item]([title],[itemtypetoken],[itemtoken],[groupname],[grouptoken],[createdby]) values (");
            sql.Append(titleParamName + "," + ItemTypeTokenParamName + "," + itemTokenParamName + "," + groupNameParamName + "," + groupTokenParamName + "," + upsertedParamName + ")");
            //sql.Append(" SELECT [ItemId],[ItemGuid],[Title],[GroupName],[ItemTypeToken],[ItemToken],[GroupToken] FROM [styx].[Item] where ItemId = scope_identity()");
            sql.Append(" SELECT @id = SCOPE_IDENTITY()");
            sql.Append(" SELECT @guid = ItemGuid from [styx].[item] where ItemId = " + idParamName);
            sql.Append(" INSERT INTO [styx].[projectdata]([itemid],[title],[xmldata],[upsertedby]) VALUES (@id," + titleParamName + "," + xmlParamName + "," + upsertedParamName);
            sql.Append(")");

            string select = " select top 1 i.ItemId, i.ItemGuid, i.Title, i.GroupName, i.GroupToken, i.ItemTypeToken, i.ItemToken, i.CreatedBy, i.Tds as CreatedAt, i.HasNotes, d.XmlData, d.UpsertedBy, d.Tds as UpsertedAt" +
            " from styx.Item AS i inner join styx.ProjectData AS d ON i.ItemId = d.ItemId" +
            " where (i.ItemId = @id) order by d.Tds desc";
            sql.Append(select);
            sql.Append(" END");
            cmd.CommandText = sql.ToString();
            cmd.Parameters.AddWithValue(ItemTypeTokenParamName, "project");
            cmd.Parameters.AddWithValue(itemTokenParamName, token);
            cmd.Parameters.AddWithValue(titleParamName, t.Title);
            cmd.Parameters.AddWithValue(groupNameParamName, t.Group);
            cmd.Parameters.AddWithValue(groupTokenParamName, t.Group.ToLower().Replace(" ", "-"));
            cmd.Parameters.AddWithValue(upsertedParamName, context.UserIdentity);
            var xml = GenericSerializer.DbParamFromItem(t);
            cmd.Parameters.AddWithValue(pseudoTokenParamName, "foo-bar");
            cmd.Parameters.AddWithValue(xmlParamName, xml);
            string s = sql.ToString();
            return cmd;
        }



        public override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            
            string sql = " select top 1 i.ItemId, i.ItemGuid, i.Title, i.GroupName, i.GroupToken, i.ItemTypeToken, i.ItemToken, i.CreatedBy, i.Tds as CreatedAt, i.HasNotes, d.XmlData, d.UpsertedBy, d.Tds as UpsertedAt" +
                " from styx.Item AS i inner join styx.ProjectData AS d ON i.ItemId = d.ItemId" +
                " where (i.ItemId = " + idParamName + ") order by d.Tds desc";
            cmd.Parameters.AddWithValue(idParamName, criterion.GetValue<int>("ItemId"));
            cmd.CommandText = sql;
            return cmd;
        }


        public override SqlCommand PutSqlCommand(SqlConnection cn, Project t, ICriterion criterion, IContext context)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sql = new StringBuilder();


            if (criterion == null)
            {

                sql.Append(" insert into [styx].[projectdata]([itemid],[title],[xmldata],[upsertedby]) values (");
                sql.Append(idParamName + "," + titleParamName + "," + xmlParamName + "," + upsertedParamName + ")");
                sql.Append(" update [styx].[item] set [title] = " + titleParamName + ",[updatedat] = getutcdate() where [itemid] = " + @idParamName);

                string select = " select top 1 i.ItemId, i.ItemGuid, i.Title, i.GroupName, i.GroupToken, i.ItemTypeToken, i.ItemToken, i.CreatedBy, i.Tds as CreatedAt, i.HasNotes, d.XmlData, d.UpsertedBy, d.Tds as UpsertedAt" +
                    " from styx.Item AS i inner join styx.ProjectData AS d ON i.ItemId = d.ItemId" +
                    " where (i.ItemId = " + idParamName + ") order by d.Tds desc";
                sql.Append(select);

                cmd.Parameters.AddWithValue(idParamName, t.ItemId);
                cmd.Parameters.AddWithValue(titleParamName, t.Title);
                var xml = GenericSerializer.DbParamFromItem(t);
                cmd.Parameters.AddWithValue(xmlParamName, xml);                
                cmd.Parameters.AddWithValue(upsertedParamName, context.UserIdentity);
               
            }
            else
            {

            }


            cmd.CommandText = sql.ToString();
            return cmd;
        }



        public override void BorrowReader(SqlDataReader reader, List<Project> list)
        {
            while (reader.Read())
            {
                string xml = reader.GetString(reader.GetOrdinal("XmlData"));
                Project item = GenericSerializer.StringToGenericItem<Project>(xml);
                item.ItemId = reader.GetInt32(reader.GetOrdinal("ItemId"));
                item.ItemGuid = reader.GetGuid(reader.GetOrdinal("ItemGuid"));
                item.GroupName = reader.GetString(reader.GetOrdinal("GroupName"));
                item.GroupToken = reader.GetString(reader.GetOrdinal("GroupToken"));
                item.ItemTypeToken = reader.GetString(reader.GetOrdinal("ItemTypeToken"));
                item.ItemToken = reader.GetString(reader.GetOrdinal("ItemToken"));
                item.Title = reader.GetString(reader.GetOrdinal("Title"));
                item.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                item.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
                item.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpsertedAt"));
                item.UpdatedBy = reader.GetString(reader.GetOrdinal("UpsertedBy"));
                int i = reader.GetInt32(reader.GetOrdinal("HasNotes"));
                item.HasNotes = (i == 0) ? false : true;
                item.ProjectId = item.ItemId.ToString();

                list.Add(item);
            }
        }
    
    
    
    
    
    
    
    
    
    }
}
