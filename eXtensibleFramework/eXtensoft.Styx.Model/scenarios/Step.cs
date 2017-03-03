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
    public class Step
    {
        [DataMember]
        [XmlAttribute("id")]
        public string StepId { get; set; }

        [DataMember]
        [XmlAttribute("personaId")]
        public string PersonaId { get; set; }

        [DataMember]
        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [DataMember]
        public StepTypeOption StepType { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }


        #region Minions (List<Step>)

        private List<Step> _Steps;

        /// <summary>
        /// Gets or sets the List<Step> value for Minions
        /// </summary>
        /// <value> The List<Step> value.</value>
        [DataMember]
        public List<Step> Steps
        {
            get { return _Steps; }
            set
            {
                if (_Steps != value)
                {
                    _Steps = value;
                }
            }
        }

        #endregion

    }
}
