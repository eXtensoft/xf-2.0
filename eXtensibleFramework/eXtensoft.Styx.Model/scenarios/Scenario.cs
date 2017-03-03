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
    public class Scenario
    {
        #region properties
        [DataMember]
        [XmlAttribute("id")]
        public string ScenarioId { get; set; }
        [DataMember]
        [XmlAttribute("type")]
        public ScenarioTypeOption ScenarioType { get; set; }
        [DataMember]
        [XmlAttribute("identifier")]
        public string Identifier { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Goal { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public Persona PrimaryActor { get; set; }
        [DataMember]
        public string Group { get; set; }

        #region Conditions (List<Condition>)

        private List<Condition> _Conditions = new List<Condition>();

        /// <summary>
        /// Gets or sets the List<Condition> value for Conditions
        /// </summary>
        /// <value> The List<Condition> value.</value>
        [DataMember]
        [XmlElement]
        public List<Condition> Conditions
        {
            get { return _Conditions; }
            set
            {
                if (_Conditions != value)
                {
                    _Conditions = value;
                }
            }
        }

        #endregion

        #region SupportingActors (List<ActorRole>)

        private List<Persona> _SupportingActors = new List<Persona>();

        /// <summary>
        /// Gets or sets the List<ActorRole> value for SupportingActors
        /// </summary>
        /// <value> The List<ActorRole> value.</value>
        [DataMember]
        public List<Persona> SupportingActors
        {
            get { return _SupportingActors; }
            set
            {
                if (_SupportingActors != value)
                {
                    _SupportingActors = value;
                }
            }
        }

        #endregion

        #region AcceptanceCriteria (List<AcceptanceCriterion>)

        private List<AcceptanceCriterion> _AcceptanceCriteria;

        /// <summary>
        /// Gets or sets the List<AcceptanceCriterion> value for AcceptanceCriteria
        /// </summary>
        /// <value> The List<AcceptanceCriterion> value.</value>
        [DataMember]
        public List<AcceptanceCriterion> AcceptanceCriteria
        {
            get { return _AcceptanceCriteria; }
            set
            {
                if (_AcceptanceCriteria != value)
                {
                    _AcceptanceCriteria = value;
                }
            }
        }

        #endregion
        // related non-functional requirements

        #region Steps (List<Step>)

        private List<Step> _Steps = new List<Step>();

        /// <summary>
        /// Gets or sets the List<Step> value for Steps
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


        #region Items (List<ListItem>)

        private List<ListItem> _Items = new List<ListItem>();

        /// <summary>
        /// Gets or sets the List<ListItem> value for Items
        /// </summary>
        /// <value> The List<ListItem> value.</value>
        [DataMember]
        [XmlElement]
        public List<ListItem> Items
        {
            get { return _Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                }
            }
        }

        #endregion

        #endregion
    
    }

}
