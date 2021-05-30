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
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

       public ManagerResponse<int> PostProductDTO(ProductParameterDTO parameterDTO)
        {
            var response = new ManagerResponse<int>();

            var parametersVO = DtoMapper.GetProductParameterVO(parameterDTO); 

            parametersVO.Id = _productRepository.PostProductVO(parametersVO);

          response.Data = parametersVO.Id;

            return response;
        }


       public ManagerResponse<int> PutProductDTO(ProductParameterDTO parameterDTO)
        {
            var response = new ManagerResponse<int>();

            var parametersVO = DtoMapper.GetProductParameterVO(parameterDTO); 

            var get = _productRepository.PutProductVO(parametersVO);

          response.Data = get;

            return response;
        }


       public ManagerResponse<int> DeleteProductDTO(int id)
        {
            ManagerResponse<int> response = new ManagerResponse<int>();

            response.Data = _productRepository.DeleteProductVO(id);

            return response;
        }


       public ManagerResponse<ProductReturnDTO> GetProductDTO(int id)
        {
            ManagerResponse<ProductReturnDTO> response = new ManagerResponse<ProductReturnDTO>();

            var get = _productRepository.GetProductVO(id);

            if (get == null)
                return response;

            response.Data = DtoMapper.GetProductReturnDTO(get);

            #region Relaciones
            #endregion Relaciones

            return response;
        }


       public ManagerResponse<BasePaginationReturn<ProductReturnDTO>> GetListProductDTO(ProductPaginationParameterDTO parameterDTO)
        {
           ManagerResponse<BasePaginationReturn<ProductReturnDTO>> response = new ManagerResponse<BasePaginationReturn<ProductReturnDTO>>();

            var parameterVO = DtoMapper.GetProductPaginationParameterVO(parameterDTO);

            var get = _productRepository.GetListProductVO(parameterVO);

            response.Data = new BasePaginationReturn<ProductReturnDTO>();

            response.Data.TotalResults = get.TotalResults;


            response.Data.Results = DtoMapper.GetProductReturnDTOs(get.Results);

            return response;
        }


    }
}
