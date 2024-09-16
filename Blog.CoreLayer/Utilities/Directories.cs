using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Utilities
{
    public static class Directories
    {
        public const string PostImage = "wwwroot/Images/Posts/";
        public const string PostContentImage = "wwwroot/Images/Posts/Content/";
        public static string GetPostImage(string imageName) 
            => $"{PostImage.Replace("wwwroot", "")}{imageName}" ;
        public static string GetPostContentImage(string imageName)
            => $"{PostContentImage.Replace("wwwroot", "")}{imageName}";
    }
}
