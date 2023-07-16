using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SunTech.API;
using SunTech.Application.EventHandlers;
using SunTech.Infrastructure.Services.AzureEventGrid;
using SunTech.Infrastructure.Services.CosmosDb;
using SunTech.Infrastructure.Services.CustomHttpClient;
using System;
using System.Collections.Generic;
using System.Text;


[assembly: WebJobsStartup(typeof(Startup))]
namespace SunTech.API
{

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            string cosmosDbUri = "https://suntech-cosmos-dev.documents.azure.com:443/";
            string cosmosDbKey = "Zdj01Tq9btaloeaMdM8uXbRxWrssZyJY3ktpNDkWJ24TjTEwuIySKn609AygXMCfSK0zbf5glq41ACDbEEBFXw==";

            string dbName = "suntech_db";
            string containerName = "customers";
            string customerSummaryContainerName = "customers_summary";

            string listenerApiUri = "http://localhost:7071/api/Function1";


            builder.Services.AddScoped<ICosmosDbService>(x => new CosmosDbService(cosmosDbUri, cosmosDbKey));
            builder.Services.AddScoped<ICustomHttpClientService>(x => new CustomHttpClientService(listenerApiUri));
            builder.Services.AddScoped<ICustomerEventBroker>(x => new CustomerEventBroker());
            builder.Services.AddScoped<ICustomerEventHandler>(
                x => new CustomerEventHandler(
                    x.GetRequiredService<ICustomerEventBroker>(),
                    x.GetRequiredService<ICosmosDbService>(),
                    x.GetRequiredService<ICustomHttpClientService>(),
                    dbName,
                    containerName,
                    customerSummaryContainerName
                 )
            );

            var assembly = AppDomain.CurrentDomain.Load("SunTech.Application");

        }
    }
}
