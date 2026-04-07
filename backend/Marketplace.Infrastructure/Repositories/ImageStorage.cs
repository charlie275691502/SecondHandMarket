using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Marketplace.Application.Interfaces;

namespace Marketplace.Infrastructure.Repositories;

public class ImageStorage : IImageStorage
{
    public readonly Cloudinary _cloudinary;

    public ImageStorage(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }

    async Task<string> IImageStorage.UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.SecureUrl.ToString();
    }
}