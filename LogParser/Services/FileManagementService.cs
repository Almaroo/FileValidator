using System;
using System.IO;
using System.Threading.Tasks;
using LogParser.Configuration;
using LogParser.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace LogParser.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly FileManagementSettings _fileManagementSettings;
        private readonly ILogger<FileManagementService> _logger;

        public FileManagementService(IOptions<FileManagementSettings> fileManagementSettings, ILogger<FileManagementService> logger)
        {
            _logger = logger;
            _fileManagementSettings = fileManagementSettings.Value;
        }

        public async Task SaveUploadedFileToDiskAsync(IBrowserFile file, string fileName)
        {

            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(),
                _fileManagementSettings.TemporaryFileStoragePath));
            
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    string.Format(_fileManagementSettings.TemporaryFilePath, fileName));
                await using FileStream fs = new(path, FileMode.Create);
                await file.OpenReadStream(int.Parse(_fileManagementSettings.MaxAllowedFileSize)).CopyToAsync(fs);
            }
            catch (Exception ex)
            {
               _logger.LogCritical("Failed to store file: {0}", ex.Message);
            }
        }

        public StreamReader OpenFileFromDisk(string fileName)
        {
            try
            {
                return new(Path.Combine(Directory.GetCurrentDirectory(),
                    string.Format(_fileManagementSettings.TemporaryFilePath, fileName)));
            }
            catch (Exception ex)
            {
               _logger.LogCritical("Failed to store file: {0}", ex.Message);
            }

            return null;
        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), string.Format(_fileManagementSettings.TemporaryFilePath, fileName));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}