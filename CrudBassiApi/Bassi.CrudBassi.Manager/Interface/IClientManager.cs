using Bassi.Api.Model;
using Bassi.CrudBassi.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Manager.Interface
{
    public interface IClientManager
    {
        public ManagerResponse<ClientReturnDTO> GetClientDTO(int id)
;
        public ManagerResponse<BasePaginationReturn<ClientReturnDTO>> GetListClientDTO(ClientPaginationParameterDTO parameterDTO)
;
        public ManagerResponse<int> PostClientDTO(ClientParameterDTO parameterDTO)
;
        public ManagerResponse<int> PutClientDTO(ClientParameterDTO parameterDTO)
;
        public ManagerResponse<int> DeleteClientDTO(int id)
;
    }
}
