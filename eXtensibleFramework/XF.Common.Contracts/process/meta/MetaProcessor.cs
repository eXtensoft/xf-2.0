using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XF.Quality
{
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
