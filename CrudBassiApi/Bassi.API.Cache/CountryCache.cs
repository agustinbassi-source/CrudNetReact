using System;
using System.Collections;
using System.Collections.Generic;
using Bassi.API.Cache.Model;

namespace Bassi.API.Cache
{
    public class CountryCache
    {
        private static List<CountryCacheVO> _countryCaches;

        public CountryCache()
        {
            _countryCaches = new List<CountryCacheVO>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isoCode">Codigo iso de 2 digitos</param>
        /// <returns></returns>
        public static CountryCacheVO GetCountryByIsoCode(string isoCode)
        {
            CountryCacheVO cacheVO = _countryCaches.Find(f => f.ISOCode == isoCode);

            if(cacheVO == null)
            {
                //Busca el pais por el codigo iso 2 digitos
            }

            return cacheVO;
        }

    }
}
