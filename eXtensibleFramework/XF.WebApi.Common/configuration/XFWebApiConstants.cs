// <copyright company="Recorded Books, Inc" file="XFWebApiConstants.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi
{
    using XF.Common;
    using System;
    using System.Collections.Generic;

    public static class XFWebApiConstants
    {
        public const string TimeFormat = "yyyy/MM/dd hh:mm:ss.fff tt";

        public static class Config
        {
            public const string SectionName = "eXtensoft.webApi";
            public const string IsLogToDatastoreKey = "is.logtosql";
            public const string LogToKey = "apirequest.logto";
            public const string SqlConnectionKey = "logtosql.connectionkey";
            public const string LoggingModeKey = "apirequest.loggingmode";
            public const string MessageProviderFolder = "message-provider.folder";

        }

        public static class Default
        {
            public const bool IsLogToDatastore = false;
            public const string DatastoreConnectionKey = "api.request.datastore";
            public const LoggingStrategyOption LogTo = LoggingStrategyOption.None;
            public const LoggingModeOption LoggingMode = LoggingModeOption.All;
            public const string MessageProviderFolder = "messageprovider";
            public const string CatchAllControllerId = "CCCBFDC2-783C-49E6-B938-61F8ABDBB3C3";
            public const string MessageIdHeaderKey = "x-message-id";
            public const bool IsEditRegistration = false;
        }
    }

}
