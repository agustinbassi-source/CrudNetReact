using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Utilities.Configuration
{
    public class DBConfigurationProvider : ConfigurationProvider
    {
        public DBConfigurationProvider()
        {
        }

        public override void Load()
        {
            Data.Add("TEST1" , "TESTA");
            Data.Add("TEST2" , "TESTB");
        }

        //public void Set(string key, string value)
        //{
        //    _dBConfiguration[key] = value;
        //}

        //public bool TryGet(string key, out string value)
        //{
        //    value = string.Empty;
            
        //    if (_dBConfiguration.ContainsKey(key)) 
        //    {
        //        value = _dBConfiguration[key];
        //        return true;
        //    }

        //    return false;

        //}
    }
}
