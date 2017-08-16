// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class Builder<T>
    {
        private List<DataMap> _Maps = null;

        public bool IsInitialized { get; set; }

        #region Indices

        private Dictionary<string, int> _indices;

        public Dictionary<string, int> Indices
        {
            get { return _indices; }
            set { _indices = value; }
        }

        #endregion Indices

        public T Model { get; set; }

        public Builder()
        {
        }

        public Builder(List<DataMap> maps)
        {
            _Maps = maps;
        }

        public void Build(List<T> list)
        {
            list.Add(Model);
        }

        public int GetIndex(string s)
        {
            if (_indices.ContainsKey(s))
            {
                return _indices[s];
            }
            else
            {
                return -1;
            }
        }

        public void InitializeIndices(IDataReader reader)
        {
            _indices = new Dictionary<string, int>();
            if (_Maps != null)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = ResolveName(reader.GetName(i));
                    _indices.Add(name, i);
                }
            }
            else
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    _indices.Add(reader.GetName(i), i);
                }
            }
            IsInitialized = true;
        }

        private string ResolveName(string readerColumnName)
        {
            string propertyname = readerColumnName;
            try
            {
                var found = _Maps.FirstOrDefault(x => x.ColumnName.Equals(readerColumnName, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    propertyname = found.PropertyName;
                }
            }
            catch
            {
            }

            return propertyname;
        }
    }
}