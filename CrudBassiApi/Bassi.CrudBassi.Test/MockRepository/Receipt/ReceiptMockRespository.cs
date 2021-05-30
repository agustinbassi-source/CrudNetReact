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
    public class ReceiptRepositoryMock : IReceiptRepository
    {
        private   List<ReceiptReturnVO> _data;
        public ReceiptRepositoryMock()
        {
            _data = new List<ReceiptReturnVO>();
        }

        public ReceiptReturnVO GetReceiptVO(int id)
        {
            var res = _data.Where(x => x.Id == id).FirstOrDefault();

            return res;
        }
        public BasePaginationReturn<ReceiptReturnVO> GetListReceiptVO(ReceiptPaginationParameterVO parameterVO)
        {
            var res = new BasePaginationReturn<ReceiptReturnVO>();

            res.Results = new List<ReceiptReturnVO>();

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
        public int PostReceiptVO(ReceiptParameterVO parameterVO)
        {
            var post = new ReceiptReturnVO
            {
                 ClientId = parameterVO.ClientId,
            };

            post.Id = _data.Count + 1;

            _data.Add(post);


            return post.Id;
        }
        public int PutReceiptVO(ReceiptParameterVO parameterVO)
        {
            var put = _data.Where(x => x.Id == parameterVO.Id).FirstOrDefault();
        
            if (put == null)
                return 0;
        
            put.IdStatus = (int)EntityStatus.Modified;
            put.ClientId = parameterVO.ClientId;
        

            int recordsUpdated = 1;

            return recordsUpdated;
        }
        public int DeleteReceiptVO(int id)
        {
            _data.Remove(_data.Where(x => x.Id == id).FirstOrDefault());

            int recordsDeleted = 1;

            return recordsDeleted;
        }
      
    }
}

