using Microsoft.AspNetCore.Http;

namespace Business.HelperServices.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string fileType , int fileSize, string baseFolder, string folderName);
    string DeleteCompanyImageAsync(string fileName);
    //Task DeleteFileAsync(string fileName, string folderName);
    //Task<string> GetFileUrlAsync(string fileName, string folderName);
}
