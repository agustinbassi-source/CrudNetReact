using System;
using System.Collections.Generic;
using System.Text;
using Bassi.Portal.Service.DTO;
using System.Xml;
using System.Net;
using System.IO;

namespace Bassi.Portal.Service
{
    public partial class PortalService
    {
        public UserProfiles UserProfiles(Guid UserId, string Module, CredentialDTO credential)
        {
            UserProfiles userProfiles = new UserProfiles();
            userProfiles.Profiles = new List<UserModuleProfileDTO>();

            Dictionary<string, object> userParameters = new Dictionary<string, object>();
            userParameters.Add("GUID", UserId);
            userParameters.Add("codModulo", Module);
            userParameters.Add("CodigoUsuario", credential.User);
            userParameters.Add("PasswordUsuario", credential.Password);

            try
            {
                XmlDocument bodyRequest = SoapMethodBody(CreateSoapEnvelope(), "GetUsuarioGUID", userParameters);
                HttpWebRequest request = PortalRequest("POST", new List<string>(new string[] { @"SOAPAction: ""http://tempuri.org/GetUsuarioGUID""" }));

                using(Stream body = request.GetRequestStream())
                {
                    bodyRequest.Save(body);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string data = "";
                using (StreamReader responseService = new StreamReader(response.GetResponseStream()))
                {
                    data = responseService.ReadToEnd();
                }

                XmlDocument xmlResponse = new XmlDocument();
                xmlResponse.LoadXml(data);

                userProfiles = Parser(xmlResponse);
                
            }
            catch { throw; }

            return userProfiles;
        }

        private UserProfiles Parser(XmlDocument userData)
        {
            UserProfiles userProfiles = new UserProfiles();
            userProfiles.Profiles = new List<UserModuleProfileDTO>();

            XmlElement root = userData.DocumentElement;

            XmlNodeList valid = root.GetElementsByTagName("Validacion");

            if(valid.Count == 0 || !IsValidUser(valid[0]))
            {
                return userProfiles;
            }
            XmlNodeList user = root.GetElementsByTagName("Usuario");
            if (user.Count > 0)
            {
                UserData(user[0], userProfiles);
            }

            XmlNodeList module = root.GetElementsByTagName("Modulo");
            if (module.Count > 0) 
            {
                userProfiles.Module = module[0]["Nombre"].InnerText;
            }
            
            return userProfiles;
        }

        private bool IsValidUser(XmlNode valid)
        {
            return valid["UsuarioOk"].InnerText == "true";
        }

        private void UserData(XmlNode user, UserProfiles userProfiles)
        {
            userProfiles.PortalId = Convert.ToInt32(user["IdUsuario"].InnerText);
            userProfiles.UserId = Guid.Parse(user["GUID"].InnerText);
            UserProfiles(user["Perfiles"], userProfiles);
        }

        private void UserProfiles(XmlNode profiles, UserProfiles userProfiles)
        {
            foreach(XmlNode profile in profiles.ChildNodes)
            {
                userProfiles.Profiles.Add(new UserModuleProfileDTO()
                {
                    Id = Convert.ToInt32(profile["Id"].InnerText),
                    Name = profile["Nombre"].InnerText,
                    Description = profile["Descripcion"].InnerText
                });
            }
        }
    }



}
