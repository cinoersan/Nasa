using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nasa.Business.Services.FileHandlers
{
    public interface IFileHandlerService
    {
        Task<string> SaveFile(IFormFile file);
        Task<List<string>> GetCommandLines(string fileName);
    }
}
