using System.Threading.Tasks;

namespace SunTech.Infrastructure.Services.AzureEventGrid
{
    public interface IAzureEventGridService
    {
        Task PublishCustomerEvent(string subject, string verb, dynamic jsonStr);
    }
}