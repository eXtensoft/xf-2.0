using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Common;
using XF.Quality;

namespace XF.WebApi.Quality
{
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
