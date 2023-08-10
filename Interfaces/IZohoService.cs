// ReSharper disable once CheckNamespace

using System.Threading.Tasks;
namespace Zoho.Interfaces
{
    public interface IZohoService
    {
        Task<string> GetOption(string key);
    }
}
