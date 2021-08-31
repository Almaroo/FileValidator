using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using LogParser.Features.Pages.LogFileUpload;
using Microsoft.AspNetCore.Components.Forms;

namespace LogParser.Interfaces
{
    public interface ILogFileUploadViewModel : IDisposable
    {
        bool IsBusy { get; set; }
        bool IsValidated { get; }
        public LogFileUploadModel UploadedFile { get; set; }
        public List<string> AllowedFileTypes { get; }
        public int FileMaxSize { get; }
        public List<string> ValidationErrors { get; }
        Task ValidateStoredFile();
        Task StoreUploadedFile(IBrowserFile file);
        event PropertyChangedEventHandler PropertyChanged;
    }
}