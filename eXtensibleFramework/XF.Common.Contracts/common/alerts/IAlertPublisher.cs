

namespace XF.Common
{
    using XF.Common;

    public interface IAlertPublisher : ITypeMap
    {
        string FromAddress { get; set; }

        bool Initialize();

        void Execute(eXAlert alert, AlertInterest interest);

        void Cleanup();

    }

}
