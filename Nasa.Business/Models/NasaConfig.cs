using Microsoft.EntityFrameworkCore;

namespace Nasa.Business.Models
{
    [Index(nameof(GroupCode))]
    public class NasaConfig: BaseEntity
    {
        public string GroupCode { get; set; }
        public string Value { get; set; }
    }
}
