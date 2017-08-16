// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
