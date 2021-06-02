using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bassi.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bassi.CrudBassi.Manager.Interface;
using Bassi.Api.Model;
using Bassi.CrudBassi.DTO;
using Bassi.CrudBassi.Manager;
using Microsoft.Extensions.Configuration;

namespace Bassi.CrudBassi.API.Controllers
{

    [ApiController]
    public class ClientController : DefaultControllerBase
    {
        private readonly IConfiguration _configuration;
        readonly IClientManager _clientManager;

        public ClientController(IConfiguration configuration,  IClientManager  clientManager)
        {
            _clientManager = clientManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/Clients/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                    string info = id.ToString();

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _clientManager.GetClientDTO(id);
  
                if (res.Data == null)
                {
                    res.Message = "Not found";
                    res.InternalStatusCode = 404;
                    return NotFound(res);
                }

                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ClientReturnDTO> res = new ManagerResponse<ClientReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("api/Clients")]
        public IActionResult GetAll()
        {
            try
            {
                    string info = null;

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var model = new ClientPaginationParameterDTO();

                model.Ascending = false;
                model.PageNumber = 1;
                model.PageSize = 999999;
                var res = _clientManager.GetListClientDTO(model);
  
                if (res.Data == null)
                {
                    res.Message = "Not found";
                    res.InternalStatusCode = 404;
                    return NotFound(res);
                }

                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ClientReturnDTO> res = new ManagerResponse<ClientReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/Clients/GetPage")]
        public IActionResult GetPage(ClientPaginationParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _clientManager.GetListClientDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ClientReturnDTO> res = new ManagerResponse<ClientReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/Clients")]
        public IActionResult Post(ClientParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _clientManager.PostClientDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ClientReturnDTO> res = new ManagerResponse<ClientReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPut]
        [Route("api/Clients/{id}")]
        public IActionResult Put(ClientParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _clientManager.PutClientDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ClientReturnDTO> res = new ManagerResponse<ClientReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("api/Clients/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                    string info = id.ToString();

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _clientManager.DeleteClientDTO(id);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ClientReturnDTO> res = new ManagerResponse<ClientReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

    }
}
