using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Bassi.Portal.Service.DTO;

namespace Bassi.Portal.Service
{
    public partial class PortalService
    {

        public enum PortalStatusLogin
        {
            Success = 0,
            ExceptionUsuarioNoValido,
            ExceptionContrasenaNoValida,
            ExceptionMaximunPasswordAttemptsReached,
            Other = 99
        };

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userLogin">Usuario para loguearse</param>
        /// <param name="passwordLogin">Clave del usuario</param>
        /// <param name="portalModule">modulo del portal que se loguea</param>
        /// <param name="credential">Credenciales para ejecutar el metodo</param>
        /// <returns></returns>
        public LoginUserPortalDTO Login(string userLogin, string passwordLogin, string portalModule, CredentialDTO credential)
        {
            LoginUserPortalDTO loginUserPortalDTO = new LoginUserPortalDTO();

            Dictionary<string, object> loginParams = new Dictionary<string, object>();
            loginParams.Add("correoElectronico", userLogin);
            loginParams.Add("clave", passwordLogin);
            loginParams.Add("codModulo", portalModule);
            loginParams.Add("CodigoUsuario", credential.User);
            loginParams.Add("PasswordUsuario", credential.Password);

            try
            {
                XmlDocument bodyRequest = SoapMethodBody(CreateSoapEnvelope(), "LoginPortal", loginParams);
                HttpWebRequest request = PortalRequest("POST", new List<string>(new string[] { @"SOAPAction: ""http://tempuri.org/LoginPortal""" }));
                
                using(Stream body = request.GetRequestStream())
                {
                    bodyRequest.Save(body);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string data = "";
                using(StreamReader responseService = new StreamReader(response.GetResponseStream()))
                {
                    data = responseService.ReadToEnd();
                }

                XmlDocument xmlResponse = new XmlDocument();
                xmlResponse.LoadXml(data);

                loginUserPortalDTO = LoginUserDataPortal(xmlResponse);

            }
            catch(Exception ex)
            {
                loginUserPortalDTO.IsValid = false;
                loginUserPortalDTO.InternalStatus = (int)PortalStatus("Other");
                loginUserPortalDTO.Message = ex.Message;
            }
            return loginUserPortalDTO;
        }

        /// <summary>
        /// Parsea la respuesta del servicio
        /// </summary>
        /// <param name="xmlPortalLoginResponse">XML que devuelve el login del portal</param>
        /// <returns></returns>
        private LoginUserPortalDTO LoginUserDataPortal(XmlDocument xmlPortalLoginResponse)
        {
            LoginUserPortalDTO loginUser = new LoginUserPortalDTO();
            XmlDocument xmlPortalLogin = new XmlDocument();

            xmlPortalLogin.LoadXml(xmlPortalLoginResponse.InnerText);

            XmlNode root = xmlPortalLogin.DocumentElement;
            XmlNode xmlValid = root.SelectSingleNode("Validacion");
            XmlNode xmlUser = root.SelectSingleNode("Usuario");

            if (xmlValid != null)
            {
                if (xmlValid.SelectSingleNode("UsuarioOk") != null)
                {
                    loginUser.IsValid = xmlValid.SelectSingleNode("UsuarioOk").InnerText == "true";
                }
                if (xmlValid.SelectSingleNode("TypeException") != null)
                {
                    loginUser.InternalStatus = (int)PortalStatus(xmlValid.SelectSingleNode("TypeException").InnerText);
                    loginUser.Message = PortalException((PortalStatusLogin)loginUser.InternalStatus);
                }
            }

            if (xmlUser != null)
            {
                loginUser.User = new UserPortalDTO();
                if (xmlUser.SelectSingleNode("CodigoPais") != null)
                {
                    loginUser.User.AcCountry = Convert.ToInt16(xmlUser.SelectSingleNode("CodigoPais").InnerText);
                }

                if (xmlUser.SelectSingleNode("Nombre") != null)
                {
                    loginUser.User.FirstName = xmlUser.SelectSingleNode("Nombre").InnerText;
                }

                if (xmlUser.SelectSingleNode("Apellido") != null)
                {
                    loginUser.User.LastName = xmlUser.SelectSingleNode("Apellido").InnerText;
                }

                if (xmlUser.SelectSingleNode("GUID") != null)
                {
                    loginUser.User.IUser = new Guid(xmlUser.SelectSingleNode("GUID").InnerText);
                }

                if (xmlUser.SelectSingleNode("Correoelectronico") != null)
                {
                    loginUser.User.Email = xmlUser.SelectSingleNode("Correoelectronico").InnerText;
                }

                if (xmlUser.SelectSingleNode("Perfiles") != null)
                {
                    loginUser.User.Profiles = new List<ProfileModulePortalDTO>();
                    foreach (XmlNode profile in xmlUser.SelectSingleNode("Perfiles").SelectNodes("Perfil"))
                    {
                        loginUser.User.Profiles.Add(new ProfileModulePortalDTO()
                        {
                            Id = Convert.ToInt32(profile.SelectSingleNode("Id").InnerText),
                            Name = profile.SelectSingleNode("Nombre").InnerText,
                            Description = profile.SelectSingleNode("Descripcion").InnerText
                        });
                    }
                }
                loginUser.InternalStatus = (int)PortalStatus("Success");
                loginUser.Message = PortalException((PortalStatusLogin)loginUser.InternalStatus);
            }

            return loginUser;
        }

        private PortalStatusLogin PortalStatus(string typeException) => typeException switch
        {
            "ExceptionUsuarioNoValido" => PortalStatusLogin.ExceptionUsuarioNoValido,
            "ExceptionContrasenaNoValida" => PortalStatusLogin.ExceptionUsuarioNoValido,
            "ExceptionMaximunPasswordAttemptsReached" => PortalStatusLogin.ExceptionMaximunPasswordAttemptsReached,
            "Success" => PortalStatusLogin.Success,
            _ => PortalStatusLogin.Other,
        };

        private string PortalException(PortalStatusLogin exceptionPortal) => exceptionPortal switch
        {
            PortalStatusLogin.ExceptionUsuarioNoValido => "User or Password not valid.",
            PortalStatusLogin.ExceptionContrasenaNoValida => "User or Password not valid.",
            PortalStatusLogin.ExceptionMaximunPasswordAttemptsReached => "The account has been blocked, contact the help desk.",
            PortalStatusLogin.Success => "User login success",
            _ => "Has problem to login",
        };
    }
}
