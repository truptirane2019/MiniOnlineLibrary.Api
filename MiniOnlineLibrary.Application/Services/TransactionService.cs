using MiniOnlineLibrar.Infrastructure.Repositories;
using MiniOnlineLibrary.Application.DTO;
using MiniOnlineLibrary.Application.Interfaces;
using MiniOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _uow;
        private readonly TimeSpan _borrowPeriod = TimeSpan.FromDays(2);


        public TransactionService(IUnitOfWork uow) { _uow = uow; }


        public async Task<TransactionDto> BorrowAsync(BorrowRequestDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(dto.UserId) ?? throw new Exception("User not found");
            var book = await _uow.Books.GetByIdAsync(dto.BookId) ?? throw new Exception("Book not found");


            // enforce max 2 active borrows
            var userTx = (await _uow.Transactions.GetAllAsync()).Where(t => t.UserId == dto.UserId && t.Status == BorrowStatus.Borrowed).ToList();
            if (userTx.Count >= 2) throw new Exception("User has reached maximum active borrows (2)");


            // ensure book not already borrowed
            var activeForBook = (await _uow.Transactions.GetAllAsync()).Any(t => t.BookId == dto.BookId && t.Status == BorrowStatus.Borrowed);
            if (activeForBook) throw new Exception("Book already borrowed");


            var tx = new BorrowTransaction { UserId = dto.UserId, BookId = dto.BookId, BorrowDate = DateTime.UtcNow, Status = BorrowStatus.Borrowed };
            await _uow.Transactions.AddAsync(tx);
            await _uow.SaveChangesAsync();


            return new TransactionDto(tx.TransactionId, tx.UserId, "","",tx.BookId,"", tx.BorrowDate, tx.ReturnDate, tx.Status);
        }


        public async Task<TransactionDto> ReturnAsync(ReturnRequestDto dto)
        {
            var tx = await _uow.Transactions.GetByIdAsync(dto.TransactionId) ?? throw new Exception("Transaction not found");
            if (tx.Status != BorrowStatus.Borrowed && tx.Status != BorrowStatus.Overdue) throw new Exception("Transaction is not currently borrowed");
            tx.ReturnDate = DateTime.UtcNow;
            tx.Status = BorrowStatus.Returned;
            _uow.Transactions.Update(tx);
            await _uow.SaveChangesAsync();
            return new TransactionDto(tx.TransactionId, tx.UserId, "","" , tx.BookId, "", tx.BorrowDate, tx.ReturnDate, tx.Status);
        }


        public async Task<IEnumerable<TransactionDto>> GetBorrowedByUserAsync(int userId)
        {
            var txs = (await _uow.Transactions.GetAllAsync("User,Book")).Where(t => t.UserId == userId).ToList();
            // compute overdue dynamically
            var list = txs.Select(t =>
            {
                var status = t.Status;
                if (status == BorrowStatus.Borrowed && t.BorrowDate + _borrowPeriod < DateTime.UtcNow)
                {
                    status = BorrowStatus.Overdue;
                }
                return new TransactionDto(t.TransactionId, t.UserId, t.User.Name,t.User.Email, t.BookId,t.Book.Title, t.BorrowDate, t.ReturnDate, status);
            });
            return list;
        }


        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var txs = await _uow.Transactions.GetAllAsync("User,Book");
             

            return txs.Select(t => new TransactionDto(
                t.TransactionId,
                t.UserId,
                t.User?.Name,
                t.User?.Email,
                t.BookId,
                t.Book?.Title,
                t.BorrowDate,
                t.ReturnDate,
                t.Status 


            ));
        }


        public async Task<IEnumerable<TransactionDto>> GetOverdueAsync()
        {
            var txs = (await _uow.Transactions.GetAllAsync()).Where(t => t.Status == BorrowStatus.Borrowed && t.BorrowDate + _borrowPeriod < DateTime.UtcNow);
            return txs.Select(t => new TransactionDto(t.TransactionId, t.UserId, t.User.Name, t.User.Email, t.BookId, t.Book.Title, t.BorrowDate, t.ReturnDate, BorrowStatus.Overdue));
        }
    }
}
