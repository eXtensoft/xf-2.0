// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.Quality
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class MetaChain
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ProcessSettings Settings { get; set; }


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

        #region Processors (List<MetaProcessor>)

        private List<MetaProcessor> _Processors;

        /// <summary>
        /// Gets or sets the List<MetaProcessor> value for Processors
        /// </summary>
        /// <value> The List<MetaProcessor> value.</value>

        public List<MetaProcessor> Processors
        {
            get { return _Processors; }
            set
            {
                if (_Processors != value)
                {
                    _Processors = value;
                }
            }
        }

        #endregion

    }
}
