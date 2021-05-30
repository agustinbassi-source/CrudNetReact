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
    public class ClientRepositoryMock : IClientRepository
    {
        private   List<ClientReturnVO> _data;
        public ClientRepositoryMock()
        {
            _data = new List<ClientReturnVO>();
        }

        public ClientReturnVO GetClientVO(int id)
        {
            var res = _data.Where(x => x.Id == id).FirstOrDefault();

            return res;
        }
        public BasePaginationReturn<ClientReturnVO> GetListClientVO(ClientPaginationParameterVO parameterVO)
        {
            var res = new BasePaginationReturn<ClientReturnVO>();

            res.Results = new List<ClientReturnVO>();

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
        public int PostClientVO(ClientParameterVO parameterVO)
        {
            var post = new ClientReturnVO
            {
                 Name = parameterVO.Name,
            };

            post.Id = _data.Count + 1;

            _data.Add(post);


            return post.Id;
        }
        public int PutClientVO(ClientParameterVO parameterVO)
        {
            var put = _data.Where(x => x.Id == parameterVO.Id).FirstOrDefault();
        
            if (put == null)
                return 0;
        
            put.IdStatus = (int)EntityStatus.Modified;
            put.Name = parameterVO.Name;
        

            int recordsUpdated = 1;

            return recordsUpdated;
        }
        public int DeleteClientVO(int id)
        {
            _data.Remove(_data.Where(x => x.Id == id).FirstOrDefault());

            int recordsDeleted = 1;

            return recordsDeleted;
        }
      
    }
}

