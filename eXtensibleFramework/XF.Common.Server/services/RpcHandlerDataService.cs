// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using XF.Common;

    public class RpcHandlerDataService : IRpcDatastoreService
    {
        #region local fields

        private static ITypeMapCache _TypeCache;

        #endregion

        #region constructors

        public RpcHandlerDataService() { }

        static RpcHandlerDataService()
        {
            Initialize();
        }

        private static void Initialize()
        {
            List<string> list = new List<string>()
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"),
                eXtensibleConfig.RemoteProcedureCallPlugins
            };

            _TypeCache = new TypeMapCache(list);
            _TypeCache.Initialize();
        }
        #endregion

        #region interface implementations


        U IRpcDatastoreService.ExecuteRpc<T, U>(T model, ICriterion criterion, IRequestContext requestContext)
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            U u = default(U);
            try
            {
                u = ExecuteRpc<T, U>(model, criterion, requestContext);
            }
            catch (Exception ex)
            {
                var message = Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, model, criterion, requestContext);
                requestContext.SetError(500, message.ToPublish());
                requestContext.SetStacktrace(ex.StackTrace);
                var props = eXtensibleConfig.GetProperties();
                props.Add(XFConstants.Context.Ticket, message.Id);
                EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, props);
            }
            if (u == null)
            {
                u = default(U);
                requestContext.SetError(500, "Internal Server Error");
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelDataGateway.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
                ((Message<T>)requestContext).Items.Add(new TypedItem(XFConstants.Metrics.Criteria, criterion.ToShortString()) { Domain = XFConstants.Domain.Metrics });
            }
            return u;
        }

        #endregion


        private U ExecuteRpc<T, U>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            U result = default(U);
            var implementor = ResolveImplementor<T>(ModelActionOption.ExecuteAction, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Execute<U>(model, criterion, context);
            }

            return result;
        }


        private IRpcHander<T> ResolveImplementor<T>(ModelActionOption option, IContext context, T t, ICriterion criterion) where T : class, new()
        {
            IRpcHander<T> implementor = null;

            Type type = _TypeCache.ResolveType<T>();
            if (type == null)
            {
                var message = Exceptions.ComposeRpcImplementorResolutionError<T>(option, t, context);
                context.SetError(500, message.ToPublish());
                EventWriter.WriteError(message.ToLog(), SeverityType.Error);
            }
            else
            {
                implementor = Activator.CreateInstance(type) as IRpcHander<T>;
                if (implementor == null)
                {
                    var message = Exceptions.ComposeRpcImplementorInstantiationError<T>(option, t, context, type.FullName);
                    context.SetError(500, message.ToPublish());
                    var props = eXtensibleConfig.GetProperties();
                    props.Add(XFConstants.Context.Ticket, message.Id);
                    EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, props);
                }
                else
                {
                    EventWriter.Inform(String.Format("RpcHandler selected: {0}", type.FullName));
                    implementor.Context = context;
                }
            }
            return implementor;
        }


    }
}
