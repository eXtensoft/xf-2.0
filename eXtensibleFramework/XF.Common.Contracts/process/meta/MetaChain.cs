using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XF.Quality
{
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
