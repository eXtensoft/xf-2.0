// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
   
    public class ExceptionMessage 
    {
        private string _Id = Guid.NewGuid().ToString().ToLower();
        public string Id
        {
            get { return _Id; }
        }

        private static IList<string> _zones = new List<string>()
        {
            XFConstants.ZONE.Staging,
            XFConstants.ZONE.Production,
        };

        private Dictionary<ExceptionMessageOption, string> _Messages = new Dictionary<ExceptionMessageOption, string>();

        public ExceptionMessage() { }
        public ExceptionMessage(ExceptionMessageOption option, string message)
        {
            _Messages.Add(option, message);
        }

        public void Add(ExceptionMessageOption option, string message)
        {
            if (!_Messages.ContainsKey(option))
            {
                _Messages.Add(option, message);
            }
            else
            {
                string s = _Messages[option] + ":" + message;
                _Messages[option] = s;
            }
        }

        public string ToPublish()
        {
            if (!_zones.Contains(eXtensibleConfig.Zone,StringComparer.OrdinalIgnoreCase) 
                && eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose))
            {
                return _Messages[ExceptionMessageOption.Log];
            }
            else
            {
                return (_Messages.ContainsKey(ExceptionMessageOption.Publish)) ? _Messages[ExceptionMessageOption.Publish] : String.Empty;
            }
        }

        public string ToLog()
        {
            StringBuilder sb = new StringBuilder();
            if (_Messages.ContainsKey(ExceptionMessageOption.Log))
            {
                sb.AppendLine(_Messages[ExceptionMessageOption.Log]);
            }
            else if(_Messages.ContainsKey(ExceptionMessageOption.Publish))
            {
                sb.AppendLine(_Messages[ExceptionMessageOption.Publish]);
            }
            if (_Messages.ContainsKey(ExceptionMessageOption.Stacktrace))
            {
                sb.AppendLine(_Messages[ExceptionMessageOption.Stacktrace]);
            }
            return sb.ToString();
        }


    }
}
