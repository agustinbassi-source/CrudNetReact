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
    public class ReceiptManager : IReceiptManager
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptDetailRepository _receiptDetailRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public ReceiptManager(IReceiptRepository receiptRepository, IReceiptDetailRepository receiptDetailRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _receiptRepository = receiptRepository;
            _receiptDetailRepository = receiptDetailRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

       public ManagerResponse<int> PostReceiptDTO(ReceiptParameterDTO parameterDTO)
        {
            var response = new ManagerResponse<int>();

            var parametersVO = DtoMapper.GetReceiptParameterVO(parameterDTO); 

            parametersVO.Id = _receiptRepository.PostReceiptVO(parametersVO);

          response.Data = parametersVO.Id;

            #region Grilla ReceiptDetail

            if (parameterDTO.ReceiptDetail != null)
            {


             foreach (var row in parameterDTO.ReceiptDetail)
               {
                   var rowParameter = new ReceiptDetailParameterVO
                   {
                       Id = row.Id,
                       ReceiptId = parametersVO.Id,
                      ProductId = row.ProductId,
                      Amount = row.Amount,
                   };

                  if (row.Id == 0)
                 {
                       row.Id = _receiptDetailRepository.PostReceiptDetailVO(rowParameter);
                   }
                   else
                   {
                       _receiptDetailRepository.PutReceiptDetailVO(rowParameter);
                   }
                }

            }

            #endregion Grilla ReceiptDetail

            return response;
        }


       public ManagerResponse<int> PutReceiptDTO(ReceiptParameterDTO parameterDTO)
        {
            var response = new ManagerResponse<int>();

            var parametersVO = DtoMapper.GetReceiptParameterVO(parameterDTO); 

            var get = _receiptRepository.PutReceiptVO(parametersVO);

          response.Data = get;

            #region Grilla ReceiptDetail

            if (parameterDTO.ReceiptDetail != null)
            {

                var receiptDetail = _receiptDetailRepository.GetListReceiptDetailByReceiptIdVO(parameterDTO.Id).Select(x => x.Id).ToList();

                foreach (var idDelete in receiptDetail)
                {
                    if (!parameterDTO.ReceiptDetail.Select(x => x.Id).ToList().Contains(idDelete))
                        _receiptDetailRepository.DeleteReceiptDetailVO(idDelete);

                }

             foreach (var row in parameterDTO.ReceiptDetail)
               {
                   var rowParameter = new ReceiptDetailParameterVO
                   {
                       Id = row.Id,
                       ReceiptId = parameterDTO.Id,
                      ProductId = row.ProductId,
                      Amount = row.Amount,
                   };

                  if (row.Id == 0)
                 {
                       row.Id = _receiptDetailRepository.PostReceiptDetailVO(rowParameter);
                   }
                   else
                   {
                       _receiptDetailRepository.PutReceiptDetailVO(rowParameter);
                   }
                }

            }

            #endregion Grilla ReceiptDetail

            return response;
        }


       public ManagerResponse<int> DeleteReceiptDTO(int id)
        {
            ManagerResponse<int> response = new ManagerResponse<int>();

            var receiptDetail = _receiptDetailRepository.GetListReceiptDetailByReceiptIdVO(id).Select(x=> x.Id).ToList();

            foreach (var idDelete in  receiptDetail)
            {
                _receiptDetailRepository.DeleteReceiptDetailVO(idDelete);
            }
           

            response.Data = _receiptRepository.DeleteReceiptVO(id);

            return response;
        }


       public ManagerResponse<ReceiptReturnDTO> GetReceiptDTO(int id)
        {
            ManagerResponse<ReceiptReturnDTO> response = new ManagerResponse<ReceiptReturnDTO>();

            var get = _receiptRepository.GetReceiptVO(id);

            if (get == null)
                return response;

            response.Data = DtoMapper.GetReceiptReturnDTO(get);

            #region Grilla ReceiptDetail

            var receiptDetailGet = _receiptDetailRepository.GetListReceiptDetailByReceiptIdVO(id);

            response.Data.ReceiptDetail = DtoMapper.GetReceiptDetailReturnDTOs(receiptDetailGet);

            if (response.Data.ReceiptDetail != null)
            {
                foreach (var item in response.Data.ReceiptDetail)
                {
                    item.Product = item.ProductId != null ? DtoMapper.GetProductReturnDTO(_productRepository.GetProductVO(Convert.ToInt32(item.ProductId))) : null;
                }
            }

            #endregion Grilla ReceiptDetail

            #region Relaciones
            if (response.Data.ClientId != null) response.Data.Client = DtoMapper.GetClientReturnDTO(_clientRepository.GetClientVO(Convert.ToInt32(response.Data.ClientId))); 
            #endregion Relaciones

            return response;
        }


       public ManagerResponse<BasePaginationReturn<ReceiptReturnDTO>> GetListReceiptDTO(ReceiptPaginationParameterDTO parameterDTO)
        {
           ManagerResponse<BasePaginationReturn<ReceiptReturnDTO>> response = new ManagerResponse<BasePaginationReturn<ReceiptReturnDTO>>();

            var parameterVO = DtoMapper.GetReceiptPaginationParameterVO(parameterDTO);

            var get = _receiptRepository.GetListReceiptVO(parameterVO);

            response.Data = new BasePaginationReturn<ReceiptReturnDTO>();

            response.Data.TotalResults = get.TotalResults;


            response.Data.Results = DtoMapper.GetReceiptReturnDTOs(get.Results);
            #region Relaciones

            foreach (var dto in response.Data.Results)
            {
                if (dto.ClientId != null) dto.Client = DtoMapper.GetClientReturnDTO(_clientRepository.GetClientVO(Convert.ToInt32(dto.ClientId))); 
            }

            #endregion Relaciones

            return response;
        }


    }
}
