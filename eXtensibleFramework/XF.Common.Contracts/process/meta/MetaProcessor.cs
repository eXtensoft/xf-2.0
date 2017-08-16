// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Quality
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class MetaProcessor
    {

        [XmlAttribute("name")]
        public string Name { get; set; }

        

        #region Parameters (List<ProcessParameter>)

        private List<ProcessParameter> _Parameters = new List<ProcessParameter>();

        /// <summary>
        /// Gets or sets the List<ProcessParameter> value for Parameters
        /// </summary>
        /// <value> The List<ProcessParameter> value.</value>

        public List<ProcessParameter> Parameters
        {
            get { return _Parameters; }
            set
            {
                if (_Parameters != value)
                {
                    _Parameters = value;
                }
            }
        }

        #endregion






    }
}
