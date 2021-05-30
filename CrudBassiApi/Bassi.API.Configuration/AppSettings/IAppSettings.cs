using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Configuration
{
    public interface IAppSettings
    {
        string Getkey(string keyName);
        string GetConnectionString(string connectionName);
        IConfigurationSection GetSection(string sectionName);
        string GetSection(string sectionName, string kayName);
    }
}
