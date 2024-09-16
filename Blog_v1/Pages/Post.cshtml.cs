using System.ComponentModel.DataAnnotations;
using Blog.CoreLayer.Dtos.Comment;
using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Services.Comments;
using Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public PostDto Post { get; set; }
        public List<CommentDto> Comments{ get; set; }
        public List<PostDto> RelatedPosts{ get; set; }
        [Required]
        [BindProperty]
        public string Text { get; set; }
        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            
            if (Post == null)
                return NotFound();
            _postService.PostVisited(Post.PostId);

            Comments = _commentService.GetComments(Post.PostId);
            RelatedPosts = _postService.GetRelatedPosts(Post.CategoryId);

            return Page();
        }
        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Login"); 
            }
            if(!ModelState.IsValid)
                return Page();

            Post = _postService.GetPostBySlug(slug);

            _commentService.CreateComment(new CreateCommentDto()
            {
                PostId = Post.PostId,
                Text = Text,
                UserId = Post.UserId
            });

            return RedirectToPage("Post", new { slug=slug});
        }
    }
}
