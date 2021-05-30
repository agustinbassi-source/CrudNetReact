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

namespace Bassi.CrudBassi.Repository
{
    public class ReceiptDetailRepositoryMock : IReceiptDetailRepository
    {
        private   List<ReceiptDetailReturnVO> _data;
        public ReceiptDetailRepositoryMock()
        {
            _data = new List<ReceiptDetailReturnVO>();
        }

        public ReceiptDetailReturnVO GetReceiptDetailVO(int id)
        {
            var res = _data.Where(x => x.Id == id).FirstOrDefault();

            return res;
        }
        public BasePaginationReturn<ReceiptDetailReturnVO> GetListReceiptDetailVO(ReceiptDetailPaginationParameterVO parameterVO)
        {
            var res = new BasePaginationReturn<ReceiptDetailReturnVO>();

            res.Results = new List<ReceiptDetailReturnVO>();

            var query = _data.AsQueryable();

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

            post.Id = _data.Count + 1;

            _data.Add(post);


            return post.Id;
        }
        public int PutReceiptDetailVO(ReceiptDetailParameterVO parameterVO)
        {
            var put = _data.Where(x => x.Id == parameterVO.Id).FirstOrDefault();
        
            if (put == null)
                return 0;
        
            put.IdStatus = (int)EntityStatus.Modified;
            put.ProductId = parameterVO.ProductId;
            put.Amount = parameterVO.Amount;
        

            int recordsUpdated = 1;

            return recordsUpdated;
        }
        public int DeleteReceiptDetailVO(int id)
        {
            _data.Remove(_data.Where(x => x.Id == id).FirstOrDefault());

            int recordsDeleted = 1;

            return recordsDeleted;
        }
        public List<ReceiptDetailReturnVO> GetListReceiptDetailByReceiptIdVO(int receiptId)
        {
            var res = new List<ReceiptDetailReturnVO>();
            res = _data.Where(x => x.ReceiptId == receiptId).ToList();
                return res;
        }
      
    }
}

