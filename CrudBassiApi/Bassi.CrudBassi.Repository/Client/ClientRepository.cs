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
    public class ClientRepository : IClientRepository
    {
        private readonly ISqlStoreExcuter _sqlStoreExcuter;
        private readonly DbContextEf _dbContextEf;
        private readonly IConfiguration _configuration;

        public ClientRepository(ISqlStoreExcuter sqlStoreExcuter, DbContextEf dbContextEf, IConfiguration configuration)
        {
            _sqlStoreExcuter = sqlStoreExcuter;
            _dbContextEf = dbContextEf;
            _configuration = configuration;
        }

        public ClientReturnVO GetClientVO(int id)
        {
            var res = _dbContextEf.ClientReturnVO.Where(x => x.Id == id).FirstOrDefault();

            return res;
        }
        public BasePaginationReturn<ClientReturnVO> GetListClientVO(ClientPaginationParameterVO parameterVO)
        {
            var res = new BasePaginationReturn<ClientReturnVO>();

            res.Results = new List<ClientReturnVO>();

            var query = _dbContextEf.ClientReturnVO.AsQueryable();

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
        
            _dbContextEf.Add(post);

            _dbContextEf.SaveChanges();

            return post.Id;
        }
        public int PutClientVO(ClientParameterVO parameterVO)
        {
            var put = _dbContextEf.ClientReturnVO.Where(x => x.Id == parameterVO.Id).FirstOrDefault();
        
            if (put == null)
                return 0;
        
            put.Name = parameterVO.Name;
        
            _dbContextEf.Update(put);

            int recordsUpdated = _dbContextEf.SaveChanges();

            return recordsUpdated;
        }
        public int DeleteClientVO(int id)
        {
            _dbContextEf.ClientReturnVO.Remove(_dbContextEf.ClientReturnVO.Where(x => x.Id == id).FirstOrDefault());

            int recordsDeleted = _dbContextEf.SaveChanges();

            return recordsDeleted;
        }
      
    }
}

