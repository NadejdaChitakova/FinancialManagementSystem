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
        [Route("Transactions/GetPDFTransactions")]
        public async Task ExportTransactionsToPdf()
        {
            var pdf = _transactionService.ExportTransactionsToPdf();
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

        [HttpGet]
        [Route("Transactions/TransactionsHistory")]
        public async Task<IActionResult> GetTransactionsByDate([FromHeader] DateTime fromDate, DateTime toDate)
        {
            var transactions = await _transactionService.GetTransactionsByDate(fromDate, toDate);
            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/TransactionsByCategory")]
        public async Task<IActionResult> GetTransactionsByCategory()
        {
            var transactionByCategory = _transactionService.GetTransactionsByCategories();
            return Ok(transactionByCategory);
        }

        [HttpGet]
        [Route("Transactions/TransactionsByType")]
        public async Task<IActionResult> GetTransactionsByType([FromHeader] int typeId)
        {
            var transactions = _transactionService.GetTransactionsByType(typeId);
            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/MonthlyTransactionSummary")]
        public async Task<IActionResult> MonthlyTransactionSummary([FromHeader] int month)
        {
            var transactions = _transactionService.GetMonthlyTransactionSummary(month);
            return Ok(transactions);
        }

        [HttpGet]
        [Route("Transactions/TransactionsPrediction")]
        public async Task<IActionResult> TransactionsPrediction()
        {
            var transactions = _transactionService.GetTransactionsPrediction();
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
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionUpdateRequestResource request)
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
