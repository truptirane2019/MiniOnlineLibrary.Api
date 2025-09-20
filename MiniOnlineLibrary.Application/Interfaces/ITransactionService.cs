using MiniOnlineLibrary.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDto> BorrowAsync(BorrowRequestDto dto);
        Task<TransactionDto> ReturnAsync(ReturnRequestDto dto);
        Task<IEnumerable<TransactionDto>> GetBorrowedByUserAsync(int userId);
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<IEnumerable<TransactionDto>> GetOverdueAsync();
    }
}
