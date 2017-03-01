// <copyright company="eXtensible Solutions, LLC" file="eXtensibleStrategySectionGroup.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

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