// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;

    public class DataRequestService : IDataRequestService
    {
        #region properties

        public IDatastoreService Service { get; set; }


        #endregion

        #region constructors

        public DataRequestService() { }

        public DataRequestService(IDatastoreService service)
        {
            Service = service;
        }

        #endregion

        #region interface implementations

        IResponse<T> IDataRequestService.Post<T>(IRequest<T> request)
        {
            return Post<T>(request);
        }

        IResponse<T> IDataRequestService.Put<T>(IRequest<T> request)
        {
            return Put<T>(request);
        }

        IResponse<T> IDataRequestService.Delete<T>(IRequest<T> request)
        {
            return Delete<T>(request);
        }

        IResponse<T> IDataRequestService.Get<T>(IRequest<T> request)
        {
            return Get<T>(request);
        }

        IResponse<T> IDataRequestService.GetAll<T>(IRequest<T> request)
        {
            return GetAll<T>(request);
        }

        IResponse<T> IDataRequestService.GetAllProjections<T>(IRequest<T> request)
        {
            return GetAllDisplay<T>(request);
        }


        IResponse<T,U> IDataRequestService.ExecuteAction<T, U>(IRequest<T> request)
        {
            return ExecuteAction<T,U>(request);
        }

        IResponse<T> IDataRequestService.ExecuteCommand<T>(IRequest<T> request)
        {
            return ExecuteCommand<T>(request);
        }


        IResponse<T, U> IDataRequestService.ExecuteMany<T, U>(IRequest<T> request)
        {
            return ExecuteMany<T, U>(request);
        }

        #endregion

        #region helper methods

        private IResponse<T> Post<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;

            if (context != null)
            {
                request.Model = Service.Post<T>(request.Model, context);
            }

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });

            }
            return request as IResponse<T>;
        }

        private IResponse<T> Put<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;
            if (context != null)
            {
                request.Model = Service.Put<T>(request.Model, request.Criterion, context);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return request as IResponse<T>;
        }

        private IResponse<T> Delete<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;
            if (context != null)
            {
                //request.ActionResult = Service.Delete<T>(request.Criterion, context);
                ICriterion criterion = Service.Delete<T>(request.Criterion, context);
                if (criterion != null)
                {
                
                }
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return request as IResponse<T>;
        }

        private IResponse<T> Get<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;
            if (context != null)
            {
                request.Model = Service.Get<T>(request.Criterion, context);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return request as IResponse<T>;
        }

        private IResponse<T> GetAll<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;
            if (context != null)
            {
                request.Content = Service.GetAll<T>(request.Criterion, context);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return request as IResponse<T>;
        }

        private IResponse<T> GetAllDisplay<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;
            if (context != null)
            {
                request.Display = Service.GetAllProjections<T>(request.Criterion, context);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return request as IResponse<T>;
        }

        private IResponse<T,U> ExecuteAction<T, U>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IResponse<T, U> response = request as IResponse<T, U>;            
            if (response != null)
            {
                IRequestContext context = request as IRequestContext;
                if (context != null)
                {
                    response.ActionResult = Service.ExecuteAction<T, U>(request.Model, request.Criterion, context);
                }                
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return response;
        }

        private IResponse<T> ExecuteCommand<T>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IRequestContext context = request as IRequestContext;
            if (context != null)
            {
                request.Data = Service.ExecuteCommand<T>(request.Data, request.Criterion, context);
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return request as IResponse<T>;
        }

        private IResponse<T, U> ExecuteMany<T, U>(IRequest<T> request) where T : class, new()
        {
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            IResponse<T, U> response = request as IResponse<T, U>;
            if (response != null)
            {
                IRequestContext context = request as IRequestContext;
                if (context != null)
                {
                    response.ActionResult = Service.ExecuteMany<T, U>(request.Content, request.Criterion, context);
                }
                
            }
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            }
            return response;
        }

        //private IResponse<T, U> Execute<T, U>(IRequest<T> request) where T : class, new()
        //{
        //    if (eXtensibleConfig.CaptureMetrics)
        //    {
        //        ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.Begin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
        //    }
        //    IResponse<T, U> response = request as IResponse<T, U>;
        //    if (response != null)
        //    {
        //        IRequestContext context = request as IRequestContext;
        //        response.ActionResult = Service.Execute<T, U>(request.Model, request.Criterion, context);
        //    }
        //    if (eXtensibleConfig.CaptureMetrics)
        //    {
        //        ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.DataRequestService.End, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
        //    }
        //    return response;
        //}



        #endregion

    }
}
