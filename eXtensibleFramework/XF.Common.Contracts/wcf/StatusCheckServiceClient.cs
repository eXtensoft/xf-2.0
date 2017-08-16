// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.ServiceModel;

    public sealed class StatusCheckServiceClient
    {
        public StatusCheck ExecuteStatusCheck(string endpointName = "default")
        {
            StatusCheck item = StatusCheck.GenerateDefault();
            StatusCheck result = null;
            try
            {
                using (ChannelFactory<IStatusCheck> factory = new ChannelFactory<IStatusCheck>(endpointName))
                {
                    IStatusCheck proxy = factory.CreateChannel();
                    result = proxy.ExecuteStatusCheck(item);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
           
        }
    }
}
