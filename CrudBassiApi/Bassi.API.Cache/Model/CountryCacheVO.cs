using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Cache.Model
{
    public class CountryCacheVO
    {
        /// <summary>
        /// Id de GEM
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Country Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id de ACNET
        /// </summary>
        public short NCode { get; set; }

        /// <summary>
        /// Iso code de 2 digitos
        /// </summary>
        public string ISOCode { get; set; }

        /// <summary>
        /// Iso code de 3 digitos
        /// </summary>
        public string ACode { get; set; }
    }
}
