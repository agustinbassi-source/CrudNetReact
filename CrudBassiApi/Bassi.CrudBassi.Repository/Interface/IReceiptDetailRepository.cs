using Bassi.CrudBassi.Repository.VOs;
using Bassi.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Repository.Interfaces
{
    public interface IReceiptDetailRepository
    {
        public ReceiptDetailReturnVO GetReceiptDetailVO(int id);
        public BasePaginationReturn<ReceiptDetailReturnVO> GetListReceiptDetailVO(ReceiptDetailPaginationParameterVO parameterVO);
        public int PostReceiptDetailVO(ReceiptDetailParameterVO parameterVO);
        public int PutReceiptDetailVO(ReceiptDetailParameterVO parameterVO);
        public int DeleteReceiptDetailVO(int id);
        public List<ReceiptDetailReturnVO> GetListReceiptDetailByReceiptIdVO(int receiptId);
    }
}
