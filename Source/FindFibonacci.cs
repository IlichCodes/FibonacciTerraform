using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class FindFibonacci
    {
        [FunctionName("FindFibonacci")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "FindFibonacci/{inputNumber}")] HttpRequest req, int inputNumber,
            ILogger log)
        {            
            if(inputNumber <= 0 || int.TryParse(inputNumber, out result))
            {
                return new OkObjectResult("Please submit valid number");         
            }
            int firstNumber = 0;
            int secondNumber = 1;
            int nextFibonacciNo = firstNumber + secondNumber;
            while (nextFibonacciNo <= inputNumber)
            {
                firstNumber = secondNumber;
                secondNumber = nextFibonacciNo;
                nextFibonacciNo = firstNumber + nextFibonacciNo;
            }
            int closestNumber = nextFibonacciNo - inputNumber < inputNumber - secondNumber ? nextFibonacciNo : secondNumber;
            if (closestNumber == secondNumber)
            {
                return new OkObjectResult(new int[] { closestNumber, firstNumber, nextFibonacciNo });               
            }
            return new OkObjectResult(new int[] { closestNumber, secondNumber, nextFibonacciNo + secondNumber}); 
        }
    }
}

