using Business.Exceptions.CommonExceptions;
using Business.Helpers.Extensions;
using Business.HelperServices.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Business.HelperServices.Implementations;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string fileType, int fileSize, string folderName)
    {
        if (!file.CheckFileType(fileType))
            throw new FileTypeException($"Invalid file type. File type must be {fileType}");

        if (!file.CheckFileSize(fileSize))
            throw new FileSizeException($"File size must be less than {fileSize} MB");

        string fileName = $"{Guid.NewGuid()}-{file.FileName}";
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", folderName, fileName);

         using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }
    //public Task DeleteFileAsync(string fileName, string folderName)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<string> GetFileUrlAsync(string fileName, string folderName)
    //{
    //    throw new NotImplementedException();
    //}
}
