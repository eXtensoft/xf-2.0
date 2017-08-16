// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace RB.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Xml.Serialization;

    public static class RouteHelper
	{
		public static List<ControllerRegistration> Execute(HttpConfiguration config)
		{
			int controllerOrder = 0;
			int endpointOrder = 1;
			ControllerRegistrations c = new ControllerRegistrations();

			var provider = config.Services.GetDocumentationProvider();
			IEnumerable<ApiDescription> descriptions = config.Services.GetApiExplorer().ApiDescriptions;
			foreach (var desc in descriptions)
			{
				if (!String.IsNullOrWhiteSpace(desc.RelativePath))
				{
					string controllerName = desc.ActionDescriptor.ControllerDescriptor.ControllerName;
					string controllerMethod = desc.ActionDescriptor.ActionName;
					string pattern = desc.RelativePath;
					string httpMethod = desc.HttpMethod.ToString();
					if (!controllerMethod.Equals("Register", StringComparison.OrdinalIgnoreCase))
					{
						if (!c.Contains(controllerName))
						{
							controllerOrder++;
							endpointOrder = 1;
							c.Add(new ControllerRegistration() { Name = controllerName, Methods = new List<string>(), Endpoints = new List<Endpoint>(), Order = controllerOrder });
						}

						c[controllerName].Methods.Add(controllerMethod);

						Endpoint endpoint = new Endpoint()
						{
							Order = endpointOrder++,
                            HttpMethod = httpMethod,
                            Controller = controllerName,
                            ControllerMethod = controllerMethod,
                            Pattern = pattern,
						};
						if (desc.ParameterDescriptions != null && desc.ParameterDescriptions.Count > 0)
						{
							endpoint.Parameters = new List<EndpointParameter>();
							foreach (ApiParameterDescription parameterDesc in desc.ParameterDescriptions)
							{
								EndpointParameter p = new EndpointParameter()
								{
									Name = parameterDesc.Name,
                                    Source = parameterDesc.Source.ToString()
								};
								if (parameterDesc.ParameterDescriptor != null && parameterDesc.ParameterDescriptor.DefaultValue != null)
								{
									p.DefaultValue = parameterDesc.ParameterDescriptor.DefaultValue.ToString();
								}

								if (parameterDesc.ParameterDescriptor != null && parameterDesc.ParameterDescriptor.ParameterType != null)
								{
									p.Type = parameterDesc.ParameterDescriptor.ParameterType.Name;
								}

								endpoint.Parameters.Add(p);
							}
						}

						c[controllerName].Endpoints.Add(endpoint);
					}
				}
			}

			return c.ToList();
		}
	}

    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [Serializable]
	public class Endpoint
	{
		[DataMember]
		[XmlAttribute("order")]
		public int Order { get; set; }

		[DataMember]
		[XmlAttribute("httpMethod")]
		public string HttpMethod { get; set; }

		[DataMember]
		public string Pattern { get; set; }

		[DataMember]
		[XmlAttribute("controller")]
		public string Controller { get; set; }

		[DataMember]
		[XmlAttribute("controllerMethod")]
		public string ControllerMethod { get; set; }

		[DataMember]
		[XmlAttribute("registeredAs")]
		public string RegisterName { get; set; }

		[DataMember]
		[XmlElement]
		public List<EndpointParameter> Parameters { get; set; }
	}

    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [Serializable]
	public class EndpointParameter
	{
		[DataMember]
		[XmlAttribute("name")]
		public string Name { get; set; }

		[DataMember]
		[XmlAttribute("type")]
		public string Type { get; set; }

		[DataMember]
		[XmlAttribute("source")]
		public string Source { get; set; }

		[DataMember]
		[XmlAttribute("defaultValue")]
		public string DefaultValue { get; set; }
	}

    [DataContract(Namespace = "http://eXtensoft/xf/schemas/2017/09")]
    [Serializable]
	public class ControllerRegistration
	{
		[DataMember]
		[XmlAttribute("order")]
		public int Order { get; set; }

		[DataMember]
		[XmlAttribute("name")]
		public string Name { get; set; }
		[DataMember]

		public List<string> Methods { get; set; }

		[DataMember]
		public List<Endpoint> Endpoints { get; set; }
	}

	public class ControllerRegistrations : KeyedCollection<string, ControllerRegistration>
	{
		protected override string GetKeyForItem(ControllerRegistration item)
		{
			return item.Name;
		}
	}
}