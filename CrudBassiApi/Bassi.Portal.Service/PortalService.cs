using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;

namespace Bassi.Portal.Service
{
    /// <summary>
    /// Servicio SOAP del Portal de aplicaciones ASSISTCARD
    /// </summary>
    public partial class PortalService
    {
        private string _uri;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="URI">Url del servicio</param>
        public PortalService(string URI)
        {
            _uri = URI;
        }

        /// <summary>
        /// WebRequest Portal
        /// </summary>
        /// <param name="Verb">Verbo http POST, GET, ...</param>
        /// <returns></returns>
        protected HttpWebRequest PortalRequest(string Verb)
        {
            return PortalRequest(Verb, new List<string>());
        }

        /// <summary>
        /// WebRequest portal
        /// </summary>
        /// <param name="Verb">Verbo http POST, GET, ...</param>
        /// <param name="Headers">List HTTP Headers  ["Content-Length: 100", ....]</param>
        /// <returns></returns>
        protected HttpWebRequest PortalRequest(string Verb, List<string> Headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_uri);
            request.ContentType = @"text/xml; charset=""utf-8""";
            request.Method = Verb;

            foreach(string header in Headers)
            {
                request.Headers.Add(header);
            }

            return request;
        }

        /// <summary>
        /// Crea la estructura XML del SOAP del portal
        /// </summary>
        /// <returns></returns>
        protected XmlDocument CreateSoapEnvelope()
        {
            XmlDocument xmlSoapEnvelope = new XmlDocument();
            xmlSoapEnvelope.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd = ""http://www.w3.org/2001/XMLSchema"" xmlns:soap = ""http://schemas.xmlsoap.org/soap/envelope/"" >
                     <soap:Body></soap:Body></soap:Envelope>");
            
            return xmlSoapEnvelope;
        }

        /// <summary>
        /// SOAP xml para enviar en el request
        /// </summary>
        /// <param name="xmlSoapEnvelope">Estructura XML para completar</param>
        /// <param name="method">funcion o metodo que se llama al servicio del portal</param>
        /// <param name="params">parametros que recibe la funcion o metodo</param>
        /// <returns></returns>
        protected XmlDocument SoapMethodBody(XmlDocument xmlSoapEnvelope, string method, Dictionary<string, object> @params)
        {
            XmlNode xmlBody = xmlSoapEnvelope.DocumentElement.ChildNodes[0];
            XmlElement xmlMetodod = xmlSoapEnvelope.CreateElement(method);
            xmlMetodod.SetAttribute("xmlns", "http://tempuri.org/");
            xmlBody.AppendChild(xmlMetodod);
            XmlElement xmlSoapParam;

            foreach (KeyValuePair<string, object> param in @params)
            {
                xmlSoapParam = xmlSoapEnvelope.CreateElement(param.Key);
                xmlSoapParam.InnerText = param.Value.ToString();
                xmlMetodod.AppendChild(xmlSoapParam);
            }

            return xmlSoapEnvelope;

        }
    }
}
