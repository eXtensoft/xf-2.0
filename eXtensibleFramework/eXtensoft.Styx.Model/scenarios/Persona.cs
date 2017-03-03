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
    public class Persona
    {

        #region properties

        [DataMember]
        [XmlAttribute("id")]
        public string PersonaId { get; set; }
        [DataMember]
        [XmlText]
        public string Title { get; set; }
        [DataMember]
        public string Group { get; set; }

        List<string> KeyCharacteristics { get; }
        List<string> Goals { get; }
        List<string> Influencers { get; }
        List<string> FrustrationsPainPoints { get; }

        [DataMember]
        public List<ListItem> Items { get; set; }

        #endregion

    }
}
