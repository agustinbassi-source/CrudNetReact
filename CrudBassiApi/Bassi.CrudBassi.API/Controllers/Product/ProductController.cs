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
    public class ProductController : DefaultControllerBase
    {
        private readonly IConfiguration _configuration;
        readonly IProductManager _productManager;

        public ProductController(IConfiguration configuration,  IProductManager  productManager)
        {
            _productManager = productManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/Products/{id}")]
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

                var res = _productManager.GetProductDTO(id);
  
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
                ManagerResponse<ProductReturnDTO> res = new ManagerResponse<ProductReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("api/Products")]
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

                var model = new ProductPaginationParameterDTO();

                model.Ascending = false;
                model.PageNumber = 1;
                model.PageSize = 999999;
                var res = _productManager.GetListProductDTO(model);
  
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
                ManagerResponse<ProductReturnDTO> res = new ManagerResponse<ProductReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/Products/GetPage")]
        public IActionResult GetPage(ProductPaginationParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _productManager.GetListProductDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ProductReturnDTO> res = new ManagerResponse<ProductReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/Products")]
        public IActionResult Post(ProductParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _productManager.PostProductDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ProductReturnDTO> res = new ManagerResponse<ProductReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpPut]
        [Route("api/Products/{id}")]
        public IActionResult Put(ProductParameterDTO model)
        {
            try
            {
                    string info = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    if (info != null && info.Length > 21844)
                        info = info.Substring(0, 21844);

                if (_configuration != null && _configuration.GetValue<bool>("UseLog"))
                {
                }

                var res = _productManager.PutProductDTO(model);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ProductReturnDTO> res = new ManagerResponse<ProductReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("api/Products/{id}")]
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

                var res = _productManager.DeleteProductDTO(id);
  
                return Ok(res);
            }
            catch (System.Exception e)
            {
                ManagerResponse<ProductReturnDTO> res = new ManagerResponse<ProductReturnDTO>();

                res.Message = e.Message;
                if (e.InnerException != null && e.InnerException.Message != null)
                { res.Message += e.InnerException.Message; }
                res.InternalStatusCode = 500;


                return BadRequest(res);
            }
        }

    }
}
