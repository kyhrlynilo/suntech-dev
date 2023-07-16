using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SunTech.Infrastructure.Services.AzureEventGrid;
using SunTech.API.Listener.Models;

namespace SunTech.API.Listener
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CustomerEventGridMessage request = JsonConvert.DeserializeObject<CustomerEventGridMessage>(requestBody);
         
            string uri = "https://suntech-eventgrid-dev.southeastasia-1.eventgrid.azure.net/api/events";
            string key = "jFXAEyrotYY8k1qT+btCFOvrLKHIneg6I7chqc3hBB0=";

            IAzureEventGridService _eventService = new AzureEventGridService(uri, key);

            await _eventService.PublishCustomerEvent(request.Subject, request.Verb, request.Data);

            return new OkObjectResult("");
        }
    }
}
