using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Emocare.Domain.Interfaces.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Emocare.Infrastructure.Extensions.Integrations.CloudinaryExtension
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly ICloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;
        public CloudinaryService(ICloudinary cloudinary, ILogger<CloudinaryService> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }
        public async Task<string> UploadImage(IFormFile file, string folder)
        {
            await using var stream = file.OpenReadStream();
            var uploadparams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadparams);
            if (uploadResult.Error != null)
            {
                _logger.LogError(uploadResult.Error.Message);
                throw new Exception("Image upload failed");

            }
            return uploadResult.SecureUrl.ToString();
        }
        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            var publicId = GetPublicIdFromUrl(imageUrl);
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }
        private string GetPublicIdFromUrl(string url)
        {
            var match = Regex.Match(url, @"upload\/(?:v\d+\/)?([^\.]+)");
            if (!match.Success)
                throw new ArgumentException("Invalid Cloudinary URL");

            return match.Groups[1].Value.Trim('/');
        }
    }
}
