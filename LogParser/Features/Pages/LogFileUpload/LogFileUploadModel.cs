using System;

namespace LogParser.Features.Pages.LogFileUpload
{
    public class LogFileUploadModel
    {
        public string UploadedFileName { get; set; }
        public DateTimeOffset UploadedFileLastModifiedDate { get; set; }
        public long UploadedFileSize { get; set; }

    }
}