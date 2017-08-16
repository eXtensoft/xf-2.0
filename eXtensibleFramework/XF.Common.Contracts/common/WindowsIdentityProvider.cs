// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    public class WindowsIdentityProvider : IUserIdentityProvider
    {
        string IUserIdentityProvider.Username
        {
            get { return System.Security.Principal.WindowsIdentity.GetCurrent().Name; }
        }

        string IUserIdentityProvider.Culture
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.EnglishName; }
        }

        string IUserIdentityProvider.UICulture
        {
            get { return System.Globalization.CultureInfo.CurrentUICulture.EnglishName; }
        }
    }
}
