// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Text;

    public static class Exceptions
    {
        private static string ErrorMessage = "An error has occurred";

        public static ExceptionMessage ComposeDbConnectionNullSettingsError<T>(ModelActionOption modelActionOption, T t, IContext context, string connectionStringKey) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                var verboseMessage = String.Format(ErrorMessages.DbConnectionStringNullSettingsVerbose, GetModelType<T>().FullName, modelActionOption, connectionStringKey, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //var simpleMessage = String.Format(ErrorMessages.DbConnectionStringNullSettings, GetModelType<T>().FullName, modelActionOption, connectionStringKey);
            //eXtensibleConfig.BigDataUrl
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
            
        }

        public static ExceptionMessage ComposeDbConnectionStringFormatError<T>(ModelActionOption modelActionOption, T t, IContext context, string connectionStringKey, string connectionString) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();

            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
            StringBuilder sb = new StringBuilder();
            if (t != null)
            {
                sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
            }
            sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
            //var verboseMessage = String.Format(ErrorMessages.DbConnectionStringNullSettingsVerbose, GetModelType<T>().FullName, modelActionOption, connectionStringKey, sb.ToString(), message.Id);
            var verboseMessage = String.Format(ErrorMessages.DbConnectionStringFormatVerbose, connectionStringKey, connectionString, GetModelType<T>().FullName, message.Id);
            message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
            //var simpleMessage = String.Format(ErrorMessages.DbConnectionStringNullSettings, GetModelType<T>().FullName, modelActionOption, connectionStringKey);
            message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
            //string s = String.Format("Improperly formatted Connection string ({0}={1})",connectionStringKey, settings.ConnectionString);
        }

        public static ExceptionMessage ComposeDbConnectionCreationError<T>(ModelActionOption modelActionOption, T t, IContext context, string connectionStringKey) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.DbConnectionCreationFormatVerbose, GetModelType<T>().FullName, modelActionOption,connectionStringKey, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.DbConnectionCreationFormat, GetModelType<T>().FullName, modelActionOption,connectionStringKey);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
        }

        public static ExceptionMessage ComposeDbConnectionStringKeyResolutionError<T>(ModelActionOption modelActionOption, T t, IContext context) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.DbConnectionStringResolutionVerbose, GetModelType<T>().FullName, modelActionOption,sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.DbConnectionStringResolution, GetModelType<T>().FullName, modelActionOption);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
        }

        public static ExceptionMessage ComposeImplementorInstantiationError<T>(ModelActionOption modelActionOption, T t, IContext context, string implementor) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.ModelDataGatewayImplementationInstantiationVerbose, GetModelType<T>().FullName, modelActionOption,implementor, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.ModelDataGatewayImplementationInstantiation, GetModelType<T>().FullName, modelActionOption,implementor);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}           
            return message;
        }

        public static ExceptionMessage ComposeImplementorResolutionError<T>(ModelActionOption modelActionOption, T t, IContext context) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            string m = String.Empty;
            StringBuilder sb = new StringBuilder();
            if (t != null)
            {
                sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
            }
            sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
            m = String.Format(ErrorMessages.NullModelDataGatewayImplementation, GetModelType<T>().FullName, modelActionOption,sb.ToString(),message.Id);
            message.Add(ExceptionMessageOption.Log,m);

            //string simpleMessage = String.Format(ErrorMessages.NullModelDataGatewayImplementation, GetModelType<T>().FullName, modelActionOption);
            message.Add(ExceptionMessageOption.Publish, ErrorMessage);

            return message;
        }

        public static ExceptionMessage ComposeRpcImplementorInstantiationError<T>(ModelActionOption modelActionOption, T t, IContext context, string implementor) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
            StringBuilder sb = new StringBuilder();
            if (t != null)
            {
                sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
            }
            sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
            string verboseMessage = String.Format(ErrorMessages.ModelDataGatewayImplementationInstantiationVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString(), message.Id);
            message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
            //string simpleMessage = String.Format(ErrorMessages.ModelDataGatewayImplementationInstantiation, GetModelType<T>().FullName, modelActionOption,implementor);
            message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}           
            return message;
        }

        public static ExceptionMessage ComposeRpcImplementorResolutionError<T>(ModelActionOption modelActionOption, T t, IContext context) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            string m = String.Empty;
            StringBuilder sb = new StringBuilder();
            if (t != null)
            {
                sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
            }
            sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
            m = String.Format(ErrorMessages.NullRpcHandlerImplementation, GetModelType<T>().FullName, modelActionOption, sb.ToString(), message.Id);
            message.Add(ExceptionMessageOption.Log, m);

            //string simpleMessage = String.Format(ErrorMessages.NullModelDataGatewayImplementation, GetModelType<T>().FullName, modelActionOption);
            message.Add(ExceptionMessageOption.Publish, ErrorMessage);

            return message;
        }


        public static ExceptionMessage ComposeNullSqlCommand<T>(ModelActionOption modelActionOption, T t, ICriterion criterion, IContext context, string implementor) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.NullSqlCommandFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.NullSqlCommandFormat, GetModelType<T>().FullName, modelActionOption, implementor);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
        }

        public static ExceptionMessage ComposeSqlException<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, string implementor,string dataSource = "") where T : class, new()
        {

            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (!String.IsNullOrWhiteSpace(dataSource))
                {
                    sb.AppendLine(dataSource);
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.SqlExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.SqlExceptionFormat, GetModelType<T>().FullName, modelActionOption, implementor);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            message.Add(ExceptionMessageOption.Stacktrace, ex.StackTrace);
            return message;
        }

        public static ExceptionMessage ComposeDatastoreException<T>(ModelActionOption modelActionOption, string ex, T t, ICriterion criterion, IContext context, string implementor, string datastoreType = "Datastore", string dataSource = "") where T : class, new()
        {

            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, ex));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (!String.IsNullOrWhiteSpace(dataSource))
                {
                    sb.AppendLine(dataSource);
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.DatastoreExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, datastoreType, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.SqlExceptionFormat, GetModelType<T>().FullName, modelActionOption, implementor, datastoreType);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
        }

        public static ExceptionMessage ComposeBorrowReaderError<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, string implementor) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.BorrowReaderExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.BorrowReaderExceptionFormat, GetModelType<T>().FullName, modelActionOption, implementor);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            message.Add(ExceptionMessageOption.Stacktrace, ex.StackTrace);
            return message;
        }

        public static ExceptionMessage ComposeGeneralException(Exception ex, string messageText, string implementor)
        {
            ExceptionMessage message = new ExceptionMessage();
            StringBuilder sb = new StringBuilder();
            sb.Append( messageText + ": ");
            string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            sb.Append(s);
            string t = String.Format(ErrorMessages.ExceptionFormat, sb.ToString());
            message.Add(ExceptionMessageOption.Log, t);
            message.Add(ExceptionMessageOption.Stacktrace, ex.StackTrace);
            return message;
        }

        public static ExceptionMessage ComposeGeneralExceptionError<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, string implementor) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.GeneralExceptionInFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.GeneralExceptionInFormat, GetModelType<T>().FullName, modelActionOption, implementor);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            message.Add(ExceptionMessageOption.Stacktrace, ex.StackTrace);
            return message;
        }

        public static ExceptionMessage ComposeGeneralExceptionError<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.GeneralExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption,  sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.GeneralExceptionFormat, GetModelType<T>().FullName, modelActionOption);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            message.Add(ExceptionMessageOption.Stacktrace, ex.StackTrace);
            return message;
        }

        public static ExceptionMessage ComposeResourceNotFoundError<T>(ModelActionOption modelActionOption, T t, ICriterion criterion, IContext context) where T : class, new()
        {
            ExceptionMessage message = new ExceptionMessage();
            //if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            //{
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                string verboseMessage = String.Format(ErrorMessages.ResourceNotFoundFormatVerbose, GetModelType<T>().FullName, modelActionOption, sb.ToString(),message.Id);
                message.Add(ExceptionMessageOption.Log, verboseMessage);
            //}
            //else
            //{
                //string simpleMessage = String.Format(ErrorMessages.ResourceNotFoundFormat, GetModelType<T>().FullName, modelActionOption);
                message.Add(ExceptionMessageOption.Publish, ErrorMessage);
            //}
            return message;
        }

        private static Type GetModelType<T>() where T : class, new()
        {
            return Activator.CreateInstance<T>().GetType();
        }


        
    }
}
