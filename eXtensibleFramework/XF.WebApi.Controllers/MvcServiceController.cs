// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using XF.Common;
    using XF.DataServices;

    public abstract class MvcServiceController
    {
        private IModelRequestService _Service = null;
        protected IModelRequestService Service
        {
            get
            {
                if (_Service == null)
                {
                    _Service = new PassThroughModelRequestService(
                        new DataRequestService(new ModelDataGatewayDataService())
                        );
                }
                return _Service;
            }
            set
            {
                _Service = value;
            }
        }
    }
}
