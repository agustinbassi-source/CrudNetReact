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
    public class ReceiptTest
    {
        private readonly ReceiptController _controller;
        private readonly IReceiptManager _manager;

        private readonly DateTime _dataTime;
        private readonly DateTime _dataTime2;

        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptDetailRepository _receiptDetailRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;


        public ReceiptTest()
        {
            _dataTime = DateTime.Parse("01/01/2000");
            _dataTime2 = DateTime.Parse("01/02/2000");


            var options = new DbContextOptionsBuilder<DbContextEf>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            DbContextEf dbContextEf = new DbContextEf(options);

            _receiptRepository = new ReceiptRepository(null, dbContextEf, null);
            _receiptDetailRepository = new ReceiptDetailRepository(null, dbContextEf, null);
            _clientRepository = new ClientRepository(null, dbContextEf, null);
            _productRepository = new ProductRepository(null, dbContextEf, null);




            _manager = new ReceiptManager(_receiptRepository, _receiptDetailRepository, _clientRepository, _productRepository);

            _controller = new ReceiptController(null, _manager);

            foreach (var item in GetData())
            {
                _controller.Post(item);
            }

        }

        [TestMethod]
        public void Test_GetAll()
        {
            var result = (OkObjectResult)_controller.GetAll();

            var content = (ManagerResponse<BasePaginationReturn<ReceiptReturnDTO>>)result.Value;

            Assert.AreNotEqual(content.Data.Results.Count, 0);

            foreach (var item in content.Data.Results)
            {
                Assert.AreEqual(item.ClientId, 1);
            }
        }


        [TestMethod]
        public void Test_Page()
        {
            var model = new ReceiptPaginationParameterDTO();

            model.Ascending = false;
            model.PageNumber = 1;
            model.PageSize = 999999;

            var result = (OkObjectResult)_controller.GetPage(model);

            var content = (ManagerResponse<BasePaginationReturn<ReceiptReturnDTO>>)result.Value;

            Assert.AreNotEqual(content.Data.Results.Count, 0);


            foreach (var item in content.Data.Results)
            {
                Assert.AreEqual(item.ClientId, 1);
            }
        }


       [TestMethod]
        public void Test_Post_BadRequest()
        {
            var result = (BadRequestObjectResult)_controller.Post(null);

            var content = (ManagerResponse<ReceiptReturnDTO>)result.Value;

            Assert.AreEqual(content.InternalStatusCode, 500);

            Assert.AreNotEqual(content.Message, string.Empty);

            Assert.AreNotEqual(content.Message, null);
        }

       [TestMethod]
        public void Test_Put_BadRequest()
        {
            var result = (BadRequestObjectResult)_controller.Put(null);

            var content = (ManagerResponse<ReceiptReturnDTO>)result.Value;

            Assert.AreEqual(content.InternalStatusCode, 500);

            Assert.AreNotEqual(content.Message, string.Empty);

            Assert.AreNotEqual(content.Message, null);
        }

       [TestMethod]
        public void Test_Get_NotFound()
        {
            var result = (NotFoundObjectResult)_controller.Get(999999);

            var content = (ManagerResponse<ReceiptReturnDTO>)result.Value;

            Assert.AreEqual(content.InternalStatusCode, 404);

            Assert.AreNotEqual(content.Message, string.Empty);

            Assert.AreNotEqual(content.Message, null);
        }

        [TestMethod]
        public void Test_Get()
        {
            var result = (OkObjectResult)_controller.Get(1);

            var content = (ManagerResponse<ReceiptReturnDTO>)result.Value;

            Assert.AreEqual(content.Data.Id, 1);

            Assert.AreEqual(content.Data.ClientId, 1);


            Assert.AreEqual(content.Data.ReceiptDetail.Count, 2);

            foreach (var item in content.Data.ReceiptDetail)
            {
                Assert.AreEqual(item.ProductId, 1);
                Assert.AreEqual(item.Amount, 1);
            }

        }

        [TestMethod]
        public void Test_Put_v2()
        {
            var result = (OkObjectResult)_controller.Get(1);

            var content = (ManagerResponse<ReceiptReturnDTO>)result.Value;

            var put = content.Data;

            put.ClientId = 2;

            foreach (var item in put.ReceiptDetail)
            {
            item.ProductId = 2;
            item.Amount = 2;
            }

            put.ReceiptDetail.RemoveAll(x => x.Id == 2);

            var putModel = DtoMapper.GetReceiptParameterDTO(put);

            _controller.Put(putModel);

            var result2 = (OkObjectResult)_controller.Get(1);

            var content2 = (ManagerResponse<ReceiptReturnDTO>)result.Value;

            var vo = content.Data;

            Assert.AreNotEqual(vo.ReceiptDetail.Count, 2);
            Assert.AreEqual(vo.ReceiptDetail.FirstOrDefault().ProductId, 2);
            Assert.AreEqual(vo.ReceiptDetail.FirstOrDefault().Amount, 2);

            Assert.AreEqual(vo.ClientId, 2);

        }

        [TestMethod]
        public void Test_Put()
        {
            var put = GetData().FirstOrDefault();

            put.Id = 1;

            put.ClientId = 2;

            _controller.Put(put);

            var vo = _receiptRepository.GetReceiptVO(1);

            Assert.AreEqual(vo.IdStatus, (int)EntityStatus.Modified);

            Assert.AreEqual(vo.ClientId, 2);
        }

        [TestMethod]
        public void Test_Delete()
        {
            _controller.Delete(2);

            var vo = _receiptRepository.GetReceiptVO(2);

            Assert.AreEqual(vo, null);
        }

        [TestMethod]
        public void Test_Post()
        {
            var result = (OkObjectResult)_controller.Post(GetData().FirstOrDefault());

            var content = (ManagerResponse<int>)result.Value;

            Assert.AreNotEqual(content.Data, 0);
        }

        private List<ReceiptParameterDTO> GetData()
        {
            var res = new List<ReceiptParameterDTO>();

            res.Add(new ReceiptParameterDTO
            {
                Id = 0,
                ClientId = 1,
            });

            res.Add(new ReceiptParameterDTO
            {
                Id = 0,
                ClientId = 1,
            });

            foreach (var item in res)
            {
                item.ReceiptDetail = new List<ReceiptDetailReturnDTO>();

                item.ReceiptDetail.Add(new ReceiptDetailReturnDTO
                {
                    Id = 0,
                    ProductId = 1,
                    Amount = 1,
                });

                item.ReceiptDetail.Add(new ReceiptDetailReturnDTO
                {
                    Id = 0,
                    ProductId = 1,
                    Amount = 1,
                });

            }
            return res;
        }

    }
}

