using Bassi.ADO.Utilities;
using Bassi.Api.Model;
using Bassi.CrudBassi.API.Controllers;
using Bassi.CrudBassi.DTO;
using Bassi.CrudBassi.Manager;
using Bassi.CrudBassi.Manager.Interface;
using Bassi.CrudBassi.Repository;
using Bassi.CrudBassi.Repository.Interfaces;
using Bassi.CrudBassi.Repository.VOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Bassi.CrudBassi.Test
{
    [TestClass]
    public class ProductTest
    {
        private readonly ProductController _controller;
        private readonly IProductManager _manager;

        private readonly DateTime _dataTime;
        private readonly DateTime _dataTime2;

        private readonly IProductRepository _productRepository;


        public ProductTest()
        {
            _dataTime = DateTime.Parse("01/01/2000");
            _dataTime2 = DateTime.Parse("01/02/2000");


            var options = new DbContextOptionsBuilder<DbContextEf>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            DbContextEf dbContextEf = new DbContextEf(options);

            _productRepository = new ProductRepository(null, dbContextEf, null);




            _manager = new ProductManager(_productRepository);

            _controller = new ProductController(null, _manager);

            foreach (var item in GetData())
            {
                _controller.Post(item);
            }

        }

        [TestMethod]
        public void Test_GetAll()
        {
            var result = (OkObjectResult)_controller.GetAll();

            var content = (ManagerResponse<BasePaginationReturn<ProductReturnDTO>>)result.Value;

            Assert.AreNotEqual(content.Data.Results.Count, 0);

            foreach (var item in content.Data.Results)
            {
                Assert.AreEqual(item.Name, "TestData");
            }
        }


        [TestMethod]
        public void Test_Page()
        {
            var model = new ProductPaginationParameterDTO();

            model.Ascending = false;
            model.PageNumber = 1;
            model.PageSize = 999999;

            var result = (OkObjectResult)_controller.GetPage(model);

            var content = (ManagerResponse<BasePaginationReturn<ProductReturnDTO>>)result.Value;

            Assert.AreNotEqual(content.Data.Results.Count, 0);


            foreach (var item in content.Data.Results)
            {
                Assert.AreEqual(item.Name, "TestData");
            }
        }


       [TestMethod]
        public void Test_Post_BadRequest()
        {
            var result = (BadRequestObjectResult)_controller.Post(null);

            var content = (ManagerResponse<ProductReturnDTO>)result.Value;

            Assert.AreEqual(content.InternalStatusCode, 500);

            Assert.AreNotEqual(content.Message, string.Empty);

            Assert.AreNotEqual(content.Message, null);
        }

       [TestMethod]
        public void Test_Put_BadRequest()
        {
            var result = (BadRequestObjectResult)_controller.Put(null);

            var content = (ManagerResponse<ProductReturnDTO>)result.Value;

            Assert.AreEqual(content.InternalStatusCode, 500);

            Assert.AreNotEqual(content.Message, string.Empty);

            Assert.AreNotEqual(content.Message, null);
        }

       [TestMethod]
        public void Test_Get_NotFound()
        {
            var result = (NotFoundObjectResult)_controller.Get(999999);

            var content = (ManagerResponse<ProductReturnDTO>)result.Value;

            Assert.AreEqual(content.InternalStatusCode, 404);

            Assert.AreNotEqual(content.Message, string.Empty);

            Assert.AreNotEqual(content.Message, null);
        }

        [TestMethod]
        public void Test_Get()
        {
            var result = (OkObjectResult)_controller.Get(1);

            var content = (ManagerResponse<ProductReturnDTO>)result.Value;

            Assert.AreEqual(content.Data.Id, 1);

            Assert.AreEqual(content.Data.Name, "TestData");

        }

        [TestMethod]
        public void Test_Put_v2()
        {
            var result = (OkObjectResult)_controller.Get(1);

            var content = (ManagerResponse<ProductReturnDTO>)result.Value;

            var put = content.Data;

            put.Name = "UpdaData";

            var putModel = DtoMapper.GetProductParameterDTO(put);

            _controller.Put(putModel);

            var result2 = (OkObjectResult)_controller.Get(1);

            var content2 = (ManagerResponse<ProductReturnDTO>)result.Value;

            var vo = content.Data;


            Assert.AreEqual(vo.Name, "UpdaData");

        }

        [TestMethod]
        public void Test_Put()
        {
            var put = GetData().FirstOrDefault();

            put.Id = 1;

            put.Name = "UpdaData";

            _controller.Put(put);

            var vo = _productRepository.GetProductVO(1);

            Assert.AreEqual(vo.IdStatus, (int)EntityStatus.Modified);

            Assert.AreEqual(vo.Name, "UpdaData");
        }

        [TestMethod]
        public void Test_Delete()
        {
            _controller.Delete(2);

            var vo = _productRepository.GetProductVO(2);

            Assert.AreEqual(vo, null);
        }

        [TestMethod]
        public void Test_Post()
        {
            var result = (OkObjectResult)_controller.Post(GetData().FirstOrDefault());

            var content = (ManagerResponse<int>)result.Value;

            Assert.AreNotEqual(content.Data, 0);
        }

        private List<ProductParameterDTO> GetData()
        {
            var res = new List<ProductParameterDTO>();

            res.Add(new ProductParameterDTO
            {
                Id = 0,
                Name = "TestData",
            });

            res.Add(new ProductParameterDTO
            {
                Id = 0,
                Name = "TestData",
            });

            return res;
        }

    }
}

