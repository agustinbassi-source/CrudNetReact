using Bassi.ADO.Utilities;
using Bassi.CrudBassi.Repository.VOs;
using Bassi.CrudBassi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Linq;
using Bassi.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bassi.CrudBassi.Repository
{
    public class ReceiptDetailRepository : IReceiptDetailRepository
    {
        private readonly ISqlStoreExcuter _sqlStoreExcuter;
        private readonly DbContextEf _dbContextEf;
        private readonly IConfiguration _configuration;

        public ReceiptDetailRepository(ISqlStoreExcuter sqlStoreExcuter, DbContextEf dbContextEf, IConfiguration configuration)
        {
            _sqlStoreExcuter = sqlStoreExcuter;
            _dbContextEf = dbContextEf;
            _configuration = configuration;
        }

        public ReceiptDetailReturnVO GetReceiptDetailVO(int id)
        {
            var res = _dbContextEf.ReceiptDetailReturnVO.Where(x => x.Id == id).FirstOrDefault();

            return res;
        }
        public BasePaginationReturn<ReceiptDetailReturnVO> GetListReceiptDetailVO(ReceiptDetailPaginationParameterVO parameterVO)
        {
            var res = new BasePaginationReturn<ReceiptDetailReturnVO>();

            res.Results = new List<ReceiptDetailReturnVO>();

            var query = _dbContextEf.ReceiptDetailReturnVO.AsQueryable();

            res.TotalResults = query.Count();

            if (parameterVO.PageSize == 0)
            {
                if (parameterVO.OrderBy != null)
                    query = LinqExtension.OrderBy(query, parameterVO.OrderBy, parameterVO.Ascending);


                res.Results = query.ToList();
            }
            else
            {
                if (parameterVO.OrderBy != null)
                    query = LinqExtension.OrderBy(query, parameterVO.OrderBy, parameterVO.Ascending);
                else
                    query = query.OrderByDescending(x => x.Id);

                int skip = (parameterVO.PageNumber - 1) * parameterVO.PageSize;

                res.Results = query.Skip(skip).Take(parameterVO.PageSize).ToList();
            }

           return res;
        }
        public int PostReceiptDetailVO(ReceiptDetailParameterVO parameterVO)
        {
            var post = new ReceiptDetailReturnVO
            {
                 ReceiptId = parameterVO.ReceiptId,
                 ProductId = parameterVO.ProductId,
                 Amount = parameterVO.Amount,
            };
        
            _dbContextEf.Add(post);

            _dbContextEf.SaveChanges();

            return post.Id;
        }
        public int PutReceiptDetailVO(ReceiptDetailParameterVO parameterVO)
        {
            var put = _dbContextEf.ReceiptDetailReturnVO.Where(x => x.Id == parameterVO.Id).FirstOrDefault();
        
            if (put == null)
                return 0;
        
            put.ProductId = parameterVO.ProductId;
            put.Amount = parameterVO.Amount;
        
            _dbContextEf.Update(put);

            int recordsUpdated = _dbContextEf.SaveChanges();

            return recordsUpdated;
        }
        public int DeleteReceiptDetailVO(int id)
        {
            _dbContextEf.ReceiptDetailReturnVO.Remove(_dbContextEf.ReceiptDetailReturnVO.Where(x => x.Id == id).FirstOrDefault());

            int recordsDeleted = _dbContextEf.SaveChanges();

            return recordsDeleted;
        }
        public List<ReceiptDetailReturnVO> GetListReceiptDetailByReceiptIdVO(int receiptId)
        {
            var res = new List<ReceiptDetailReturnVO>();
            res = _dbContextEf.ReceiptDetailReturnVO.Where(x => x.ReceiptId == receiptId).ToList();
                return res;
        }
      
    }
}

