// <copyright company="eXtensible Solutions, LLC" file="Messages.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public static class Messages
    {
        //public static string ComposeResourceNotFoundError<T>(ModelActionOption modelActionOption, T t, ICriterion criterion, IContext context) where T : class, new()
        //{
        //    string message = String.Empty;

        //        StringBuilder sb = new StringBuilder();
        //        if (t != null)
        //        {
        //            sb.AppendLine(String.Format(Messages.ModelFormat, t.ToString()));
        //        }
        //        if (criterion != null)
        //        {
        //            sb.AppendLine(String.Format(Messages.ICriterionFormat, criterion.ToString()));
        //        }
        //        sb.AppendLine(String.Format(Messages.IContextFormat, context.ToString()));
        //        message = String.Format(Messages.ResourceNotFoundFormatVerbose, GetModelType<T>().FullName, modelActionOption, sb.ToString());

        //    return message;
        //}

        private static Type GetModelType<T>() where T : class, new()
        {
            return Activator.CreateInstance<T>().GetType();
        }

    }
}
