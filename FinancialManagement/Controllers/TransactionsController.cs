using FinancialManagement.Entities;
using FinancialManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("Transactions/GetTransaction")]
        public async Task<IActionResult> PostTransaction([FromHeader] int personId)
        {
            var transactions = await _transactionService.GetTransactions(personId);

            return Ok(transactions);
        }

        [HttpPost]
        [Route("Transactions/CreateTransaction")]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionRequestResource request)
        {
            await _transactionService.CreateTransaction(request);

            return Ok();
        }

        [HttpPut]
        [Route("Transactions/UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionRequestResource request)
        {
            await _transactionService.UpdateTransaction(request);

            return Ok();
        }

        [HttpDelete]
        [Route("Transactions/RemoveTransaction")]
        public async Task<IActionResult> DeleteTransaction([FromHeader] int transactionId)
        {
            await _transactionService.DeleteTransaction(transactionId);

            return Ok();
        }
    }
}
