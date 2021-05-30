using System;
using System.Collections.Generic;
using System.Text;
using Bassi.CrudBassi.DTO;
using Bassi.CrudBassi.Repository.VOs;
namespace Bassi.CrudBassi.Manager
{
    public static class DtoMapper
    {

        #region Receipt

        public static ReceiptParameterVO GetReceiptParameterVO(ReceiptParameterDTO parameterDTO)
        {
            if (parameterDTO == null)
                return null;

            if (parameterDTO.Client != null && parameterDTO.Client.Id != 0) { parameterDTO.ClientId = parameterDTO.Client.Id; }
            var parametersVO = new ReceiptParameterVO()
            {
                Id = parameterDTO.Id,
                ClientId = parameterDTO.ClientId,
            };


            return parametersVO;
        }

        public static ReceiptParameterDTO GetReceiptParameterDTO(ReceiptReturnDTO model)
        {
            var response = new ReceiptParameterDTO();
 
            response.Id = model.Id;
            response.ClientId = model.ClientId;
 
            if (model.ReceiptDetail != null)
                response.ReceiptDetail = model.ReceiptDetail;
 
            return response;
        }
        public static ReceiptReturnDTO GetReceiptReturnDTO(ReceiptReturnVO parameter)
        {
            if (parameter == null)
                return null;


            var response = new ReceiptReturnDTO()
            {
                Id = parameter.Id,
                ClientId = parameter.ClientId,
            };

            return response;
        }

        public static ReceiptPaginationParameterVO GetReceiptPaginationParameterVO(ReceiptPaginationParameterDTO parameter)
        {
            if (parameter == null)
                return null;

            ReceiptPaginationParameterVO response = new ReceiptPaginationParameterVO();

                response.Ascending = parameter.Ascending;
                response.PageNumber = parameter.PageNumber;
                response.OrderBy = parameter.OrderBy;
                response.PageSize = parameter.PageSize;

            return response;
        }

        public static List<ReceiptReturnDTO> GetReceiptReturnDTOs(List<ReceiptReturnVO> parameters)
        {
            if (parameters == null)
                return null;

            var response = new List<ReceiptReturnDTO>();

            foreach (var item in parameters)
            {
                response.Add(GetReceiptReturnDTO(item));
            }

            return response;
        }

        #endregion Receipt

        #region ReceiptDetail

        public static ReceiptDetailReturnDTO GetReceiptDetailReturnDTO(ReceiptDetailReturnVO parameter)
        {
            if (parameter == null)
                return null;


            var response = new ReceiptDetailReturnDTO()
            {
                Id = parameter.Id,
                ProductId = parameter.ProductId,
                Amount = parameter.Amount,
            };

            return response;
        }

        public static ReceiptDetailPaginationParameterVO GetReceiptDetailPaginationParameterVO(ReceiptDetailPaginationParameterDTO parameter)
        {
            if (parameter == null)
                return null;

            ReceiptDetailPaginationParameterVO response = new ReceiptDetailPaginationParameterVO();

                response.Ascending = parameter.Ascending;
                response.PageNumber = parameter.PageNumber;
                response.OrderBy = parameter.OrderBy;
                response.PageSize = parameter.PageSize;

            return response;
        }

        public static List<ReceiptDetailReturnDTO> GetReceiptDetailReturnDTOs(List<ReceiptDetailReturnVO> parameters)
        {
            if (parameters == null)
                return null;

            var response = new List<ReceiptDetailReturnDTO>();

            foreach (var item in parameters)
            {
                response.Add(GetReceiptDetailReturnDTO(item));
            }

            return response;
        }

        #endregion ReceiptDetail

        #region Client

        public static ClientParameterVO GetClientParameterVO(ClientParameterDTO parameterDTO)
        {
            if (parameterDTO == null)
                return null;

            var parametersVO = new ClientParameterVO()
            {
                Id = parameterDTO.Id,
                Name = parameterDTO.Name,
            };


            return parametersVO;
        }

        public static ClientParameterDTO GetClientParameterDTO(ClientReturnDTO model)
        {
            var response = new ClientParameterDTO();
 
            response.Id = model.Id;
            response.Name = model.Name;
 
            return response;
        }
        public static ClientReturnDTO GetClientReturnDTO(ClientReturnVO parameter)
        {
            if (parameter == null)
                return null;


            var response = new ClientReturnDTO()
            {
                Id = parameter.Id,
                Name = parameter.Name,
            };

            return response;
        }

        public static ClientPaginationParameterVO GetClientPaginationParameterVO(ClientPaginationParameterDTO parameter)
        {
            if (parameter == null)
                return null;

            ClientPaginationParameterVO response = new ClientPaginationParameterVO();

                response.Ascending = parameter.Ascending;
                response.PageNumber = parameter.PageNumber;
                response.OrderBy = parameter.OrderBy;
                response.PageSize = parameter.PageSize;

            return response;
        }

        public static List<ClientReturnDTO> GetClientReturnDTOs(List<ClientReturnVO> parameters)
        {
            if (parameters == null)
                return null;

            var response = new List<ClientReturnDTO>();

            foreach (var item in parameters)
            {
                response.Add(GetClientReturnDTO(item));
            }

            return response;
        }

        #endregion Client

        #region Product

        public static ProductParameterVO GetProductParameterVO(ProductParameterDTO parameterDTO)
        {
            if (parameterDTO == null)
                return null;

            var parametersVO = new ProductParameterVO()
            {
                Id = parameterDTO.Id,
                Name = parameterDTO.Name,
            };


            return parametersVO;
        }

        public static ProductParameterDTO GetProductParameterDTO(ProductReturnDTO model)
        {
            var response = new ProductParameterDTO();
 
            response.Id = model.Id;
            response.Name = model.Name;
 
            return response;
        }
        public static ProductReturnDTO GetProductReturnDTO(ProductReturnVO parameter)
        {
            if (parameter == null)
                return null;


            var response = new ProductReturnDTO()
            {
                Id = parameter.Id,
                Name = parameter.Name,
            };

            return response;
        }

        public static ProductPaginationParameterVO GetProductPaginationParameterVO(ProductPaginationParameterDTO parameter)
        {
            if (parameter == null)
                return null;

            ProductPaginationParameterVO response = new ProductPaginationParameterVO();

                response.Ascending = parameter.Ascending;
                response.PageNumber = parameter.PageNumber;
                response.OrderBy = parameter.OrderBy;
                response.PageSize = parameter.PageSize;

            return response;
        }

        public static List<ProductReturnDTO> GetProductReturnDTOs(List<ProductReturnVO> parameters)
        {
            if (parameters == null)
                return null;

            var response = new List<ProductReturnDTO>();

            foreach (var item in parameters)
            {
                response.Add(GetProductReturnDTO(item));
            }

            return response;
        }

        #endregion Product

    }
}
