using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using qwer.Services.Base;

namespace qwer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FibonacciController : ControllerBase
    {
        private readonly ILogger<FibonacciController> _logger;
        private readonly IFibonacciService _fibonacciService;

        public FibonacciController(ILogger<FibonacciController> logger, IFibonacciService fibonacciService)
        {
            _logger = logger;
            _fibonacciService = fibonacciService;
        }

        [HttpPost("reverse")]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        { 
            _logger.LogInformation("Request started");

            var data = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                data.AppendLine(reader.ReadLine()); 
            }

            _logger.LogInformation("File parsed to string");

            var result = _fibonacciService.reverseFibonacci(data.ToString());

            _logger.LogInformation("Result");

            return File(GenerateStreamFromString(result), "application/txt", file.FileName);
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
