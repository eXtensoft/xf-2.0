// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    public class DataPacket
    {
        [DataMember]
        public string Typename { get; set; }
        [DataMember]
        public ApplicationContext Context { get; set; }
        [DataMember]
        public List<TypedItem> Items { get; set; }
        [DataMember]
        public ModelActionOption ModelAction { get; set; }
        [DataMember]
        public Criterion Criteria { get; set; }
        [DataMember]
        public DataSet Tables { get; set; }
        [DataMember]
        public byte[] Buffer { get; set; }
        [DataMember]
        public string SecondaryTypename { get; set; }
        [IgnoreDataMember]
        public string ErrorMessage { get; set; }

        [IgnoreDataMember]
        public bool IsOkay
        {
            get { return String.IsNullOrEmpty(ErrorMessage); }
        }

        [IgnoreDataMember]
        private Type _ModelType = null;
        public Type ModelType
        {
            get 
            {
                if (_ModelType == null)
                {
                    _ModelType = Type.GetType(Typename);
                    if (_ModelType == null)
                    {
                        EventWriter.WriteError(String.Format("Unable to resolve type: '{0}'", Typename), SeverityType.Error);
                    }
                }
                return _ModelType;
            }

        }

        public void Assimilate(object o)
        {

        }


        public void SetError(int code, string message)
        {
            ErrorMessage = message;
            Items.Add(new TypedItem() { Key = XFConstants.Message.RequestStatus, Value = code, Text = message, Tds = DateTime.Now });
        }
    }
}
