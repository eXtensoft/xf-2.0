// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Configuration;

    public sealed class eXtensibleStrategySectionGroup : ConfigurationSectionGroup
    {
        public bool ContainsSectionContext(string name, out eXtensibleStrategySection found)
        {
            bool b = false;
            found = null;
            for (int i = 0; !b && i < this.Sections.Count; i++)
            {
                eXtensibleStrategySection section = this.Sections[i] as eXtensibleStrategySection;
                if (section.Context.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    found = section;
                    b = true;
                }
            }
            return b;
        }
    }
}