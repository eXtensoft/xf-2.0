// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.ComponentModel.Composition;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class TypeMappedHandler<T> : ITypeMap where T : class, new()
    {

        string ITypeMap.Domain
        {
            get { return Domain; }
        }

        Type ITypeMap.KeyType
        {
            get { return GetModelType(); }
        }

        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }

        public abstract string Domain{get;set;}


        private Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }
    }
}
