using System.Security.Claims;
using AutoMapper;
using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Models.Category;
using Blog.CoreLayer.Models.Post;
using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    //[Authorize(policy: "AdminPolicy")]
    //[Authorize(Roles = "Admin,User")]
    public class PostController : AdminControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        public IActionResult Index(int pageId=1,string title="" , string categorySlug="")
        {
            var model = _postService.GetPostsByFilter(new PostFilterParams()
            {
                PageId = pageId,
                Take = 2,
                Title = title,
                CategorySlug = categorySlug

            });
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var post = _mapper.Map<CreatePostDto>(viewModel);

            if (User == null)
            {
                ModelState.AddModelError("Title", "کاربر پیدا نشد");
                return View();
            }

            post.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);//find user id

            var result = _postService.CreatePost(post);

            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("Slug", result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }

            var postViewModel = _mapper.Map<EditPostViewModel>(post);
            return View(postViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var postDto = _mapper.Map<EditPostDto>(viewModel);
            var result = _postService.EditPost(postDto);

            if (result.Status == OperationResultStatus.NotFound ||
                result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError(nameof(viewModel.Title), result.Message);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }
    }
}
