

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ConfigModelDataGateway<T> : GenericModelDataGateway<T> where T : class,new()
    {
        private XF.Common.Db.Model _Model;

        public ConfigModelDataGateway(XF.Common.Db.Model model)
        {
            // TODO: Complete member initialization
            _Model = model;
        }



        protected override SqlCommand DeleteSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return GetCommand(cn, criterion, context, ModelActionOption.Delete);
        }

        protected override SqlCommand GetAllSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return GetCommand(cn, criterion, context, ModelActionOption.GetAll);
            //return SqlResolver.ResolveGetAllCommand<T>(cn, "dbo", criterion);
        }

        protected override SqlCommand GetSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {

            return GetCommand(cn, criterion, context, ModelActionOption.Get);
        }

        protected override SqlCommand GetAllProjectionsSqlCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return GetCommand(cn, criterion, context, ModelActionOption.GetAllProjections);
        }

        protected override SqlCommand PostSqlCommand(SqlConnection cn, T t, IContext context)
        {
            return GetCommand(cn, t, context, ModelActionOption.Post);
        }

        protected override SqlCommand PutSqlCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
        {
            if (t != null)
            {
                return GetCommand(cn, t, context, ModelActionOption.Put);
            }
            else
            {
                return GetCommand(cn, criterion, context, ModelActionOption.Put);
            }
        }



        private SqlCommand GetCommand(SqlConnection cn, T t, IContext context, ModelActionOption option)
        {
            SqlCommand cmd = null;
            var found = _Model.ModelActions.Find(x => x.Verb.Equals(option));
            if (found == null)
            {
                XF.Common.Discovery.SqlTable table = null;
                if (SprocMapCache.Instance.TryGetTable<T>(context,cn,out table))
                {
                    cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate<T>(cn, table, option, t);
                }                
            }
            else
            {
                cmd = cn.CreateCommand();
                XF.Common.Db.DbCommand dbCmd = null;
                var foundAction = _Model.ModelActions.Find(x => x.Verb.Equals(option));
                if (foundAction != null)
                {
                    dbCmd = _Model.Commands.Find(x => x.Key.Equals(foundAction.DbCommandKey, StringComparison.OrdinalIgnoreCase));
                }
                if (dbCmd != null)
                {
                    cmd.CommandType = ConvertCommandType(dbCmd.CommandType);
                    cmd.CommandText = dbCmd.CommandText;
                    AddParameters(cmd, dbCmd.Parameters, t,context);
                }
            }

            return cmd;            
        }



        private SqlCommand GetCommand(SqlConnection cn, ICriterion criterion, IContext context, ModelActionOption option)
        {
            SqlCommand cmd = null;
            var found = _Model.ModelActions.Find(x => x.Verb.Equals(option));
            if(found != null)
            {
                XF.Common.Db.DbCommand dbCmd = null;
                if (criterion != null && criterion.ContainsStrategy() && found.Switches != null)
                {
                    var foundCase = found.Switches[0].Cases.Find(x => x.Value.Equals(criterion.GetStrategyKey()));
                    if (foundCase != null)
                    {
                        dbCmd = _Model.Commands.Find(x => x.Key.Equals(foundCase.CommandKey, StringComparison.OrdinalIgnoreCase));
                    }
                }
                else
                {
                    var foundAction = _Model.ModelActions.Find(x => x.Verb.Equals(option));
                    if (foundAction != null && _Model.Commands != null)
                    {
                        dbCmd = _Model.Commands.Find(x => x.Key.Equals(foundAction.DbCommandKey, StringComparison.OrdinalIgnoreCase));
                    }
                }
                if (dbCmd != null)
                {
                    cmd = cn.CreateCommand();
                    cmd.CommandType = ConvertCommandType(dbCmd.CommandType);
                    cmd.CommandText = dbCmd.CommandText;
                    AddParameters(cmd, dbCmd.Parameters, criterion,context);
                }
            }
            if (cmd == null)
            {
                XF.Common.Discovery.SqlTable table = null;
                if (SprocMapCache.Instance.TryGetTable<T>(context,cn,out table))
                {
                    cmd = XF.Common.Discovery.InlineSqlCommandGenerator.Generate(cn, table, option, criterion);
                }
            }


            return cmd;
        }


        protected override List<DataMap> GetDataMaps()
        {
            if (_Model.DataMaps != null)
            {
                List<DataMap> list = new List<DataMap>();
                foreach (var item in _Model.DataMaps)
                {
                    DataMap map = new DataMap();
                    map.DataType = item.DataType;
                    map.ColumnName = item.Column;
                    map.PropertyName = item.Property;
                    map.IsNullable = item.IsNullable;
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        private static object GetValue(string name, DbType dbType, ICriterion criterion, IContext context)
        {
            object o = null;
            if (criterion != null)
            {
                try
                {
                    switch (dbType)
                    {
                        case DbType.AnsiString:
                            break;
                        case DbType.AnsiStringFixedLength:
                            break;
                        case DbType.Binary:
                            break;
                        case DbType.Boolean:
                            o = criterion.GetValue<bool>(name);
                            break;
                        case DbType.Byte:
                            break;
                        case DbType.Currency:
                            break;
                        case DbType.Date:                   
                        case DbType.DateTime:
                        case DbType.DateTime2:
                        case DbType.DateTimeOffset:
                            o = criterion.GetValue<DateTime>(name);
                            break;
                        case DbType.Decimal:
                            o = criterion.GetValue<decimal>(name);
                            break;
                        case DbType.Double:
                            o = criterion.GetValue<double>(name);
                            break;
                        case DbType.Guid:
                            o = criterion.GetValue<Guid>(name);
                            break;
                        case DbType.Int16:
                            o = criterion.GetValue<Int16>(name);
                            break;
                        case DbType.Int32:
                            o = criterion.GetValue<Int32>(name);
                            break;
                        case DbType.Int64:
                            o = criterion.GetValue<Int64>(name);
                            break;
                        case DbType.Object:
                            break;
                        case DbType.SByte:
                            break;
                        case DbType.Single:
                            break;
                        case DbType.String:
                            o = criterion.GetValue<string>(name);
                            break;
                        case DbType.StringFixedLength:
                            o = criterion.GetValue<string>(name);
                            break;
                        case DbType.Time:
                            break;
                        case DbType.UInt16:
                            break;
                        case DbType.UInt32:
                            break;
                        case DbType.UInt64:
                            break;
                        case DbType.VarNumeric:
                            break;
                        case DbType.Xml:
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    
                    string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties();
                    context.SetError(500, message);
                    EventWriter.WriteError(message, SeverityType.Error, "DataAccess", props);
                }                
             
            }
            return o;
        }
       
        
        private static IDictionary<string, ParameterDirection> maps = new Dictionary<string, ParameterDirection>(StringComparer.OrdinalIgnoreCase)
        {
            {String.Empty,ParameterDirection.Input},
            {"in",ParameterDirection.Input},
            {"input",ParameterDirection.Input},
            {"out",ParameterDirection.Output},
            {"output",ParameterDirection.Output},
            {"inout",ParameterDirection.InputOutput},
            {"inputoutput",ParameterDirection.InputOutput},
            {"both",ParameterDirection.InputOutput},
            {"ret",ParameterDirection.ReturnValue},
            {"retval",ParameterDirection.ReturnValue},
            {"return",ParameterDirection.ReturnValue},
            {"returnvalue",ParameterDirection.ReturnValue},
        };

        private static ParameterDirection ConvertDirection(string p)
        {
            return maps.ContainsKey(p) ? maps[p] : ParameterDirection.Input;
        }

        private static CommandType ConvertCommandType(string commandTypeText)
        {
            CommandType cmdType = CommandType.TableDirect; ;
            if (commandTypeText.Equals("text", StringComparison.OrdinalIgnoreCase))
            {
                cmdType = System.Data.CommandType.Text;
            }
            else if(commandTypeText.Equals("storedprocedure",StringComparison.OrdinalIgnoreCase))
            {
                cmdType = System.Data.CommandType.StoredProcedure;
            }
            return cmdType;
        }



        private static void AddParameters(SqlCommand command, List<XF.Common.Db.Parameter> dbParameters, ICriterion criterion, IContext context)
        {
            if (dbParameters != null)
            {
                if (criterion != null)
                {
                    foreach (var parm in dbParameters)
                    {

                        AddParameter(command, parm, criterion, context);
                    }                    
                }
                else
                {
                    int i = 0;
                    StringBuilder sb = new StringBuilder();
                    foreach (var parm in dbParameters)
	                {
                        if (i++ > 0)
                        {
                            sb.Append(",");
                        }
                        sb.Append(String.Format("{0}={1}", parm.Name, parm.Target));
	                }
                    context.SetError(500,String.Format("Client did not provider the expected parameters: {0}",sb.ToString()));
                }

            }
        }

        private static void AddParameter(SqlCommand command, XF.Common.Db.Parameter parm, ICriterion criterion, IContext context)
        {
                    SqlParameter p = new SqlParameter();
                    p.Direction = ConvertDirection(parm.Mode);
                    p.DbType = parm.DataType;
                    p.ParameterName = parm.Name;
                    p.Value = GetValue(parm.Target, parm.DataType, criterion,context);
                    command.Parameters.Add(p);
        }

        private static void AddParameters(SqlCommand cmd, List<XF.Common.Db.Parameter> dbParameters, T t, IContext context)
        {
            if (dbParameters != null && t != null)
            {
                List<PropertyInfo> infos = new List<PropertyInfo>(typeof(T).GetProperties());
                foreach (var parm in dbParameters)
                {
                    string paramName = parm.Name.Replace("@", String.Empty);

                    SqlParameter p = new SqlParameter();
                    p.Direction = ConvertDirection(parm.Mode);
                    p.DbType = parm.DataType;
                    p.ParameterName = String.Format("@{0}",paramName);

                    object o = null;
                    
                    var info = infos.Find(x => x.CanRead == true && x.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase));
                    if (info != null && info.CanRead)
                    {
                        if (info.PropertyType.Equals(typeof(DateTime)))
                        {
                            DateTime target = (DateTime)(object)info.GetValue(t, null);
                            if (target >= new DateTime(1753,1,1))
                            {
                                o = target;
                            }
                        }
                        else
                        {
                            o = info.GetValue(t, null);
                        }
                    }

                    if (parm.AllowsNull && o == null)
                    {
                        p.Value = DBNull.Value;
                        
                    }
                    else
                    {
                        p.Value = o;
                    }

                    cmd.Parameters.Add(p);

                }
            }
        }

    }
}
