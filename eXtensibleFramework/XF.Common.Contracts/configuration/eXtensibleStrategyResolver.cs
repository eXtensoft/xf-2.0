// <copyright company="eXtensible Solutions, LLC" file="eXtensibleStrategyResolver.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Concurrent;
    using System.Text;

    public class eXtensibleStrategyResolver : IConfigStrategyResolver
    {
        private eXtensibleStrategySectionGroup group;
        protected bool isInitialized;

        private ConcurrentDictionary<string, string> maps = new ConcurrentDictionary<string, string>();

        void IConfigStrategyResolver.Initialize(eXtensibleStrategySectionGroup sectionGroup)
        {
            Initialize(sectionGroup);
        }

        string IConfigStrategyResolver.Resolve<T>(IContext context)
        {
            return (isInitialized) ? Resolve<T>(context) : String.Empty;
        }

        bool IConfigStrategyResolver.IsInitialized
        {
            get { return isInitialized; }
        }

        public virtual void Initialize(eXtensibleStrategySectionGroup sectionGroup)
        {
            if (sectionGroup != null)
            {
                group = sectionGroup;
                isInitialized = true;
            }                        
        }

        public virtual string Resolve<T>(IContext context)
        {
                       
            string key = string.Empty;
            string modelName = GetModelTypename<T>();
            string applicationContext = context.ApplicationContextKey;
            string namespaceString;
            

            if (!String.IsNullOrWhiteSpace(applicationContext) && !String.IsNullOrWhiteSpace(modelName))
            {
                string mapkey = String.Format("{0}:{1}",applicationContext,modelName);

                if (!maps.ContainsKey(mapkey))
                {
                    bool b = false; 
                    for (int i = 0;!b && i < group.Sections.Count; i++)
                    {
                        eXtensibleStrategySection section = group.Sections[i] as eXtensibleStrategySection;
                        if (section.Context.Equals(applicationContext, System.StringComparison.OrdinalIgnoreCase))
                        {
                            key = section.DefaultResolution;

                            for (int j = 0;!b && j < section.Strategies.Count; j++)
                            {
                                eXtensibleStrategyElement element = section.Strategies[j] as eXtensibleStrategyElement;
                                if (element.StrategyValue.Equals(modelName, StringComparison.OrdinalIgnoreCase))
                                {
                                    key = element.Resolution;
                                    b = true;
                                }
                            }
                            if (!b && ParseNamespace(modelName, out namespaceString))
                            {
                                for (int j = 0;!b && j < section.Strategies.Count; j++)
                                {
                                    eXtensibleStrategyElement element = section.Strategies[j] as eXtensibleStrategyElement;
                                    if (element.StrategyValue.Equals(namespaceString, StringComparison.OrdinalIgnoreCase))
                                    {
                                        key = element.Resolution;
                                        b = true;
                                    }
                                }                                
                            }
                            b = true;
                        }
                    }
                    maps.TryAdd(mapkey, key);
                }
                else
                {
                    maps.TryGetValue(mapkey, out key);
                }
            }
            return key;
        }

        private static bool ParseNamespace(string modelName, out string namespaceText)
        {
            bool b = true;
            StringBuilder sb = new StringBuilder();
            string[] tokens = modelName.Split(new char[] { '.' });
            int max = tokens.Length - 1;
            if (max > 0)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(".");
                    }
                    sb.Append(tokens[i]);
                }                
            }
            else
            {
                b = false;
            }
            namespaceText = sb.ToString();
            return b;
        }

        private string GetModelTypename<T>()
        {
            return Activator.CreateInstance<T>().GetType().FullName;
        }

    }
}


        //protected eXtensibleStrategySection
        //protected eXtensibleStrategyElementCollection Strategies
        //{
        //    get { return _Section.Strategies; }
        //}

        //private eXtensibleStrategySection _Section;

        //public virtual void Initialize(eXtensibleStrategySection section)
        //{
        //    _Section = section;
        //}