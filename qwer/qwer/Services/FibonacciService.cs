
using Microsoft.Extensions.Logging;
using qwer.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qwer.Services
{
    public class FibonacciService: IFibonacciService
    {
        private readonly ILogger<FibonacciService> _logger;
        public FibonacciService(ILogger<FibonacciService> logger) =>
            _logger = logger;

        public string reverseFibonacci(string data)
        {
            _logger.LogInformation("Reverse Fibonacci rows");
            List<string> reversed = new();
            List<string> lines = data.Split("\r\n").ToList();

            foreach (string line in lines)
            {
                var array = line.Split(',').Where(n=>!String.IsNullOrWhiteSpace(n)).Select(n => Convert.ToInt32(n.Trim())).ToArray();
                if (IsFibonacci(array))
                {
                    ReverseArray(array);
                }
                string result = string.Join(',', array);

                reversed.Add(result);
            }
            return String.Join("\r\n", reversed);
        }

        private static void ReverseArray(int[] arr)
        {
            for (int i = 0; i < arr.Length - i; i++)
            {
                var value = arr[arr.Length - i - 1];
                arr[arr.Length - i - 1] = arr[i];
                arr[i] = value;
            }
        }

        private static bool IsFibonacci(int[] arr)
        {
            for (int i = 2; i < arr.Length; i++)
            {
                if ((arr[i - 1] + arr[i - 2]) != arr[i])
                    return false;
            }
            return true;
        }
    }
}