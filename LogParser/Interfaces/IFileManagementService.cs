using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace LogParser.Interfaces
{
    public interface IFileManagementService
    {
        public Task SaveUploadedFileToDiskAsync(IBrowserFile file, string fileName);
        public StreamReader OpenFileFromDisk(string fileName);
        public void DeleteFile(string fileName);
    }
}