// <copyright company="eXtensoft, LLC" file="ApiEndpointManager.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.WebApi.Core
{
    using XF.Common;
    using System;
    using System.Web.Hosting;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Text;
    using WebApi;
    public sealed class ApiEndpointManager : IEnumerable<IEndpointController>
    {
        #region local members
        private bool _IsInitialized = false;
        private List<IEndpointController> _OrderedControllers = new List<IEndpointController>();
        FileInfo info = null;
        #endregion

        #region properties
        [ImportMany(typeof(IEndpointController))]
        public List<IEndpointController> Controllers { get; set; }

        public bool IsDirty { get; set; }

        private List<Endpoint> _Endpoints = null;
        public IEnumerable<Endpoint> Endpoints
        {
            get
            {
                
                return _Endpoints;
            }
        }

        public IEndpointController CatchAllController { get; set; }
        #endregion

        #region constructors
        public ApiEndpointManager() { }

        static ApiEndpointManager()
        {
            InitializeConfiguration();
        }

        private static void InitializeConfiguration()
        {
            // read in xml file if exists

        }
        #endregion

        public bool Initialize()
        {
            string gg = eXtensibleWebApiConfig.CatchAll;
            Guid gid = new Guid(gg);

           _IsInitialized = Controllers != null && Controllers.Count() > 0;
            int i = 1;
            if (_IsInitialized)
            {
                List<Endpoint> list = new List<Endpoint>();


                string filepath = HostingEnvironment.MapPath("~/app_data/api.endpoints.xml");
                info = new FileInfo(filepath);
                if (!info.Exists)
                {
                    foreach (var endpoint in this)
                    {
                        var item = new Endpoint()
                        {
                            SortOrder = i++,
                            Id = endpoint.EndpointId.ToString(),
                            Name = endpoint.EndpointName,
                            Description = endpoint.EndpointDescription,
                            RoutePattern = endpoint.EndpointRouteTablePattern,
                            WhitelistPattern = endpoint.EndpointWhitelistPattern,
                            Version = endpoint.EndpointVersion
                        };
                        list.Add(item);
                    }

                    GenericSerializer.WriteGenericList<Endpoint>(list, info.FullName);
                }
                else
                {
                    List<Endpoint> endpoints = GenericSerializer.ReadGenericList<Endpoint>(info.FullName);

                    // IF endpoints came from existing, then order
                    List<Guid> burned = new List<Guid>();
                    
                    foreach (Endpoint orderedEndpoint in endpoints)
                    {
                        Guid g = new Guid(orderedEndpoint.Id);

                        var found = Controllers.Find(x => x.EndpointId.Equals(g));
                        if (found != null && found.EndpointId != gid)
                        {
                            if (found != null)
                            {
                                burned.Add(g);
                                _OrderedControllers.Add(found);
                            }                            
                        }

                    }
                    // add any discovered, unordered controllers
                    // to the tail of the explicitly ordered controllers
                    foreach (var controller in Controllers)
                    {
                        
                        if (!burned.Contains(controller.EndpointId) && controller.EndpointId != gid)
                        {
                            _OrderedControllers.Add(controller);
                        }
                    }
                    if (CatchAllController != null)
                    {
                        _OrderedControllers.Add(CatchAllController);
                    }
                    Controllers.Clear();
                    Controllers = _OrderedControllers;

                    foreach (IEndpointController item in _OrderedControllers)
                    {
                        Endpoint model = new Endpoint();
                        model.SortOrder = i++;
                        model.Id = item.EndpointId.ToString();
                        model.Name = item.EndpointName;
                        model.Description = item.EndpointDescription;
                        model.RoutePattern = item.EndpointRouteTablePattern;
                        model.WhitelistPattern = item.EndpointWhitelistPattern;
                        model.Version = item.EndpointVersion;
                        list.Add(model);
                    }



                    // if not under 'development', then persist the
                    // ordered controllers to datastore
                    if (eXtensibleWebApiConfig.IsEditRegistration)
                    {
                        var found = list.Find(x => new Guid(x.Id) == gid);
                        if (found != null)
                        {
                            list.Remove(found);
                        }
                        WriteToDatastore(list);
                    }

                }

                _Endpoints = list;
            }
            return _IsInitialized;
        }

        private void WriteToDatastore(List<Endpoint> list)
        {            
            GenericSerializer.WriteGenericList<Endpoint>(list, info.FullName);
        }

        public void SaveChanges() {
            if (IsDirty && eXtensibleWebApiConfig.IsEditRegistration)
            {
                string filepath = HostingEnvironment.MapPath("~/app_data/api.endpoints.xml");
                GenericSerializer.WriteGenericList<Endpoint>(_Endpoints, filepath);
                IsDirty = false;
            }

        }

        IEnumerator<IEndpointController> IEnumerable<IEndpointController>.GetEnumerator()
        {
            if (!_IsInitialized)
            {

            }
            else if (_OrderedControllers != null && _OrderedControllers.Count > 0)
            {
                foreach (IEndpointController controller in _OrderedControllers)
                {
                    yield return controller;
                }
            }
            else
            {
                foreach (IEndpointController controller in Controllers)
                {
                    yield return controller;
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Swap(Guid xId, Guid yId)
        {
            Endpoint xEndpoint = null;
            Endpoint yEndpoint = null;

            string xid = xId.ToString();
            string yid = yId.ToString();
            List<Endpoint> list = new List<Endpoint>();
            foreach (var endpoint in Endpoints)
            {
                if (!endpoint.Id.Equals(xid) && !endpoint.Id.Equals(yid))
                {
                    Endpoint item = endpoint;
                    list.Add(item);
                }
                else if(endpoint.Id.Equals(xid))
                {
                    Endpoint item = endpoint;
                    xEndpoint = item;
                }
                else if(endpoint.Id.Equals(yid))
                {
                    yEndpoint = endpoint;
                    list.Add(yEndpoint);
                    list.Add(xEndpoint);
                    IsDirty = true;
                }
            }
            _Endpoints = list;
        }
    }

}
