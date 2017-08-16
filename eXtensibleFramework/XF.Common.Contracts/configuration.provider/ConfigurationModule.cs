// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Contracts
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    public class ConfigurationModule
    {
        [ImportMany(typeof(IConfigurationProvider))]
        public List<IConfigurationProvider> Providers { get; set; }

    }
}
