using Nasa.Business.Data;
using System.Threading.Tasks;

namespace Nasa.Business.Repositories.Configuration
{
    public interface IConfigurationRepository
    {
        Task SetConfig<T>(T configItem, string groupCode);
        Task<T> GetConfig<T>(string groupCode);
    }
}
