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
    public class ReceiptController : DefaultControllerBase
    {
        private readonly IConfiguration _configuration;
        readonly IReceiptManager _receiptManager;

        public ReceiptController(IConfiguration configuration,  IReceiptManager  receiptManager)
        {
            _receiptManager = receiptManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/Receipts/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                    string info = id.ToString();

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _receiptManager.GetReceiptDTO(id);
  
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
                ManagerResponse<ReceiptReturnDTO> res = new ManagerResponse<ReceiptReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("api/Receipts")]
        public IActionResult GetAll()
        {
            try
            {
                    string info = null;

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration.GetValue<bool>("UseLog"))
                {
                }

                var model = new ReceiptPaginationParameterDTO();

                model.Ascending = false;
                model.PageNumber = 1;
                model.PageSize = 999999;
                var res = _receiptManager.GetListReceiptDTO(model);
  
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
                ManagerResponse<ReceiptReturnDTO> res = new ManagerResponse<ReceiptReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/Receipts/GetPage")]
        public IActionResult GetPage(ReceiptPaginationParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _receiptManager.GetListReceiptDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ReceiptReturnDTO> res = new ManagerResponse<ReceiptReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/Receipts")]
        public IActionResult Post(ReceiptParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _receiptManager.PostReceiptDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ReceiptReturnDTO> res = new ManagerResponse<ReceiptReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPut]
        [Route("api/Receipts/{id}")]
        public IActionResult Put(ReceiptParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _receiptManager.PutReceiptDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ReceiptReturnDTO> res = new ManagerResponse<ReceiptReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("api/Receipts/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                    string info = id.ToString();

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _receiptManager.DeleteReceiptDTO(id);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ReceiptReturnDTO> res = new ManagerResponse<ReceiptReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

    }
}
