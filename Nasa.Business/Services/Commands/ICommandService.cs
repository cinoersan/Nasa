using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nasa.Model.Movements;

namespace Nasa.Business.Services.Commands
{
    public interface ICommandService
    {
        Task<MovementResult> MoveAll(List<string> movements);
    }
}
