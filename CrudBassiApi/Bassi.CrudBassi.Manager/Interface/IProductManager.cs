using Bassi.Api.Model;
using Bassi.CrudBassi.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Manager.Interface
{
    public interface IProductManager
    {
        public ManagerResponse<ProductReturnDTO> GetProductDTO(int id)
;
        public ManagerResponse<BasePaginationReturn<ProductReturnDTO>> GetListProductDTO(ProductPaginationParameterDTO parameterDTO)
;
        public ManagerResponse<int> PostProductDTO(ProductParameterDTO parameterDTO)
;
        public ManagerResponse<int> PutProductDTO(ProductParameterDTO parameterDTO)
;
        public ManagerResponse<int> DeleteProductDTO(int id)
;
    }
}
