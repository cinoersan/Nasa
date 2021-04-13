using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using CsvHelper.Configuration;
using Nasa.Business.Exceptions;

namespace Nasa.Business.Services.FileHandlers
{
    public class FileHandlerService : IFileHandlerService
    {
        public async Task<string> SaveFile(IFormFile file)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Resources", "MovementFiles"));
            if (file.Length == 0)
                throw new BadRequestException("File not found!");

            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName) ?? string.Empty;
            if (!extension.Equals(".csv"))
                throw new BadRequestException("File should be in csv format");
            var fullPath = Path.Combine(pathToSave, $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}");
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fullPath;
        }
        public async Task<List<string>> GetCommandLines(string fileName)
        {
            using TextReader reader = File.OpenText(@fileName);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            var csv = new CsvReader(reader, config);
            var commands = new List<string>();

            while (await csv.ReadAsync())
            {
                commands.Add(csv.GetField(0));
            }

            return commands;
        }
    }
}
