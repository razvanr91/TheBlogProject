using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Services
{
    public class ImageService : IImageService
    {
        public string ContentType(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public string DecodeImage(byte[] imageData, string type)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> EncodeImageAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            throw new NotImplementedException();
        }
    }
}
