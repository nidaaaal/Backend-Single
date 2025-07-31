

using Microsoft.AspNetCore.Http;

namespace Emocare.Domain.Interfaces.Extension
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile file, string folder);
        Task<bool> DeleteImageAsync(string publicId);

    }
}
