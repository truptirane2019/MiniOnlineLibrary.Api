using Microsoft.EntityFrameworkCore;
using MiniOnlineLibrar.Infrastructure;
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
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;
        public BookService(IUnitOfWork uow) { _uow = uow; }


        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book { Title = dto.Title, Author = dto.Author, Description = dto.Description, FilePath = dto.FilePath };
            await _uow.Books.AddAsync(book);
            await _uow.SaveChangesAsync();
            return new BookDto(book.BookId, book.Title, book.Author, book.Description, book.FilePath, book.CreatedAt);
        }


        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _uow.Books.GetAllAsync();
            return books.Select(b => new BookDto(b.BookId, b.Title, b.Author, b.Description, b.FilePath, b.CreatedAt));
        }


        public async Task<IEnumerable<BookDto>> GetAvailableAsync()
        {
            // A book is available if it has no active Borrowed transaction (status Borrowed without ReturnDate)
            var allBooks = (await _uow.Books.GetAllAsync()).ToList();
            var txs = (await _uow.Transactions.GetAllAsync()).Where(t => t.Status == BorrowStatus.Borrowed).ToList();
            var borrowedBookIds = txs.Select(t => t.BookId).ToHashSet();
            var available = allBooks.Where(b => !borrowedBookIds.Contains(b.BookId));
            return available.Select(b => new BookDto(b.BookId, b.Title, b.Author, b.Description, b.FilePath, b.CreatedAt));
        }
    }
}
