// <copyright company="eXtensible Solutions LLC" file="RpcDataRequestSerivce.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public class RpcDataRequestService : IRpcDataRequestService
    {
        #region properties

        public IRpcDatastoreService Service { get; set; }


        #endregion

        #region constructors

        public RpcDataRequestService(IRpcDatastoreService service)
        {
            Service = service;
        }

        #endregion

        IResponse<T, U> IRpcDataRequestService.ExecuteRpc<T, U>(IRequest<T> request)
        {
            return ExecuteRpc<T, U>(request);
        }

        private IResponse<T, U> ExecuteRpc<T, U>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IResponse<T, U> response = request as IResponse<T, U>;
            if (response != null)
            {
                IRequestContext context = request as IRequestContext;
                response.ActionResult = Service.ExecuteRpc<T, U>(request.Model, request.Criterion, context);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return response;
        }

    }
}
