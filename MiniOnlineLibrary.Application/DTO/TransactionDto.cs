using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.DTO
{
    public record BorrowRequestDto(int BookId, int UserId);
    public record ReturnRequestDto(int TransactionId);
    public record TransactionDto(int TransactionId, int UserId, int BookId, DateTime BorrowDate, DateTime? ReturnDate, string Status);
 
  
}
