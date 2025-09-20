using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOnlineLibrary.Application.Interfaces;

namespace MiniOnlineLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _tx;
        public TransactionsController(ITransactionService tx) { _tx = tx; }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _tx.GetAllAsync());


        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue() => Ok(await _tx.GetOverdueAsync());
    }
}
