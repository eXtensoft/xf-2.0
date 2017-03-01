// <copyright company="eXtensible Solutions, LLC" file="ModelRequestService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public abstract class ModelRequestService
    {
        #region Context

        private IContext _Context = null;
        protected IContext Context
        {
            get
            {

                if (_Context == null)
                {
                    string key = (!String.IsNullOrWhiteSpace(eXtensibleConfig.Context)) ? eXtensibleConfig.Context : XFConstants.Context.DefaultApplication;
                    string instanceid = (!String.IsNullOrWhiteSpace(eXtensibleConfig.InstanceIdentifier)) ? eXtensibleConfig.InstanceIdentifier : String.Empty;
                    var appcontext = new ApplicationContext(key,IdentityProvider.UICulture,IdentityProvider.Username,eXtensibleConfig.Zone);
                    appcontext.Add(XFConstants.Context.ZONE, eXtensibleConfig.Zone);
                    _Context = appcontext;
                    
                }
                ApplicationContext ctx = _Context as ApplicationContext;
                if (ctx != null)
                {
                    return ctx.Prototype();
                }
                return _Context;
            }
        }

        #endregion Context

        #region UserIdentityProvider
        private IUserIdentityProvider _IdentityProvider;
        public IUserIdentityProvider IdentityProvider 
        {
            get
            {
                if (_IdentityProvider == null)
                {
                    _IdentityProvider = new WindowsIdentityProvider();
                }
                return _IdentityProvider;
            }
            set { _IdentityProvider = value; } 
        }

        #endregion UserIdentityProvider

    }
}
