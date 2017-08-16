// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



namespace XF.WebApi.Quality
{
    using XF.Common;
    using XF.Quality;

    public class ApiProcessItem : ProcessItem
    {

        #region Properties (PropertyCollection)

        private PropertyCollection _Properties = new PropertyCollection();

        /// <summary>
        /// Gets or sets the PropertyCollection value for Properties
        /// </summary>
        /// <value> The PropertyCollection value.</value>

        public PropertyCollection Properties
        {
            get { return _Properties; }
            set
            {
                if (_Properties != value)
                {
                    _Properties = value;
                }
            }
        }

        #endregion




    }
}
