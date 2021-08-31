using System.Collections.Generic;

namespace LogParser.Configuration
{
    public class FileManagementSettings
    {
        public string TemporaryFileStoragePath { get; set; }
        public string TemporaryFilePath { get; set; }
        public string MaxAllowedFileSize { get; set; }
        public List<string> AllowedFileTypes { get; set; }
    }
}