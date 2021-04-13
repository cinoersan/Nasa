using Microsoft.AspNetCore.Mvc;
using Nasa.Business.Attributes;

namespace Nasa.WebApi.Controllers
{
    [ApiValidadtionFilter]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController: ControllerBase
    {
        
    }
}
