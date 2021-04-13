using System.Threading.Tasks;
using Nasa.Business.Data;
using Nasa.Business.Repositories.Configuration;
using Nasa.Model.Config;

namespace Nasa.Business.Repositories.Movements
{
    public class MovementRepository: IMovementRepository
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly NasaDbContext _context;
        public MovementRepository(IConfigurationRepository configurationRepository, NasaDbContext context)
        {
            _configurationRepository = configurationRepository;
            _context = context;
        }

        
    }
}
