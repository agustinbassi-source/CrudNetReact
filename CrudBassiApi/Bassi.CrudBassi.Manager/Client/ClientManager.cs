using Bassi.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Bassi.CrudBassi.Repository.Interfaces;
using Bassi.CrudBassi.DTO;
using Bassi.CrudBassi.Repository.VOs;
using Bassi.CrudBassi.Manager.Interface;
using Newtonsoft.Json;
using System.Linq;

namespace Bassi.CrudBassi.Manager
{
    public class ClientManager : IClientManager
    {
        private readonly IClientRepository _clientRepository;

        public ClientManager(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

       public ManagerResponse<int> PostClientDTO(ClientParameterDTO parameterDTO)
        {
            var response = new ManagerResponse<int>();

            var parametersVO = DtoMapper.GetClientParameterVO(parameterDTO); 

            parametersVO.Id = _clientRepository.PostClientVO(parametersVO);

          response.Data = parametersVO.Id;

            return response;
        }


       public ManagerResponse<int> PutClientDTO(ClientParameterDTO parameterDTO)
        {
            var response = new ManagerResponse<int>();

            var parametersVO = DtoMapper.GetClientParameterVO(parameterDTO); 

            var get = _clientRepository.PutClientVO(parametersVO);

          response.Data = get;

            return response;
        }


       public ManagerResponse<int> DeleteClientDTO(int id)
        {
            ManagerResponse<int> response = new ManagerResponse<int>();

            response.Data = _clientRepository.DeleteClientVO(id);

            return response;
        }


       public ManagerResponse<ClientReturnDTO> GetClientDTO(int id)
        {
            ManagerResponse<ClientReturnDTO> response = new ManagerResponse<ClientReturnDTO>();

            var get = _clientRepository.GetClientVO(id);

            if (get == null)
                return response;

            response.Data = DtoMapper.GetClientReturnDTO(get);

            #region Relaciones
            #endregion Relaciones

            return response;
        }


       public ManagerResponse<BasePaginationReturn<ClientReturnDTO>> GetListClientDTO(ClientPaginationParameterDTO parameterDTO)
        {
           ManagerResponse<BasePaginationReturn<ClientReturnDTO>> response = new ManagerResponse<BasePaginationReturn<ClientReturnDTO>>();

            var parameterVO = DtoMapper.GetClientPaginationParameterVO(parameterDTO);

            var get = _clientRepository.GetListClientVO(parameterVO);

            response.Data = new BasePaginationReturn<ClientReturnDTO>();

            response.Data.TotalResults = get.TotalResults;


            response.Data.Results = DtoMapper.GetClientReturnDTOs(get.Results);

            return response;
        }


    }
}
