// <copyright company="eXtensible Solutions, LLC" file="TypeMapContainer.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    public sealed class TypeMapContainer
    {

        [ImportMany(typeof(ITypeMap))]
        public List<ITypeMap> TypeMaps { get; set; }
    }
}
