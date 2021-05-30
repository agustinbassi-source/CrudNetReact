using Bassi.API.Authorize.DTO;
using Bassi.API.Authorize.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bassi.API.Authorize
{
    public class CustomAuthenticate : ICustomAuthenticate
    {
        //private readonly string _applicationModule;

        public CustomAuthenticate(string urlServiceAutenticate, string applicationModule)
        {
            GetUrlService = urlServiceAutenticate;
            ApplicationModule = applicationModule;
        }

        public AuthenticateServiceResponseDTO Authenticate(string userName, string password)
        {
            //llama al servicio de autenticacion
            WebRequest httpRequest = WebRequest.Create($"{GetUrlService}/Authenticate");

            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("userName", userName);
            data.Add("password", password);
            data.Add("module", ApplicationModule);
            string autenticateData = System.Text.Json.JsonSerializer.Serialize(data);
            byte[] credentials = Encoding.UTF8.GetBytes(autenticateData);

            httpRequest.ContentLength = credentials.Length;

            using (Stream request = httpRequest.GetRequestStream())
            {
                request.Write(credentials, 0, credentials.Length);
                request.Flush();
            }

            WebResponse webResponse = httpRequest.GetResponse();
            string text;
            using (StreamReader response = new StreamReader(webResponse.GetResponseStream()))
            {
                text = response.ReadToEnd();
            }

            AuthenticateServiceResponseDTO authenticate = System.Text.Json.JsonSerializer.Deserialize<AuthenticateServiceResponseDTO>(text, new System.Text.Json.JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return authenticate;
        }

        public AuthorizationServiceResponseDTO ValidateToken(string authorizationToken)
        {
 
            WebRequest httpRequest = WebRequest.Create($"{GetUrlService}/Validate/{ApplicationModule}");

            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";

            httpRequest.Headers.Add("Authorization", $"bearer {authorizationToken}");

            var response = httpRequest.GetResponse();
            string readResponse;

            using StreamReader reader = new StreamReader(response.GetResponseStream());
            readResponse = reader.ReadToEnd();

            AuthorizationServiceResponseDTO authorize = System.Text.Json.JsonSerializer.Deserialize<AuthorizationServiceResponseDTO>(readResponse, new System.Text.Json.JsonSerializerOptions() { 
                PropertyNameCaseInsensitive = true
            });

            return authorize;
            //throw new NotImplementedException();
        }

        public string GetUrlService { get; }
        public string ApplicationModule { get; }
    }
}
