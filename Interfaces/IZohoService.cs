using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Zoho.Interfaces
{
    public interface IZohoService
    {
        Task<string> GetOption(string key);
    }
}