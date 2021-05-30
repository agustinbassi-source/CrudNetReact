using Bassi.CrudBassi.Repository.VOs;
using Bassi.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Repository.Interfaces
{
    public interface IProductRepository
    {
        public ProductReturnVO GetProductVO(int id);
        public BasePaginationReturn<ProductReturnVO> GetListProductVO(ProductPaginationParameterVO parameterVO);
        public int PostProductVO(ProductParameterVO parameterVO);
        public int PutProductVO(ProductParameterVO parameterVO);
        public int DeleteProductVO(int id);
    }
}
