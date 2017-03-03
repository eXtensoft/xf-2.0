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
    public class ScenarioContext
    {
        #region properties
        [XmlAttribute("id")]
        public string ScenarioContextId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ValueProposition { get; set; }

        public string Group { get; set; }

        #region UseCases (List<UseCase>)

        private List<Scenario> _Scenarios = new List<Scenario>();

        /// <summary>
        /// Gets or sets the List<UseCase> value for UseCases
        /// </summary>
        /// <value> The List<UseCase> value.</value>

        public List<Scenario> Scenarios
        {
            get { return _Scenarios; }
            set
            {
                if (_Scenarios != value)
                {
                    _Scenarios = value;
                }
            }
        }

        #endregion

        #region Actors (List<Persona>)

        private List<Persona> _Personas;

        /// <summary>
        /// Gets or sets the List<Persona> value for Personas
        /// </summary>
        /// <value> The List<Persona> value.</value>
        [XmlElement]
        public List<Persona> Personas
        {
            get { return _Personas; }
            set
            {
                if (_Personas != value)
                {
                    _Personas = value;
                }
            }
        }

        #endregion

        #endregion

    }
}
