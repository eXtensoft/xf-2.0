// <copyright company="eXtensoft, LLC" file="eXtensibleClaimsPrincipal.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.Common
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using XF.Common;

    public class eXtensibleClaimsPrincipal : ClaimsPrincipal
    {

        private dynamic _Item = new ExpandoObject();
        public dynamic Item
        {
            get { return _Item; }
        }

        public Stopwatch StopWatch { get; set; }

        #region Items (List<TypedItem>)

        private List<TypedItem> _Items = new List<TypedItem>();

        /// <summary>
        /// Gets or sets the List<TypedItem> value for Items
        /// </summary>
        /// <value> The List<TypedItem> value.</value>

        public List<TypedItem> Items
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

        public new eXtensibleIdentity Identity { get; private set; }


        //#region CallerItems (List<TypedItem>)

        //private List<TypedItem> _CallerItems = new List<TypedItem>();

        ///// <summary>
        ///// Gets or sets the List<TypedItem> value for CallerItems
        ///// </summary>
        ///// <value> The List<TypedItem> value.</value>

        //public List<TypedItem> CallerItems
        //{
        //    get { return _CallerItems; }
        //    set
        //    {
        //        if (_CallerItems != value)
        //        {
        //            _CallerItems = value;
        //        }
        //    }
        //}

        //#endregion

        public eXtensibleClaimsPrincipal(ClaimsIdentity identity)
        : base(identity)
        {
            Identity = new eXtensibleIdentity();
            StopWatch = new Stopwatch();
            StopWatch.Start();
            Start();
        }

        public void Start()
        {
            Items.Add(new TypedItem("xf-request.start", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt")));
        }

        public void Stop()
        {
            Items.Add(new TypedItem("xf-request.end", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt")));
            StopWatch.Stop();
            TimeSpan elapsed = StopWatch.Elapsed;
            Items.Add(new TypedItem("xf-request.milliseconds-elapsed", elapsed.TotalMilliseconds));
        }


        public void RemoveClaim(string key)
        {
            var existingClaims = FindAll(key);

        }

        public void AddItem(string key, object item)
        {
            Items.Add(new TypedItem(key, item));

        }

        public static void Add(string key, object item)
        {
            var cp = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal;
            if (!String.IsNullOrWhiteSpace(key) && item != null && cp != null)
            {
                cp.AddItem(key, item);
            }
        }

        public static void Accept(IeXtensibleVisitor visitor)
        {
            var cp = Thread.CurrentPrincipal as eXtensibleClaimsPrincipal;
            if (cp != null)
            {
                cp.Items.Accept(visitor);
            }
        }


        public bool TryGetClaim<T>(string key, out T t) where T : IConvertible
        {
            bool b = false;
            t = default(T);
            string typeName = t.GetType().Name;
            string claimAsText;
            if (TryGetClaim(key, out claimAsText) && Parser.Parse<T>(claimAsText, out t))
            {
                b = true;
            }
            return b;
        }


        private bool TryGetClaim(string key, out string claim)
        {
            bool b = false;
            claim = String.Empty;
            var found = Claims.FirstOrDefault(x => x.Type.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (found != null && !String.IsNullOrEmpty(found.Value))
            {
                claim = found.Value;
                b = true;
            }
            return b;
        }


    }

}
