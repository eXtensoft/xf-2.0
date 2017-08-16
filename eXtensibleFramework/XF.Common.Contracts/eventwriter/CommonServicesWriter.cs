// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text;

    public class CommonServicesWriter : EventWriterBase
    {
        private const string ApiUrlFormat = "{0}/{1}/";

        protected override void Publish(EventTypeOption eventType, List<TypedItem> properties)
        {
            string url = eXtensibleConfig.BigDataUrl;
            if (!String.IsNullOrWhiteSpace(url))
            {
                Run(eventType, properties, url);
            }           
            //RunAsync(eventType, properties,url).Wait();
        }



        protected override void Publish(eXMetric metric)
        {
            string url = eXtensibleConfig.BigDataUrl;
            Run(metric, url);
            //RunAsync(metric,url).Wait();
            //new Action<eXMetric, string>(Run).BeginInvoke(metric, url, null, null);
        }

        private void Run(EventTypeOption eventType, List<TypedItem> properties, string baseUrl)
        {
            try
            {
                string endpoint = ResolveEndpoint(eventType);
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Add("Authorization",String.Format("{0} {1}","basic",eXtensibleConfig.ServiceToken));
                    var response = client.PostAsJsonAsync(endpoint, properties);
                    if (response.Result.IsSuccessStatusCode)
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        if (ex.InnerException.InnerException.InnerException != null)
                        {
                            sb.Append(ex.InnerException.InnerException.InnerException.Message);
                        }
                        else
                        {
                            sb.Append(ex.InnerException.InnerException.Message);   
                        }
                    }
                    else
                    {
                        sb.Append(ex.InnerException.Message);  
                    }
                    sb.Append(ex.Message);
                }
                try
                {

                    EventLog.WriteEntry(XFConstants.Config.DefaultLogSource, sb.ToString(), EventLogEntryType.Error, 1);
                    Console.WriteLine(sb.ToString());                
                }
                catch
                {

                }
            }           
        }

        private void Run(eXMetric metric, string baseUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("{0} {1}", "basic", eXtensibleConfig.ServiceToken));
                    var response = client.PostAsJsonAsync<eXMetric>(String.Format(ApiUrlFormat,eXtensibleConfig.ApiRoot,eXtensibleConfig.ApiMetrics), metric);
                    if (response.Result.IsSuccessStatusCode)
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch 
            {
                

            }

        }

        private static string ResolveEndpoint(EventTypeOption option)
        {
            string endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiEvents);
            switch (option)
            {
                case EventTypeOption.Error:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiErrors);    //"/log/errors"
                    break;
                case EventTypeOption.Event:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiEvents); //"/log/events";
                    break;
                case EventTypeOption.Status:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiStatii); // "/log/statii";
                    break;
                case EventTypeOption.Task:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiTasks); // "/log/tasks";
                    break;
                case EventTypeOption.Alert:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiAlerts); // "/log/alerts";
                    break;
                case EventTypeOption.Kpi:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiKpi); // "/log/kpi";
                    break;
                case EventTypeOption.Custom:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiCustom); // "/log/custom";
                    break;
                case EventTypeOption.None:
                default:
                    endpoint = String.Format(ApiUrlFormat, eXtensibleConfig.ApiRoot, eXtensibleConfig.ApiEvents); // "/log/events";
                    break;
            }
            return endpoint;
        }

    }

}
