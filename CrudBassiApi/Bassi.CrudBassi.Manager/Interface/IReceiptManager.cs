using Bassi.Api.Model;
using Bassi.CrudBassi.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.CrudBassi.Manager.Interface
{
    public interface IReceiptManager
    {
        public ManagerResponse<ReceiptReturnDTO> GetReceiptDTO(int id)
;
        public ManagerResponse<BasePaginationReturn<ReceiptReturnDTO>> GetListReceiptDTO(ReceiptPaginationParameterDTO parameterDTO)
;
        public ManagerResponse<int> PostReceiptDTO(ReceiptParameterDTO parameterDTO)
;
        public ManagerResponse<int> PutReceiptDTO(ReceiptParameterDTO parameterDTO)
;
        public ManagerResponse<int> DeleteReceiptDTO(int id)
;
    }
}
