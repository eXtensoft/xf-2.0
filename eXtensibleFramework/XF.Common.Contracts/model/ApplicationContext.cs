// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ApplicationContext : IContext
    {
        private string _ApplicationContextKey = XFConstants.Context.DefaultApplication;
        string IContext.ApplicationContextKey
        {
            get { return _ApplicationContextKey; }
        }

        private string _Zone = ZoneOption.Development.ToString();
        string IContext.Zone
        {
            get { return _Zone; }
        }

        private string _UserIdentity = String.Empty;
        string IContext.UserIdentity
        {
            get { return _UserIdentity; }
        }

        private List<string> _Claims = new List<string>();
        IEnumerable<string> IContext.Claims
        {
            get { return _Claims; }
        }

        private string _UICulture = String.Empty;
        string IContext.UICulture
        {
            get { return _UICulture; }
        }

        T IContext.GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }

        private List<TypedItem> _Items = new List<TypedItem>();
        IEnumerable<TypedItem> IContext.TypedItems
        {
            get { return _Items; }
        }

        
        void IContext.SetError(int errorCode, string errorMessage)
        {
            throw new NotImplementedException();
        }

        public ApplicationContext(string applicationContextKey, string instanceId) 
        {
            _ApplicationContextKey = applicationContextKey;
        }

        public ApplicationContext(string applicationContextKey,  string uiCulture, string userIdentity, string zone = "Development")
        {
            _ApplicationContextKey = applicationContextKey;
            _UICulture = uiCulture;
            _UserIdentity = userIdentity;
            _Zone = zone;
        }


        public ApplicationContext Prototype()
        {

            ApplicationContext ctx = new ApplicationContext(_ApplicationContextKey, _UICulture, _UserIdentity, _Zone);
            return ctx;
        }

        public void Add(string key, object value)
        {
            _Items.Add(new TypedItem(key, value));
        }


        void IContext.SetStacktrace(string stackTrace)
        {
            _Items.Add(new TypedItem(XFConstants.EventWriter.StackTrace, stackTrace));
        }

        void IContext.Set<T>(T t)
        {
        //    if (typeof(T).IsSerializable)
        //    {
        //        try
        //        {
        //            string s = GenericSerializer.GenericItemToParam<T>(t);
        //            _Items.Add(new TypedItem(XFConstants.EventWriter.ModelT, t));
        //        }
        //        catch
        //        {
        //            _Items.Add(new TypedItem(XFConstants.EventWriter.ModelT,"Model could not be serialized."));
        //        }
                
        //    }           
        }



    }
}
