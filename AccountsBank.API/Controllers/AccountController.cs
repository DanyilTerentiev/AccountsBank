using AccountsBank.BLL.DTOs;
using AccountsBank.BLL.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace AccountsBank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    // [ProducesResponseType()]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        try
        {
            var results = await _accountService.GetAllAsync();
            return Ok(results);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAccountAsync(AccountDTO account)
    {
        try
        {
            var res = await _accountService.InsertAsync(account);
            return Ok(res);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id)
    {
        try
        {
            var result = await _accountService.GetByIdAsync(id);
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
            await _accountService.DeleteAsync(id);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(AccountDTO account)
    {
        try
        {
            var res = await _accountService.UpdateAsync(account);
            return Ok(res);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    [HttpGet("userId/{userId}")]
    public async Task<IActionResult> GetByUserIdAsync([FromRoute]Guid userId)
    {
        var res = await _accountService.GetAccountsByUserIdAsync(userId);
        return Ok(res);
    }
}