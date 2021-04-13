using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nasa.Business.Services.Commands;
using Nasa.Business.Services.FileHandlers;
using Nasa.Model.Common;
using Nasa.Model.Movements;

namespace Nasa.WebApi.Controllers
{
    public class MovementController : BaseController
    {
        private readonly IFileHandlerService _fileHandlerService;
        private readonly ICommandService _commandService;
        public MovementController(IFileHandlerService fileHandlerService, ICommandService commandService)
        {
            _fileHandlerService = fileHandlerService;
            _commandService = commandService;
        }
        [HttpPost("handlefile"), DisableRequestSizeLimit]
        public async Task<MovementResult> HandleFile()
        {
            var file = Request.Form.Files[0];
            var fileName = await _fileHandlerService.SaveFile(file);
            var commands = await _fileHandlerService.GetCommandLines(fileName);
            var result = await _commandService.MoveAll(commands);
            return result;
        }
    }
}
