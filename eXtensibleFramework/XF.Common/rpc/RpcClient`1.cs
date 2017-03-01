using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XF.Common
{
    public class RpcClient<T> : DynamicObject where T : class, new()
    {
        #region local fields

        private IRpcRequestService _Service = null;

        #endregion

        #region constructors

        public RpcClient(IRpcRequestService service)
        {
            _Service = service;
        }

        #endregion



        #region overrides

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Type contracts = typeof(IRpcRequestService);
            MethodInfo methodInfo = contracts.GetMethod("ExecuteRpc");
            Type t = typeof(T);
            Type u = binder.ReturnType;
            ICriterion c = new Criterion();
            c.AddItem(XFConstants.Application.ActionResultModelType, u.AssemblyQualifiedName);
            c.AddItem(XFConstants.Application.ActionExecuteMethodName, binder.Name);
            c.AddItem(XFConstants.Application.ActionExecuteStrategy, XFConstants.Application.ActionExecuteStrategyDynamic);

            int i = 1;
            foreach (var arg in args)
            {
                string s = String.Format("", XFConstants.Application.StrategyKey,i.ToString());
                c.AddItem(s, arg);
                i++;
            }

            MethodInfo constructed = methodInfo.MakeGenericMethod(t, u);
            object[] methodArgs = { null, c };

            result = constructed.Invoke(_Service, methodArgs);
            return (result != null) ? true : false;

        }

        #endregion





    }
}
