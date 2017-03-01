// <copyright company="eXtensible Solutions, LLC" file="PassThroughModelRequestService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data;
    using System.Threading.Tasks;
    using XF.Common.Extensions;

    public class PassThroughModelRequestService :ModelRequestService, IModelRequestService
    {
        #region local fields


        #endregion

        #region properties

        [Import(typeof(IDatastoreService), AllowDefault=true)]
        public IDataRequestService Service { get; set; }

        #endregion

        #region constructors

        public PassThroughModelRequestService() { }

        public PassThroughModelRequestService(IDataRequestService service)
        {
            Service = service;
        }

        #endregion

        #region interface implementations (synchronous)

        IResponse<T> IModelRequestService.Post<T>(T model)
        {
            return Post<T>(model);
        }

        IResponse<T> IModelRequestService.Put<T>(T model, ICriterion criterion)
        {
            return Put<T>(model,criterion);
        }

        IResponse<T> IModelRequestService.Delete<T>(ICriterion criterion)
        {
            return Delete<T>(criterion);
        }

        IResponse<T> IModelRequestService.Get<T>(ICriterion criterion)
        {
            return Get<T>(criterion);
        }

        IResponse<T> IModelRequestService.GetAll<T>(ICriterion criterion)
        {
            return GetAll<T>(criterion);
        }

        IResponse<T> IModelRequestService.GetAllProjections<T>(ICriterion criterion)
        {
            return GetAllProjections<T>(criterion);
        }


        IResponse<T,U> IModelRequestService.ExecuteAction<T,U>(T model, ICriterion criterion)
        {
            return ExecuteAction<T, U>(model, criterion);
        }

        IResponse<T> IModelRequestService.ExecuteCommand<T>(DataSet ds, ICriterion criterion)
        {
            return ExecuteCommand<T>(ds, criterion);
        }


        IResponse<T, U> IModelRequestService.ExecuteMany<T, U>(System.Collections.Generic.IEnumerable<T> list, ICriterion criterion)
        {
            return ExecuteMany<T, U>(list, criterion);
        }



        #endregion

        #region interface implementations (asynchronous)

        void IModelRequestService.PostAsync<T>(T model, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => PostAsync<T>(model));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.PutAsync<T>(T model, ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => PutAsync<T>(model,criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.DeleteAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => DeleteAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.GetAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => GetAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.GetAllAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => GetAllAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.GetAllProjectionsAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => GetAllProjectionsAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<IResponse<T,U>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T,U> result = await Task.Run<IResponse<T,U>>(() => ExecuteActionAsync<T, U>(model, criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.ExecuteCommand<T>(DataSet ds, ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
            {
                IResponse<T> result = await Task.Run<IResponse<T>>(() => ExecuteCommand<T>(ds, criterion));
                callback.Invoke(result);
            }).Invoke();
        }

        void IModelRequestService.ExecuteManyAsync<T, U>(IEnumerable<T> list, ICriterion criterion, Action<IResponse<T, U>> callback)
        {
            new Action(async () =>
            {
                IResponse<T, U> result = await Task.Run<IResponse<T, U>>(() => ExecuteManyAsync<T, U>(list, criterion));
                callback.Invoke(result);
            }).Invoke();            
        }

        #endregion

        #region sync helpers

        private IResponse<T> Post<T>(T model) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context,ModelActionOption.Post) { };

            request.Model = model;
            response = Service.Post<T>(request);

            if (eXtensibleConfig.CaptureMetrics && !typeof(T).IsSubclassOf(typeof(eXBase)))
            {
                
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T> Put<T>(T model, ICriterion criterion) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context, ModelActionOption.Put) { Criterion = criterion };

            request.Model = model;
            response = Service.Put<T>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T> Delete<T>(ICriterion criterion) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context,ModelActionOption.Delete) { Criterion = criterion };

            response = Service.Delete<T>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T> Get<T>(ICriterion criterion) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context,ModelActionOption.Get) { Criterion = criterion };

            response = Service.Get<T>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T> GetAll<T>(ICriterion criterion) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context, ModelActionOption.GetAll) { Criterion = criterion };

            response = Service.GetAll<T>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T> GetAllProjections<T>(ICriterion criterion) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context, ModelActionOption.GetAllProjections) { Criterion = criterion};
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelRequestService.Begin, DateTime.Now));
            }

            response = Service.GetAllProjections<T>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T,U> ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            IResponse<T,U> response = null;
            IRequest<T> request = new Message<T,U>(Context,ModelActionOption.ExecuteAction) { Criterion = criterion };

            request.Model = model;
            response =  Service.ExecuteAction<T, U>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T> ExecuteCommand<T>(DataSet ds, ICriterion criterion) where T : class, new()
        {
            IResponse<T> response = null;
            IRequest<T> request = new Message<T>(Context, ModelActionOption.ExecuteCommand) { Criterion = criterion };
            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.Scope.ModelRequestService.Begin, DateTime.Now));
            }

            response = Service.ExecuteCommand<T>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }

        private IResponse<T, U> ExecuteMany<T, U>(IEnumerable<T> list, ICriterion criterion) where T : class, new()
        {
            IResponse<T, U> response = null;
            IRequest<T> request = new Message<T, U>(Context, ModelActionOption.ExecuteMany) { Criterion = criterion };

            //request.Model = list;
            request.Content = list;
            response = Service.ExecuteMany<T, U>(request);

            if (eXtensibleConfig.CaptureMetrics)
            {
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Context.RequestEnd, DateTime.Now));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusCode, response.Status.Code));
                ((Message<T>)request).Items.Add(new TypedItem(XFConstants.Metrics.StatusDesc, response.Status.Description));
                eXMetric metric = response.ToMetric();
                EventWriter.WriteMetric(metric);
            }

            return response;
        }


        #endregion

        #region async helpers

        private Task<IResponse<T>> PostAsync<T>(T t) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Post<T>(t));
        }

        private Task<IResponse<T>> PutAsync<T>(T t, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Put<T>(t,criterion));
        }

        private Task<IResponse<T>> DeleteAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Delete<T>(criterion));
        }

        private Task<IResponse<T>> GetAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Get<T>(criterion));
        }

        private Task<IResponse<T>> GetAllAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => GetAll<T>(criterion));
        }

        private Task<IResponse<T>> GetAllProjectionsAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => GetAllProjections<T>(criterion));
        }

        private Task<IResponse<T,U>> ExecuteActionAsync<T,U>(T model, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T,U>>(() => ExecuteAction<T,U>(model,criterion));
        }

        private Task<IResponse<T>> ExecuteCommandAsync<T>(DataSet ds, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => ExecuteCommand<T>(ds, criterion));
        }

        private Task<IResponse<T, U>> ExecuteManyAsync<T, U>(IEnumerable<T> list, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T, U>>(() => ExecuteMany<T, U>(list, criterion));
        }

        #endregion


    }
}
