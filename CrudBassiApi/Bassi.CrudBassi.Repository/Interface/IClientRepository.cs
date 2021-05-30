using Bassi.CrudBassi.Repository.VOs;
using Bassi.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Repository.Interfaces
{
    public interface IClientRepository
    {
        public ClientReturnVO GetClientVO(int id);
        public BasePaginationReturn<ClientReturnVO> GetListClientVO(ClientPaginationParameterVO parameterVO);
        public int PostClientVO(ClientParameterVO parameterVO);
        public int PutClientVO(ClientParameterVO parameterVO);
        public int DeleteClientVO(int id);
    }
}
