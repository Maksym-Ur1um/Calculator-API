using CalculatorAPI.DTO;
using CalculatorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
    
        private readonly ICalculateService calculateService;
        public HomeController( ICalculateService calculateService)
        {
            this.calculateService = calculateService;
        }

        [Route("/api/calculate")]
        [HttpPost]
        public IActionResult Index([FromBody]CalculationRequest calculationRequest)
        {
            double result = calculateService.Calculate(calculationRequest.Expression);
            try
            {
                return Ok(result);
            } catch
            {
                return BadRequest("InvalidExpression");
            }
        }
    }
}
