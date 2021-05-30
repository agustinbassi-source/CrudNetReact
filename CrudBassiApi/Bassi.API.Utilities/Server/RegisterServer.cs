using Bassi.ADO.Utilities;
using Bassi.Gem.Utilities.VOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Bassi.API.Utilities.Server
{
    public class RegisterServer
    {
        public static void Register(int applicationId, string connectionStringUtility)
        {
            var binding = GetBinding();
           

            InsertRegistroServidorParametersVO parameter = new InsertRegistroServidorParametersVO()
            {
                Activo = true,
                AplicacionId = applicationId,
                Dns = binding.Dns,
                Ip = binding.Ip,
                Puerto = binding.Port,
                SSLActivo = binding.Protocol == "https"
            };


            InsertRegistroServidorVO(parameter, connectionStringUtility);


        }

        private static string GetIp()
        {
            string localIP = string.Empty;

            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }

            return localIP;

        }

        private static BindingDTO GetBinding()
        {
            var bindings = GetBindings();

            BindingDTO res = new BindingDTO();

            foreach (var binding in bindings)
            {
                if (binding.Port != 443 && binding.Port != 80)
                {
                    res.Port = binding.Port;
                    res.Protocol = binding.Protocol;
                }

                if (binding.Dns.IndexOf("card.com") > 0)
                {
                    res.Dns = binding.Dns;
                }

            }

            res.Ip = GetIp();
 
            return res;

        }

       

        private static List<BindingDTO> GetBindings()
        {
            List<BindingDTO> res = new List<BindingDTO>();

            //HttpContext context
            // Get the Site name
            // string siteName = "MailSender";

            // Get the sites section from the AppPool.config
            Microsoft.Web.Administration.ConfigurationSection sitesSection =
                Microsoft.Web.Administration.WebConfigurationManager.GetSection(null, null, "system.applicationHost/sites");

            foreach (Microsoft.Web.Administration.ConfigurationElement site in sitesSection.GetCollection())
            {
                // Find the right Site
                //if (String.Equals((string)site["name"], siteName, StringComparison.OrdinalIgnoreCase))
                //{

                // For each binding see if they are http based and return the port and protocol
                foreach (Microsoft.Web.Administration.ConfigurationElement binding in site.GetCollection("bindings"))
                {
                    string protocol = (string)binding["protocol"];
                    string bindingInfo = (string)binding["bindingInformation"];

                    if (protocol.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] parts = bindingInfo.Split(':');
                        if (parts.Length == 3)
                        {

                            int port = 0;

                            int.TryParse(parts[1], out port);

                            // string port = parts[1];
                            // port = bindingInfo;
                            //   yield return new KeyValuePair<string, string>(protocol, port);

                            res.Add(new BindingDTO
                            {
                                Dns = parts[2],
                                Port = port,
                                Protocol = protocol
                            });

                        }
                    }
                }
                //   }
            }

            return res;
        }

        #region Functions
      
        public static void InsertRegistroServidorVO(InsertRegistroServidorParametersVO parameters, string connectionStringUtility)
        {

            SqlParameters sqlParameters = new SqlParameters(6);

            sqlParameters.Add("@AplicacionId", SqlDbType.Int, DbType.Int32, parameters.AplicacionId);
            sqlParameters.Add("@Ip", SqlDbType.VarChar, DbType.String, parameters.Ip);
            sqlParameters.Add("@Puerto", SqlDbType.Int, DbType.Int32, parameters.Puerto);
            sqlParameters.Add("@Dns", SqlDbType.VarChar, DbType.String, parameters.Dns);
            sqlParameters.Add("@Activo", SqlDbType.Bit, DbType.Boolean, parameters.Activo);
            sqlParameters.Add("@SSLActivo", SqlDbType.Bit, DbType.Boolean, parameters.SSLActivo);

            int records;

            SqlStoreExcuter.ExecuteNonQuery(connectionStringUtility, "[dbo].[procRegistroServidorInsert]", sqlParameters, (result) =>
            {
                records = result;
            });

        }

        #endregion


    }

    public class BindingDTO
    {
        public int Port { get; set; }
        public string Protocol { get; set; }
        public string Dns { get; set; }
        public string Ip { get; set; }

    }
}
