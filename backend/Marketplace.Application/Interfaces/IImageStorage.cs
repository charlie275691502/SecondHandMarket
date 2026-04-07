using Microsoft.AspNetCore.Http;

namespace Marketplace.Application.Interfaces;

public interface IImageStorage
{
    Task<string> UploadImageAsync(IFormFile file);
}

