using Microsoft.AspNetCore.Mvc;
using ChangeCalculatorApi.Models;

namespace ChangeCalculatorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeController : ControllerBase
    {
        private readonly decimal[] denominations = new decimal[]
        {
            200, 100, 50, 20, 10, 5, 2, 1, 0.5m, 0.2m, 0.1m
        };

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] ChangeRequest request)
        {
            // Validate null
            if (request == null)
                return BadRequest(new { error = "Request body is required." });

            // Validate negative
            if (request.Amount < 0)
                return BadRequest(new { error = "Amount cannot be negative." });

            // Validate zero
            if (request.Amount == 0)
                return BadRequest(new { error = "Amount must be greater than zero." });

            // Round to 2 decimals (cents)
            decimal amount = Math.Round(request.Amount, 2);

            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var denom in denominations)
            {
                int count = (int)(amount / denom);

                if (denom >= 1)
                    result.Add($"R{denom}", count);
                else
                    result.Add($"{denom * 100}c", count);

                amount = Math.Round(amount % denom, 2);
            }

            return Ok(result);
        }
    }
}
