


namespace XF.Common
{
    using System;
    using System.ComponentModel.Composition;
    using XF.Common;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class AlertPublisher : IAlertPublisher
    {

        bool IAlertPublisher.Initialize()
        {
            return Initialize();
        }

        void IAlertPublisher.Execute(eXAlert alert, AlertInterest interest)
        {
            Execute(alert, interest);
        }

        void IAlertPublisher.Cleanup()
        {
            Cleanup();
        }

        string Common.ITypeMap.Domain
        {
            get { return NotificationMethod.ToString(); }
        }

        Type Common.ITypeMap.KeyType
        {
            get { return typeof(String); }
        }

        Type Common.ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }


        string IAlertPublisher.FromAddress
        {
            get
            {
                return FromAddress;
            }
            set
            {
                FromAddress = value;
            }
        }



        protected abstract Common.CommunicationTypeOption NotificationMethod { get; }

        protected virtual void Cleanup()
        {

        }

        protected virtual bool Initialize()
        {
            return true;
        }

        protected abstract void Execute(eXAlert alert, AlertInterest interest);


        private string _FromAddress = String.Empty;

        public virtual string FromAddress
        {
            get 
            {
                if (String.IsNullOrWhiteSpace(_FromAddress))
                {
                    _FromAddress = ConfigurationProvider.AppSettings["alert.from.address"];
                }
                return _FromAddress;            
            }
            set { _FromAddress = value; }
        }

    }
}
