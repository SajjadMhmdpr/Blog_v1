using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog.CoreLayer.Utilities
{
    public static class FileManager
    {
        public static string SaveImage(this IFormFile file, string savePath)
        {
            if (file == null)
            {
                throw new Exception("file is null");
            }

            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),
                savePath.Replace("/", "\\"));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath+fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);

            file.CopyTo(stream);

            return fileName;

        }

        public static void DeleteImage(this string imageName , string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path , imageName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

        }
    }
}
