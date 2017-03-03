using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Styx.ProjectManagement
{
    [DataContract]
    [Serializable]
    public class Task : Item
    {
        [DataMember]
        [XmlIgnore]
        public string TaskId { get; set; }

        [DataMember]
        [XmlAttribute("phase")]
        public string Phase { get; set; }

        [DataMember]
        [XmlAttribute("state")]
        public string CurrentState { get; set; }

        [DataMember]
        [XmlAttribute("grp")]
        public string Group { get; set; }

        [DataMember]
        [XmlAttribute("masterId")]
        public string MasterId { get; set; }
        [DataMember]
        [XmlAttribute("noteId")]
        public int NoteId { get; set; }

        [DataMember]
        public string TaskType { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }




        #region Tasks (List<Task>)

        private List<Task> _Tasks;

        /// <summary>
        /// Gets or sets the List<Task> value for Tasks
        /// </summary>
        /// <value> The List<Task> value.</value>
        [DataMember]
        [XmlIgnore]
        public List<Task> Tasks
        {
            get { return _Tasks; }
            set
            {
                if (_Tasks != value)
                {
                    _Tasks = value;
                }
            }
        }

        #endregion

    }
}
