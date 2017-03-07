// <copyright company="eXtensible Solutions, LLC" file="Message`1.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Wcf;

    //using XF.Common.Wcf;

    public class Message<T> : IMessage<T>, IRequest<T>, IResponse<T>, IRequestContext where T : class, new()
    {
        #region properties

        private List<T> _Content = new List<T>();
        public List<T> Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private List<TypedItem> _Items = new List<TypedItem>();
        public List<TypedItem> Items
        {
            get { return _Items; }
            set { _Items = value; }

        }

        public ICriterion Criterion { get; set; }

        public IEnumerable<IProjection> Display { get; set; }

        #endregion

        #region interface implementations

        IEnumerable<TypedItem> IMessage<T>.Context
        {
            get
            {
                return _Items.Where(x => x.Scope.Equals(XFConstants.Message.Context));
            }
            set
            {
                _Items.RemoveAll(x => x.Scope.Equals(XFConstants.Message.Context));
                foreach (var item in value)
                {
                    item.Scope = XFConstants.Message.Context;
                    _Items.Add(item);
                }
            }
        }

        string IRequest<T>.Verb
        {
            get
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
                return (found != null) ? found.Value.ToString() : XFConstants.Message.Verbs.None;
            }
            set
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
                if (found != null)
                {
                    _Items.Remove(found);
                }
                _Items.Add(new TypedItem() { Key = XFConstants.Message.Verb, Value = value });
            }
        }

        T IRequest<T>.Model
        {
            get
            {
                return Content.FirstOrDefault();
            }
            set
            {
                Content.Insert(0, value);
            }
        }

        IEnumerable<T> IRequest<T>.Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if (value != null)
                {
                    _Content = value.ToList();
                }                
            }
        }

        bool IResponse<T>.IsOkay
        {
            get
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.RequestStatus));
                return (found != null && (int)found.Value > 399) ? false : true;
            }
        }

        T IResponse<T>.Model
        {
            get { return Content.FirstOrDefault(); }
        }

        RequestStatus IResponse<T>.Status
        {
            get
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.RequestStatus));
                return (found != null) ? new RequestStatus() { Code = (int)found.Value, Description = found.Text } : new RequestStatus() { Code = 200, Description = RequestStatii.Http200 };

            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (T t in Content)
            {
                yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        #endregion

        #region local interface implementations



        #endregion

        #region constructors

        public Message(IEnumerable<TypedItem> items)
        {
            Items = items.ToList();
        }
        
        public Message(IContext context, ModelActionOption modelActionOption)
        {
            Items.Add(new TypedItem(XFConstants.Message.Verb, XFConstants.Message.VerbConstList[modelActionOption]) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.Application, context.ApplicationContextKey) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.USERIDENTITY, context.UserIdentity) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.UICULTURE, context.UICulture) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.ZONE, context.Zone) { Domain = XFConstants.Domain.Context });
            if (context.Claims != null && context.Claims.Count() > 0)
            {
                int i = 1;
                foreach (var item in context.Claims)
                {
                    Items.Add(new TypedItem(String.Format("{0}.{1}", XFConstants.Context.Claim, i++), item) 
                    { 
                        Domain = XFConstants.Domain.Claims 
                    });
                }
            }
            Items.Add(new TypedItem(XFConstants.Context.INSTANCEIDENTIFIER, Environment.MachineName) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.RequestBegin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            Items.Add(new TypedItem(XFConstants.Context.Model, GetModelType().FullName) { Domain = XFConstants.Domain.Context });
        }

        #endregion

        string IMessage<T>.ModelTypename
        {
            get { return GetType().FullName; }
        }


        string IMessage<T>.Verb
        {
            get 
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
                return (found != null) ? found.Value.ToString() : XFConstants.Message.Verbs.None;
            }
        }

        string IContext.ApplicationContextKey
        {
            get 
            {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.Application));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.DefaultApplication;

            }
        }

        string IContext.UserIdentity
        {
            get {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.USERIDENTITY));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.USERIDENTITY;         
            }
        }

        string IRequestContext.InstanceIdentifier
        {
            get {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.INSTANCEIDENTIFIER));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.INSTANCEIDENTIFIER;            
            }
        }

        IEnumerable<string> IContext.Claims
        {
            get { throw new NotImplementedException(); }
        }

        string IContext.UICulture
        {
            get {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.UICULTURE));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.UICULTURE;            
            }
        }

        bool IRequestContext.HasError()
        {
            bool b = false;
            var found = this._Items.Find(x=>x.Key.Equals(XFConstants.Message.RequestStatus));
            if (found != null)
            {
                b = true;
            }
            return b;
        }

        public bool TryAdd(object o)
        {
            bool b = o is T;
            if (b)
            {
                Content.Insert(0, o as T);
            }
            return b;
        }

        public void SetError(int code, string message)
        {
            this._Items.Add(new TypedItem() { Key = XFConstants.Message.RequestStatus, Value = code, Text = message, Tds = DateTime.Now });
        }

        void IRequestContext.SetMetric( string scope, string key, object value)
        {
            _Items.Add(new TypedItem(key, value) { Domain = XFConstants.Domain.Metrics, Scope = scope });
        }


        U IContext.GetValue<U>(string key)
        {
            U u = default(U);
            bool b = false;

            if (Items != null)
            {
                for (int i = 0; !b && i < Items.Count; i++)
                {
                    if (Items[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        TypedItem item = Items.Single(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

                        if (item != null)
                        {
                            u = (U)item.Value;
                        }
                        b = true;
                    }
                }
            }
            return u;           
        }

        IEnumerable<TypedItem> IContext.TypedItems
        {
            get { return Items; }
        }
        void IContext.Set<U>(U u)
        {
            if (typeof(T).IsSerializable)
            {
                try
                {
                    string s = GenericSerializer.GenericItemToParam<U>(u);
                    _Items.Add(new TypedItem(XFConstants.EventWriter.ModelT, u));
                }
                catch
                {
                    _Items.Add(new TypedItem(XFConstants.EventWriter.ModelT, "Model could not be serialized."));
                }

            }
        }

        public override string ToString()
        {
            var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
            string verb = (found != null) ? found.Value.ToString() : XFConstants.Message.Verbs.None;
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Message<{0}>.{1}", GetModelType().FullName, XFConstants.Message.ConstVerbList[verb]));
            foreach (var item in Items)
            {
                sb.AppendLine(String.Format("{0}.{1}.{2}", item.Domain, item.Key, item.Value.ToString()));
            }
            return sb.ToString();
        }

        private Type GetModelType()
        {
            T t = Activator.CreateInstance<T>();
            return t.GetType();
        }


        public virtual void Assimilate(DataPacket item)
        {
            item.Items.Clear();
            item.Items = this.Items;

            switch (item.ModelAction)
            {
                case ModelActionOption.None:
                    break;
                case ModelActionOption.Delete:
                    break;
                case ModelActionOption.Post:
                case ModelActionOption.Put:
                case ModelActionOption.Get:
                    if (this.Content != null && this.Content.Count >= 1 && this.Content[0] != null)
                    {
                        item.Buffer = GenericSerializer.ItemToByteArray(Content[0]);
                    }
                    break;
                case ModelActionOption.GetAll:
                case ModelActionOption.GetAllProjections:
                    if (this.Content != null)
                    {
                        item.Buffer = GenericSerializer.ItemToByteArray(this.Content);
                    }
                    break;
                case ModelActionOption.ExecuteAction:
                    break;
                case ModelActionOption.ExecuteCommand:
                    item.Tables = new DataSet();
                    if (this.Data != null && this.Data.Tables.Count > 0)
                    {
                        item.Tables = this.Data;
                    }
                    break;
                default:
                    break;
            }
        }

        string IContext.Zone
        {
            get 
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Context.ZONE));
                return (found != null) ? found.Value.ToString() : ZoneOption.Development.ToString();
            }
        }


        void IContext.SetStacktrace(string stackTrace)
        {
            _Items.Add(new TypedItem(XFConstants.EventWriter.StackTrace, stackTrace) { });
        }

        private DataSet _Data;
        public DataSet Data
        {
            get
            {
                if (_Data == null)
                {
                    _Data = new DataSet() { DataSetName = "DataSet" };
                }
                return _Data;
            }
            set
            {
                _Data = value;
            }
        }

        public void InsertContent(object o)
        {
            if (o is T)
            {
                Content.Insert(0, o as T);
            }
        }
    }
}
