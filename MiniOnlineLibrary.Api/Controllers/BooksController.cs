using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOnlineLibrary.Application.DTO;
using MiniOnlineLibrary.Application.Interfaces;

namespace MiniOnlineLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _books;
        private readonly ITransactionService _txs;
        public BooksController(IBookService books, ITransactionService txs) { _books = books; _txs = txs; }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateBookDto dto) => Ok(await _books.CreateAsync(dto));


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _books.GetAllAsync());


        [HttpGet("available")]
        [AllowAnonymous]
        public async Task<IActionResult> Available() => Ok(await _books.GetAvailableAsync());


        [HttpPost("borrow")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Borrow(BorrowRequestDto dto)
        {
            try { return Ok(await _txs.BorrowAsync(dto)); }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }


        [HttpPost("return")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Return(ReturnRequestDto dto)
        {
            try { return Ok(await _txs.ReturnAsync(dto)); }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }


        [HttpGet("borrowed/{userId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Borrowed(int userId) => Ok(await _txs.GetBorrowedByUserAsync(userId));
    }
}
