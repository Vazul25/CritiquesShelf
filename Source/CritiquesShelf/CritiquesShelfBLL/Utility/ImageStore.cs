using System;
using System.IO;
using Microsoft.Extensions.Configuration;
 

namespace CritiquesShelfBLL.Utility
{
    public class ImageStore
    {
        private readonly IConfiguration _configuration;
        private readonly string BasePath;

        public ImageStore(IConfiguration configuration) {
            _configuration = configuration;

            BasePath = _configuration["ImagePath"];
        }

        public string SaveImage(byte[] image)
        {
            var guid = Guid.NewGuid().ToString();

            File.WriteAllBytes($"{BasePath}{guid}" ,image);

            return guid;
        }

        public byte[] GetImage(string guid)
        {
            return File.ReadAllBytes($"{BasePath}{guid}");
        }
    }
}
