// <copyright company="eXtensible Solutions, LLC" file="PassThroughRpcRequestService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PassThroughRpcRequestService : ModelRequestService, IRpcRequestService
    {

        #region properties

        [Import(typeof(IRpcDatastoreService), AllowDefault=true)]
        public IRpcDataRequestService Service { get; set; }

        #endregion

        #region constructors

        public PassThroughRpcRequestService() { }

        public PassThroughRpcRequestService(IRpcDataRequestService service)
        {
            Service = service;
        }

        #endregion

        #region interface implementations (synchronous)

        IResponse<T, U> IRpcRequestService.ExecuteRpc<T, U>(T model, ICriterion criterion)
        {
            return ExecuteRpc<T, U>(model, criterion);
        }

        #endregion

        #region interface implementations (asynchronous)

        void IRpcRequestService.ExecuteRpcAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T, U>> callback)
        {
            new Action(async () =>
            {
                IResponse<T, U> result = await Task.Run<IResponse<T, U>>(() => ExecuteRpcAsync<T, U>(model, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        #endregion



        private IResponse<T, U> ExecuteRpc<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            IResponse<T, U> response = null;
            IRequest<T> request = new Message<T, U>(Context, ModelActionOption.ExecuteAction) { Criterion = criterion };

            request.Model = model;
            response = Service.ExecuteRpc<T, U>(request);

            return response;
        }

        private Task<IResponse<T, U>> ExecuteRpcAsync<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T, U>>(() => ExecuteRpc<T, U>(model, criterion));
        }



    }
}
