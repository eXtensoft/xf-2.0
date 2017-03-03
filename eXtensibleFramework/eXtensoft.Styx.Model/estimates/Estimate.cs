using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Styx.ProjectManagement
{
    [DataContract]
    [Serializable]
    public class Estimate
    {
        [DataMember]
        [XmlAttribute("id")]
        public string EstimateId { get; set; }

        #region Estimates (List<EstimateValue>)

        private List<EstimateValue> _Estimates = new List<EstimateValue>();

        /// <summary>
        /// Gets or sets the List<EstimateValue> value for Estimates
        /// </summary>
        /// <value> The List<EstimateValue> value.</value>
        [DataMember]
        public List<EstimateValue> Estimates
        {
            get { return _Estimates; }
            set
            {
                if (_Estimates != value)
                {
                    _Estimates = value;
                }
            }
        }

        #endregion
    }
}
