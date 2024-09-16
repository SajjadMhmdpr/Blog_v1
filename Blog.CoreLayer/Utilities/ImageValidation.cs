using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Utilities
{
    public static class ImageValidation
    {
        public static bool Validate(this string imageName)
        {
            var extension = Path.GetExtension(imageName);
            if (extension == null)
                return false;

            return extension.ToLower() == ".png" || extension.ToLower() == ".jpg";
        }

        public static bool Validate(this IFormFile file)
        {
            try
            {
                using var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
