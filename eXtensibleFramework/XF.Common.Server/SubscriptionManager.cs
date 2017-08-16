// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Alert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using XF.Common;

    public class SubscriptionManager
    {
        #region local members

        private static ITypeMapCache _TypeCache;

        private Dictionary<CommunicationTypeOption, IAlertPublisher> publishers = new Dictionary<CommunicationTypeOption, IAlertPublisher>();

        private List<AlertSubscriber> subscribers;


        #endregion

        public IModelRequestService Service {get;set;}

        public List<eXAlert> Alerts { get; set; }


        private bool isInitialized = false;
        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        static SubscriptionManager()
        {
         
            _TypeCache = new TypeMapCache();
            _TypeCache.Initialize();
        }

        public SubscriptionManager(List<eXAlert> alerts)
        {
            Alerts = alerts;
        }
        public SubscriptionManager() { }

        private IAlertPublisher ResolveHandler(CommunicationTypeOption key)
        {
            IAlertPublisher implementor = null;
            Type candidateType = _TypeCache.ResolveType(key.ToString());
            if (candidateType != null)
            {
                if (!publishers.ContainsKey(key))
                {
                    var publisher = Activator.CreateInstance(candidateType) as IAlertPublisher;
                    if (publisher.Initialize())
                    {
                        publishers.Add(key,publisher);                        
                    }                                  
                }   
                implementor = publishers[key];                  
            }
            return implementor;
        }

        public bool Initialize(IModelRequestService service)
        {

            var response = service.GetAll<AlertSubscriber>(null);
            if (response.IsOkay)
            {
                subscribers = response.ToList();
                if (subscribers.Count > 0)
                {
                    isInitialized = true;
                }
            }
            return isInitialized;
        }

        public void Execute()
        {

            if (IsInitialized)
            {
                if (FetchAlertsForProcessing())
                {
                    foreach (var alert in Alerts)
                    {
                        Execute(alert);
                    }                  
                }                
            }
        }

        public bool Execute(eXAlert model)
        {
            bool b = false;
            if (model != null)
            {
                b = ExecuteAlert(model);
            }
            return b;
        }

        private bool ExecuteAlert(eXAlert alert)
        {
            int alertCount = 0;

            List<string> list = null;
            foreach (var subscriber in subscribers)
            {

                foreach (var interest in subscriber.Interests)
                {
                    bool b = true;
                    for (int i = 0;b && i < interest.Targets.Count; i++)
                    {
                        AlertTarget target = interest.Targets[i];
                        b = Contains(alert,target);
                    }
                    if (b)
                    {
                        alertCount++;
                        Publish(alert, interest);
                    }
                }
            }
            if (alert.Dispositions != null)
            {
                list = new List<string>(alert.Dispositions);
            }
            else
            {
                list = new List<string>();
            }
            string disposition = String.Format("{0} subscribers, {1} alerts :{2}", subscribers.Count,alertCount, DateTime.Now.ToString(XFConstants.DateTimeFormat));
            list.Add(disposition);
            alert.Dispositions = list.ToArray();
            return true;
        }

        private void Publish(eXAlert alert, AlertInterest interest)
        {
            var implementor = ResolveHandler( interest.Notification.Method);
            if (implementor != null)
            {
                implementor.Execute(alert, interest);
            }
        }

        private static IDictionary<string, Func<eXAlert, AlertTarget, bool>> targetmaps = new Dictionary<string, Func<eXAlert, AlertTarget, bool>>(StringComparer.OrdinalIgnoreCase)
        {
            {"event.alert.importance",Importance},
            {"event.alert.urgency",Urgency},
            {"event.alert.target",Target},
            {"event.alert.source",Source},
            {"event.alert.category",Category},
            {"event.alert.zone",Zone},
        };

        private bool Contains(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            if (targetmaps.ContainsKey(target.Key))
            {
                b = targetmaps[target.Key].Invoke(alert, target);
            }
            return b;
        }

        private static bool Importance(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            ScaleOption alertOpt;
            ScaleOption targetOpt;
            if (!String.IsNullOrEmpty(alert.Importance) && Enum.TryParse<ScaleOption>(alert.Importance, out alertOpt))
	        {
                if (Enum.TryParse<ScaleOption>(target.Value, out targetOpt))
                {
                    b = alertOpt >= targetOpt;
                }
	        }
            return b;
        }
        private static bool Urgency(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            ScaleOption alertOpt;
            ScaleOption targetOpt;
            if (!String.IsNullOrEmpty(alert.Urgency) && Enum.TryParse<ScaleOption>(alert.Urgency, out alertOpt))
            {
                if (Enum.TryParse<ScaleOption>(target.Value, out targetOpt))
                {
                    b = alertOpt >= targetOpt;
                }
            }
            return b;
        }
        private static bool Target(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            if (alert.Audiences != null && alert.Audiences.Contains(target.Value))
            {
                b = true;
            }
            return b;
        }
        private static bool Source(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            if (!String.IsNullOrEmpty(alert.Source) && alert.Source.Equals(target.Value))
            {
                b = true;
            }
            return b;
        }
        private static bool Category(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            if (alert.Categories != null && alert.Categories.Contains(target.Value))
            {
                b = true;
            }
            return false;
        }
        private static bool Zone(eXAlert alert, AlertTarget target)
        {
            bool b = false;
            if (!String.IsNullOrEmpty(alert.Zone) && alert.Zone.Equals(target.Value))
            {
                b = true;
            }
            return b;
        }


        private bool FetchAlertsForProcessing()
        {
            bool b = false;
            var c = Criterion.GenerateStrategy("for.processing");
            c.AddItem("CurrentStatus", "none");
            var alertResponse = Service.GetAll<eXAlert>(c);
            if (alertResponse.IsOkay)
            {
                foreach (var item in alertResponse)
                {
                    Alerts.Add(item);
                }
                b = true;
            }
            return b;
        }

    }
}
