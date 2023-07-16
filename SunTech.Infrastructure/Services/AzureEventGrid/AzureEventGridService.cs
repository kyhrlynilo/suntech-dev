using Azure.Messaging.EventGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SunTech.Infrastructure.Services.AzureEventGrid
{
    public class AzureEventGridService : IAzureEventGridService
    {
        private readonly EventGridPublisherClient _publisherClient;

        public AzureEventGridService(string endpoint, string accessKey)
        {
            _publisherClient = new EventGridPublisherClient(new Uri(endpoint), new Azure.AzureKeyCredential(accessKey));
        }

        public async Task PublishCustomerEvent(string subject, string verb, dynamic jsonStr)
        {

            EventGridEvent eventGridEvent = new EventGridEvent(subject, verb, "1.0", JsonConvert.SerializeObject(jsonStr));
            eventGridEvent.Topic = "customers";

            await _publisherClient.SendEventAsync(eventGridEvent);
        }

    }
}
