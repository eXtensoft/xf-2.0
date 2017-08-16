// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Reflection;

    [InheritedExport(typeof(ITypeMap))]
    public class RpcHandler<T> : IRpcHander<T> where T : class, new()
    {
        string ITypeMap.Domain
        {
            get { return String.Empty; }
        }

        Type ITypeMap.KeyType
        {
            get { return GetModelType(); }
        }

        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }

        private IContext _Context = null;
        IContext IRpcHander<T>.Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value;
            }
        }


        U IRpcHander<T>.Execute<U>(T t, ICriterion criterion, IContext context)
        {
            U u = default(U);
            object o = null;
            try
            {
                o = Execute<U>(t, criterion, context);
                if (o is U)
                {
                    u = (U)o;
                }
            }
            catch (Exception)
            {
            }
            return u;
        }


        public virtual object Execute<U>(T t, ICriterion criterion, IContext context)
        {
            return DynamicExecute<U>(t, criterion, context);
        }

        private object DynamicExecute<U>(T t, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            object o = null;

            if (criterion.Contains(XFConstants.Application.ActionExecuteStrategy))
            {
                string method = criterion.GetStringValue(XFConstants.Application.ActionExecuteMethodName);
                Type impl = GetType();
                MethodInfo[] infos = impl.GetMethods();
                MethodInfo info = this.GetType().GetMethod(method);
                if (info == null)
                {
                }
                else
                {
                    List<object> paramList = new List<object>();
                    int j = 1;
                    string key = String.Format("{0}:{1}", XFConstants.Application.StrategyKey, j.ToString());
                    foreach (var item in criterion.Items)
                    {
                        if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                        {
                            paramList.Add(item.Value);
                            j++;
                            key = String.Format("{0}:{1}", XFConstants.Application.StrategyKey, j.ToString());
                        }
                    }
                    try
                    {
                        o = info.Invoke(this, paramList.ToArray());
                    }
                    catch (Exception ex)
                    {
                        var message = Exceptions.ComposeSqlException<T>(ModelActionOption.ExecuteAction, ex, t, criterion, context, this.GetType().FullName);
                        context.SetError(500, message.ToPublish());
                        context.SetStacktrace(ex.StackTrace);
                        EventWriter.WriteError(message.ToLog(), SeverityType.Error, XFConstants.Category.DataAccess, context.ToDictionary(message.Id));
                    }

                    if (o is U)
                    {
                        return (U)o;
                    }
                }
            }
            return o;
        }

        private Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }



    }
}
