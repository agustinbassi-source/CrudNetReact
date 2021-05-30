using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Utilities.Configuration
{
    public static class DBConfiguration 
    {
        public static IConfigurationBuilder AddDBConfiguration(this IConfigurationBuilder builder) 
        {
            return builder.Add(new DBConfigurationSource());
        }
    }
    public class DBConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DBConfigurationProvider();
        }
    }
}
