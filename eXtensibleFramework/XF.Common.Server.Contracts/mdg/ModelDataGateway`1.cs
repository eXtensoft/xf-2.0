// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data;

    [InheritedExport(typeof(ITypeMap))]
    public class ModelDataGateway<T> : IModelDataGateway<T> where T : class, new()
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

        private IDatastoreService _DataService;
        IDatastoreService IModelDataGateway<T>.DataService
        {
            get
            {
                return _DataService;
            }
            set
            {
                _DataService = value;
            }
        }

        private IContext _Context = null;

        IContext IModelDataGateway<T>.Context
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


        T IModelDataGateway<T>.Post(T t, IContext context)
        {
            return Post(t, context);
        }

        T IModelDataGateway<T>.Get(ICriterion criterion, IContext context)
        {
            return Get(criterion, context);
        }

        T IModelDataGateway<T>.Put(T t, ICriterion criterion, IContext context)
        {
            return Put(t, criterion, context);
        }

        ICriterion IModelDataGateway<T>.Delete(ICriterion criterion, IContext context)
        {
            return Delete(criterion, context);
        }

        IEnumerable<T> IModelDataGateway<T>.GetAll(ICriterion criterion, IContext context)
        {
            return GetAll(criterion, context);
        }

        IEnumerable<Projection> IModelDataGateway<T>.GetAllProjections(ICriterion criterion, IContext context)
        {
            return GetAllProjections(criterion, context);
        }

        U IModelDataGateway<T>.ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            U u = default(U);
            object o = null;
            try
            {
                o = ExecuteAction<U>(t, criterion, context);
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

        DataSet IModelDataGateway<T>.ExecuteCommand(DataSet ds, ICriterion criterion, IContext context)
        {
            return ExecuteCommand(ds, criterion, context);
        }

        U IModelDataGateway<T>.ExecuteMany<U>(IEnumerable<T> list, ICriterion criterion, IContext context)
        {
            U u = default(U);
            object o = null;
            try
            {
                o = ExecuteMany<U>(list, criterion, context);
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


        #region helper methods

        public virtual T Post(T t, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Post.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
            //context.SetError(501, message);
        }

        public virtual T Get(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Get.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual T Put(T t, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Put.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Delete.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.GetAll.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual IEnumerable<Projection> GetAllProjections(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.GetAllProjections.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual U ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.ExecuteAction.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual DataSet ExecuteCommand(DataSet ds, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.ExecuteCommand.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        public virtual U ExecuteMany<U>(IEnumerable<T> t, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}>", this.GetType().FullName, ModelActionOption.ExecuteMany.ToString(), GetModelType().FullName);
            throw new NotImplementedException(message);
        }

        private Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }
        #endregion


    }
}
