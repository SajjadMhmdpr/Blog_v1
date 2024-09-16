using Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        [Route("/Upload/Article")]
        public IActionResult UploadArticleImage(IFormFile upload)
        {
            if (upload==null)
            {
                return BadRequest();
            }

            var imageName = upload.SaveImage(Directories.PostContentImage);

            return new JsonResult(new
            {
                Uploaded=true,
                url = upload.SaveImage(Directories.GetPostContentImage(imageName))
            });
        }
    }
}
