using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.DTO
{
    public record CreateBookDto(string Title, string Author, string? Description, string? FilePath);
    public record BookDto(int BookId, string Title, string Author, string? Description, string? FilePath, DateTime CreatedAt);
}
