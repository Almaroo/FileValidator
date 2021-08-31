using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogParser.Configuration;
using LogParser.Helpers;
using LogParser.Interfaces;
using LogParser.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;

namespace LogParser.Features.Pages.LogFileUpload
{
    public class LogFileUploadViewModel : ViewModelBase, ILogFileUploadViewModel
    {
        private readonly IFileManagementService _fileManagementService;
        private readonly LogFileParserService _fileParser;
        private readonly FileManagementSettings _fileManagementSettings;

        private string _tmpFileName;

        public LogFileUploadViewModel(IFileManagementService fileManagementService, LogFileParserService fileParser, IOptions<FileManagementSettings> fileManagementSettings)
        {
            _fileManagementService = fileManagementService;
            _fileParser = fileParser;
            _fileManagementSettings = fileManagementSettings.Value;
        }

        private bool _isValidated;
        public bool IsValidated
        {
            get => _isValidated;
            private set => SetValue(ref _isValidated, value);
        }
        public int FileMaxSize => int.Parse(_fileManagementSettings.MaxAllowedFileSize);
        public List<string> AllowedFileTypes => _fileManagementSettings.AllowedFileTypes;

        private LogFileUploadModel _uploadedFile;
        public LogFileUploadModel UploadedFile
        {
            get => _uploadedFile;
            set => SetValue(ref _uploadedFile, value);
        }

        private List<string> _validationErrors = new();
        public List<string> ValidationErrors
        {
            get => _validationErrors;
            private set => SetValue(ref _validationErrors, value);
        }

        public async Task ValidateStoredFile()
        {
            IsBusy = true;
            
            using var file = _fileManagementService.OpenFileFromDisk(_tmpFileName);

            if (file == null) return;
            
            await _fileParser.ParseFile(file);
            file.Close();
            
            IsValidated = true;
            ValidationErrors = _fileParser.ParsingErrorMessages;
            _fileManagementService.DeleteFile(_tmpFileName);
            IsBusy = false;
        }
        public async Task StoreUploadedFile(IBrowserFile file)
        {
            IsBusy = true;
            if (!string.IsNullOrEmpty(_tmpFileName))
            {
                _fileManagementService.DeleteFile(_tmpFileName);
            }
            _tmpFileName = $"tmp_{DateTime.Now:yy_MM_ddThh_mm_ss}";
            await _fileManagementService.SaveUploadedFileToDiskAsync(file, _tmpFileName);
            IsBusy = false;
        }
        public void Dispose()
        {
            if (string.IsNullOrEmpty(_tmpFileName))
            {
                return;
            }
            
            _fileManagementService.DeleteFile(_tmpFileName);
        }
    }
}