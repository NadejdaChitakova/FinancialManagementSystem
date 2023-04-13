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

        [HttpGet]
        [Route("Transactions/GetTransaction")]
        public async Task<IActionResult> Get([FromHeader] int personId)
        {
            var transactions = await _transactionService.GetTransactions(personId);

            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/GetTransactionsByLocation")]
        public async Task<IActionResult> GetTransactionsByLocation([FromHeader] int locationId)
        {
            var transactions = await _transactionService.GetTransactionsByLocation(locationId);

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
