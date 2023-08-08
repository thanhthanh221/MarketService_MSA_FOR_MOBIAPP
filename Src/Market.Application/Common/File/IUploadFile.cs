using Microsoft.AspNetCore.Http;

namespace Market.Application.Common.File;
public interface IUploadFile
{
    Task<string> UploadImageToImgur(IFormFile imageFile);
    byte[] ReadFileBytes(IFormFile file);
}
