using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase{
        

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger){
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber){
            if (isNumber(firstNumber) && isNumber(secondNumber)){
                var sum = toNumber(firstNumber) + toNumber(secondNumber);
                return Ok(sum.ToString());

            }
            return BadRequest("Invalid Input"); //when input is not valid
        }

        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public IActionResult Sub(string firstNumber, string secondNumber)
        {
            if (isNumber(firstNumber) && isNumber(secondNumber))
            {
                var sub = toNumber(firstNumber) - toNumber(secondNumber);
                return Ok(sub.ToString());

            }
            return BadRequest("Invalid Input"); //when input is not valid
        }

        [HttpGet("div/{firstNumber}/{secondNumber}")]
        public IActionResult Div(string firstNumber, string secondNumber)
        {
            if (isNumber(firstNumber) && isNumber(secondNumber))
            {
                var div = toNumber(firstNumber) / toNumber(secondNumber);
                return Ok(div.ToString());

            }
            return BadRequest("Invalid Input"); //when input is not valid
        }

        [HttpGet("mult/{firstNumber}/{secondNumber}")]
        public IActionResult Mult(string firstNumber, string secondNumber)
        {
            if (isNumber(firstNumber) && isNumber(secondNumber))
            {
                var mult = toNumber(firstNumber) * toNumber(secondNumber);
                return Ok(mult.ToString());

            }
            return BadRequest("Invalid Input"); //when input is not valid
        }

        [HttpGet("sqrt/{firstNumber}")]
        public IActionResult Sqrt(string firstNumber, string secondNumber)
        {
            if (isNumber(firstNumber))
            {
                var sqrt = Math.Sqrt((double)toNumber(firstNumber));
                return Ok(sqrt.ToString());

            }
            return BadRequest("Invalid Input"); //when input is not valid
        }


        private bool isNumber(string strNumber){
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number
            ); //saber se eh . ou ,
            return isNumber;
        }

        private decimal toNumber(string strNumber){
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue)){
                return decimalValue;
            }
            return 0;
            
        }

        
    }
}
