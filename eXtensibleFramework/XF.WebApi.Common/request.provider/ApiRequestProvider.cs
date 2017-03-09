// <copyright company="eXtensoft, LLC" file="ApiRequestProvider.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using XF.Common;
    using System.ComponentModel.Composition;
    using System.Xml;
    using System.Xml.Serialization;
    using System.IO;

   // [InheritedExport(typeof(ITypeMap))]
    public abstract class ApiRequestProvider : IApiRequestProvider //, ITypeMap
    {

        //#region ITypeMap
        //string ITypeMap.Domain
        //{
        //    get
        //    {
        //        return ProviderKey;
        //    }
        //}

        //Type ITypeMap.KeyType
        //{
        //    get
        //    {
        //        return typeof(ApiRequest);
        //    }
        //}

        //Type ITypeMap.TypeResolution
        //{
        //    get
        //    {
        //        return this.GetType();
        //    }
        //}

        //string IApiRequestProvider.Key
        //{
        //    get
        //    {
        //        return ProviderKey;
        //    }
        //}
        //#endregion


        #region IApiRequestProvider

        string IApiRequestProvider.Key
        {
            get
            {
                return ProviderKey;
            }
        }
        ApiRequest IApiRequestProvider.Get(Guid id)
        {
            return Get(id);
        }

        ApiRequest IApiRequestProvider.Get(string id)
        {
            return Get(id);
        }

        Page<ApiRequest> IApiRequestProvider.Get(int pageIndex, int pageSize)
        {
            return Get(pageIndex, pageSize);
        }

        void IApiRequestProvider.Post(IEnumerable<ApiRequest> models)
        {
             Post(models);
        }

        void IApiRequestProvider.Post(ApiRequest model)
        {
            Post(model);
        }

        IEnumerable<ApiRequest> IApiRequestProvider.Get(int id)
        {
            return Get(id);
        }

        #endregion

        #region protected abstract
        protected abstract string ProviderKey { get; }
        protected abstract ApiRequest Get(Guid id);
        protected abstract ApiRequest Get(string id);
        protected abstract Page<ApiRequest> Get(int pageIndex, int pageSize);
        protected abstract IEnumerable<ApiRequest> Get(int id);
        protected abstract void Post(IEnumerable<ApiRequest> models);
        protected abstract void Post(ApiRequest model);

        #endregion
        protected static ApiRequest StringToRequest(string xml)
        {

            var t = Activator.CreateInstance<ApiRequest>();
            Type type = typeof(ApiRequest);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            XmlSerializer serializer = new XmlSerializer(type);
            using (MemoryStream stream = new MemoryStream())
            {
                xdoc.Save(stream);
                stream.Position = 0;
                t = (ApiRequest)serializer.Deserialize(stream);
            }

            return t;
        }

    }


}
