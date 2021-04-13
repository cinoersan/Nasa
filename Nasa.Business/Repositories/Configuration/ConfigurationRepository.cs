    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nasa.Business.Data;
using Nasa.Business.Models;
using Nasa.Model.Config;

namespace Nasa.Business.Repositories.Configuration
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly NasaDbContext _context;

        public ConfigurationRepository(NasaDbContext context)
        {
            _context = context;
        }

        public async Task SetConfig<T>(T configItem, string groupCode)
        {
            var value = JsonSerializer.Serialize(configItem);
            var item = _context.All<NasaConfig>().FirstOrDefault(t => t.GroupCode.Equals(groupCode));
            if (item == null)
            {
                item = new NasaConfig
                {
                    GroupCode = groupCode,
                    Value = value
                };
                await _context.AddAsync(item);
            }
            else
                item.Value = value;

            await _context.SaveChangesAsync();

        }
        public async Task<T> GetConfig<T>(string groupCode)
        {
            var item = await _context.All<NasaConfig>().FirstOrDefaultAsync(t => t.GroupCode.Equals(groupCode));
            return item == null ? default : JsonSerializer.Deserialize<T>(item.Value);
        }
    }
}
