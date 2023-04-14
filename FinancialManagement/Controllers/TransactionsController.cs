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
        public async Task<IActionResult> Get()
        {
            var transactions = await _transactionService.GetTransactions();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/GetTransactionsByLocation")]
        public async Task<IActionResult> GetTransactionsByLocation([FromHeader] int locationId)
        {
            var transactions = await _transactionService.GetTransactionsByLocation(locationId);

            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/GetTransactionsByBank")]
        public async Task<IActionResult> GetTransactionsByBank()
        {
            var transactions = await _transactionService.GetTransactionsByBank();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/PersonTransactions")]
        public async Task<IActionResult> GetPersonTransactions([FromHeader] int personId)
        {
            var transaction = await _transactionService.GetTransactionsByPerson(personId);

            return Ok(transaction);
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
