using Market.Application.Common.File;
using Microsoft.AspNetCore.Http;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace Market.Infrastructure.Configurations.File;
public class UploadFile : IUploadFile
{
    private readonly ImgurClient imgurClient;
    private readonly ImageEndpoint imageEndpoint;

    public UploadFile()
    {
        imgurClient = new("54c615e991c2f1e");
        imageEndpoint = new(imgurClient);
    }

    public byte[] ReadFileBytes(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadImageToImgur(IFormFile imageFile)
    {
        byte[] imageData;
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();
        }
        IImage image;
        try
        {
            image = await imageEndpoint.UploadImageBinaryAsync(imageData);
        }
        catch (Exception ex)
        {
            return "Upload failed: " + ex.Message;
        }
        return image.Link;
    }
}
