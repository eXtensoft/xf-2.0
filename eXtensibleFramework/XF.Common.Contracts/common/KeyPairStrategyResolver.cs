// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    public sealed class KeyPairStrategyResolver : eXtensibleStrategyResolver, IStrategyResolver
    {
        string IStrategyResolver.Resolve(params string[] args)
        {
            string resolution = string.Empty;
            if (args.Length == 3)
            {
                resolution = LocalResolve(args[0], args[1], args[2]);
            }
            if (args.Length == 2)
            {
                resolution = LocalResolve(args[0], args[1], null);
            }
            else
            {
                resolution = LocalResolve(args[0], null, null);
            }
            return resolution;
        }

        private string LocalResolve(string primaryKey, string secondaryKey, string tertiaryKey)
        {
            string resolution = string.Empty;
            //List<eXtensibleStrategyElement> list = Strategies.GetForStrategyType(StrategyTypeOption.KeyPair);
            //if (!string.IsNullOrWhiteSpace(tertiaryKey))
            //{
            //    var found = list.Find(x => x.ParamDomain.Equals(secondaryKey, StringComparison.OrdinalIgnoreCase) & x.ParamKey.Equals(primaryKey, StringComparison.OrdinalIgnoreCase));
            //    if (found != null)
            //    {
            //        resolution = found.Resolution;
            //    }
            //}
            //if (!string.IsNullOrEmpty(secondaryKey))
            //{
            //    var found = list.Find(x => x.ParamDomain.Equals(secondaryKey, StringComparison.OrdinalIgnoreCase) & x.ParamKey.Equals(primaryKey, StringComparison.OrdinalIgnoreCase));
            //    if (found != null)
            //    {
            //        resolution = found.Resolution;
            //    }
            //}
            //else if (!string.IsNullOrEmpty(primaryKey))
            //{
            //    var found = list.Find(x => x.ParamKey.Equals(primaryKey, StringComparison.OrdinalIgnoreCase));
            //    if (found != null)
            //    {
            //        resolution = found.Resolution;
            //    }
            //}
            return resolution;
        }
    }
}
