using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Services
{
    public interface IImageService
    {
        Task<byte[]> EncodeImageAsync(IFormFile file);

        Task<byte[]> EncodeImageAsync(string fileName);

        string DecodeImage(byte[] imageData, string type);

        string ContentType(IFormFile file);

        int Size();
    }
}
