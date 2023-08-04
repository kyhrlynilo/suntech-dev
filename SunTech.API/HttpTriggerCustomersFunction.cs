using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SunTech.Infrastructure.Services.CosmosDb;
using SunTech.Infrastructure.Services.AzureEventGrid;
using SunTech.Application.Customers.Commands;
using SunTech.Application.EventHandlers;
using SunTech.API.Models;
using SunTech.Application.Customers.Queries;
using SunTech.Application.CustomersSummary.Queries;
using SunTech.Infrastructure.Services.CustomHttpClient;
using System;

namespace SunTech.API
{
    public class HttpTriggerCustomersFunction
    {

        private readonly ICustomerEventHandler _ceHandler;
        private readonly ICustomerEventBroker _ceBroker;

        public HttpTriggerCustomersFunction(
            ICustomerEventHandler ceHandler,
            ICustomerEventBroker cebroker
            )
        {
            _ceBroker = cebroker;
            _ceHandler = ceHandler;
            _ceHandler.Subscribe(cebroker);
        }

        [FunctionName("Customers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "delete", "put", Route = null)] HttpRequest req,
            ILogger log)
        {

            try
            {
                string method = req.Method;

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                Customer request = request = JsonConvert.DeserializeObject<Customer>(requestBody); ;

                switch (method)
                {
                    case "POST":
                        new CreateCustomerCommand(_ceBroker, request.FirstName, request.LastName, request.Birthday, request.Email);
                        break;

                    case "PUT":
                        new UpdateCustomerCommand(_ceBroker, request.id, request.FirstName, request.LastName, request.Birthday, request.Email);
                        break;

                    case "DELETE":
                        new DeleteCustomerCommand(_ceBroker, request.id);
                        break;

                    case "GET":

                        if (req.Query != null && req.Query.Count > 0)
                        {
                            if (req.Query.ContainsKey("id"))
                            {
                                new GetCustomerQuery(_ceBroker, req.Query["id"]);
                                return new OkObjectResult(_ceHandler.GetCustomer());
                            }

                            if (req.Query.ContainsKey("count"))
                            {
                                new GetCustomerCountQuery(_ceBroker);
                                return new OkObjectResult(_ceHandler.GetCount());
                            }
                        }

                        new GetCustomersQuery(_ceBroker);
                        return new OkObjectResult(_ceHandler.GetCustomers());


                    default:
                        break;
                }


                return new OkObjectResult(method);
            
            } 
            catch(Exception)
            {
                return new BadRequestObjectResult("Sorry for inconvenience. An uncaught error has occured and needs time fixing.");
            }
            
        }

    }
}
