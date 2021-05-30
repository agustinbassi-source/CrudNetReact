using Bassi.CrudBassi.Repository.VOs;
using Bassi.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Repository.Interfaces
{
    public interface IReceiptRepository
    {
        public ReceiptReturnVO GetReceiptVO(int id);
        public BasePaginationReturn<ReceiptReturnVO> GetListReceiptVO(ReceiptPaginationParameterVO parameterVO);
        public int PostReceiptVO(ReceiptParameterVO parameterVO);
        public int PutReceiptVO(ReceiptParameterVO parameterVO);
        public int DeleteReceiptVO(int id);
    }
}
