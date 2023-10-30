using AccountsBank.BLL.DTOs;
using AccountsBank.BLL.Services.AccountService;
using AccountsBank.BLL.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

namespace AccountsBank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    
    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsAsync()
    {
        try
        {
            var results = await _transactionService.GetAllAsync();
            return Ok(results);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(TransactionDTO model)
    {
        try
        {
            var res = await _transactionService.InsertAsync(model);
            return Ok(res);
        }
        catch(Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync([FromQuery]Guid id)
    {
        try
        {
            var result = await _transactionService.GetByIdAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id)
    {
        try
        {
            await _transactionService.DeleteAsync(id);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(TransactionDTO model)
    {
        try
        {
            var res = await _transactionService.UpdateAsync(model);
            return Ok(res);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpGet("senderId")]
    public async Task<IActionResult> GetBySenderId([FromQuery]Guid senderId)
    {
        var res = await _transactionService.GetBySenderIdAsync(senderId);
        return Ok(res);
    }
    
    [HttpGet("receiverId")]
    public async Task<IActionResult> GetByReceiverId([FromQuery]Guid receiverId)
    {
        var res = await _transactionService.GetByReceiverIdAsync(receiverId);
        return Ok(res);
    }
    
    [HttpGet("limit")]
    public async Task<IActionResult> GetTopFiveAsync([FromQuery] int limit)
    {
        try
        {
            var results = await _transactionService.GetTopFiveAsync(limit);
            return Ok(results);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }
}