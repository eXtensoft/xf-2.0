// <copyright company="eXtensible Solutions LLC" file="TypeMappedHandler.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
