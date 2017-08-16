// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
    using System.Net;

    public class MessageProvider : HttpMessageProvider
    {
        public override HttpStatusCode DefaultErrorCode
        {
            get
            {
                return base.DefaultErrorCode;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Get(string identifier, out System.Net.HttpStatusCode code)
        {
            base.Get(identifier, out code);
        }

        public override void Get(string identifier, out System.Net.HttpStatusCode code, out string message)
        {
            base.Get(identifier, out code, out message);
        }

        public override void Get(string identifier, out System.Net.HttpStatusCode code, out string message, object[] messageParameters)
        {
            base.Get(identifier, out code, out message, messageParameters);
        }


        public override void GetError<T>(System.Net.HttpStatusCode statusCode, T t, out string message)
        {
            base.GetError<T>(statusCode, t, out message);
        }

        public override void GetError(string errorIdentifier, object[] messageParameters, out System.Net.HttpStatusCode errorCode, out string message)
        {
            base.GetError(errorIdentifier, messageParameters, out errorCode, out message);
        }

        public override void GetError(string identifier, out System.Net.HttpStatusCode errorCode, out string message)
        {
            base.GetError(identifier, out errorCode, out message);
        }

        public override bool VetStatusCode(System.Net.HttpStatusCode candidateStatusCode)
        {
            return base.VetStatusCode(candidateStatusCode);
        }
    }
}
