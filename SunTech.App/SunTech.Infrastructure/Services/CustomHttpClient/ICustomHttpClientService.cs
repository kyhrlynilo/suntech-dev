using System.Threading.Tasks;

namespace SunTech.Infrastructure.Services.CustomHttpClient
{
    public interface ICustomHttpClientService
    {
        Task SendMessageToEventGrid(string subject, string verb, dynamic data);
    }
}