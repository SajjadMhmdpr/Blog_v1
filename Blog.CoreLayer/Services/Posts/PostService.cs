using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Context;
using Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.CoreLayer.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BlogContext _blogContext;
        private readonly IMapper _mapper;

        public PostService(BlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public OperationResult CreatePost(CreatePostDto dto)
        {
            if (IsSlugExist(dto.Slug))
            {
                return OperationResult.Error("این slug تکراری است");
            }

            var post = _mapper.Map<Post>(dto);

            post.Slug = post.Slug.ToSlug();
            post.IsDelete = false;
            post.CreationDate = DateTime.Now;
            if (!dto.ImageFile.FileName.Validate())
            {
                throw new BadImageFormatException();
            }

            post.ImageName = dto.ImageFile.SaveImage(Directories.PostImage);

            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditPost(EditPostDto dto)
        {
            var post = _blogContext.Posts.FirstOrDefault(c => c.Id == dto.PostId);
            if (post == null)
                return OperationResult.NotFound();
            var oldImageName = post.ImageName;

            if (post.Slug.ToSlug() != dto.Slug.ToSlug())
            {
                if (IsSlugExist(dto.Slug))
                {
                    return OperationResult.Error("این slug تکراری است");
                }
            }
            
            if (dto.ImageFile != null)
            {
                if (!dto.ImageFile.Validate())
                {
                    throw new BadImageFormatException();
                }
                post.ImageName.DeleteImage(Directories.PostImage);
            }


            _blogContext.Posts.Entry(post).State = EntityState.Detached;
            var newPost = _mapper.Map<Post>(dto);
            newPost.ImageName = post.ImageName;
            newPost.UserId = post.UserId;
            newPost.CreationDate = post.CreationDate;
            newPost.Visit = post.Visit;
            newPost.IsDelete= post.IsDelete;
            newPost.Slug = newPost.Slug.ToSlug();

            if (dto.ImageFile != null)
            {
                newPost.ImageName = dto.ImageFile.SaveImage(Directories.PostImage);
            }

            _blogContext.Posts.Update(newPost);
            _blogContext.SaveChanges();

            return OperationResult.Success();
        }

        public PostDto? GetPostById(int postId)
        {
            var post = _blogContext.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return null;

            var dto = _mapper.Map<PostDto>(post);

            return dto;
        }

        public PostDto? GetPostBySlug(string slug)
        {
            var post = _blogContext.Posts
                .Include(p=>p.Category)
                .Include(p=>p.SubCategory)
                .Include(p=>p.User)
                .FirstOrDefault(p => p.Slug == slug);

            if (post == null)
                return null;

            var dto = _mapper.Map<PostDto>(post);

            return dto;
        }

        public List<PostDto> GetPosts()
        {
            return _mapper.Map<List<PostDto>>(_blogContext.Posts.ToList());
        }

        public PostFilterDto GetPostsByFilter(PostFilterParams filterParams)
        {
            var result = _blogContext.Posts
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p=>p.User)
                .OrderByDescending(d => d.CreationDate).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Category.Slug == filterParams.CategorySlug ||
                                           r.SubCategory.Slug== filterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;


            var model =  new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take)
                    .Select(post => _mapper.Map<PostDto>(post)).ToList(),
                FilterParams = filterParams

            };

            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);

            return model;
        }

        public List<PostDto> GetMustVisitedPosts(int num)
        {
            var result = _blogContext.Posts
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Include(p=>p.User)
                .OrderByDescending(d => d.Visit).Take(num)
                .Select(post => _mapper.Map<PostDto>(post)).ToList();

            return result;
        }

        public List<PostDto> GetRelatedPosts(int categoryId)
        {
            var result = _blogContext.Posts
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Where(p=>p.CategoryId==categoryId||p.SubCategoryId==categoryId).Take(5)
                .Select(post => _mapper.Map<PostDto>(post)).ToList();

            return result;
        }

        public bool IsSlugExist(string slug)
        {
            return _blogContext.Posts.Any(p => p.Slug == slug.ToSlug());
        }

        public void PostVisited(int id)
        {
            var post = _blogContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                throw new AccessViolationException();
            post.Visit++;
            _blogContext.SaveChanges();
        }
    }
}
