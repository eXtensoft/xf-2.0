// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
