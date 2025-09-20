using MiniOnlineLibrary.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> GetAvailableAsync();
    }
}
