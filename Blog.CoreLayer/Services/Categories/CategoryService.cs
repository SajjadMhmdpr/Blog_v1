using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Context;
using Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _blogContext;
        private readonly IMapper _mapper;

        public CategoryService(BlogContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public OperationResult CreateCategory(CreateCategoryDto dto)
        {
            if (IsSlugExist(dto.Slug))
            {
                return OperationResult.Error("این slug تکراری است");
            }
            var category = _mapper.Map<Category>(dto);

            category.IsDelete = false;
            category.Slug = dto.Slug.ToSlug();
            category.CreationDate = DateTime.Now;

            _blogContext.Categories.Add(category);
            _blogContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto dto)
        {
            var category = _blogContext.Categories.FirstOrDefault(c => c.Id == dto.Id);

            if(category==null)
                return OperationResult.NotFound();
            if (category.Slug.ToSlug() != dto.Slug.ToSlug())
            {
                if (IsSlugExist(dto.Slug))
                {
                    return OperationResult.Error("این slug تکراری است");
                }
            }
            
            _blogContext.Categories.Entry(category).State = EntityState.Detached;
            var newCategory = _mapper.Map<Category>(dto);
            newCategory.Slug = newCategory.Slug.ToSlug();
            //category =_mapper.Map<Category>(dto);
            _blogContext.Categories.Update(newCategory);

            _blogContext.SaveChanges();

            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategories()
        {
            return _mapper.Map<List<CategoryDto>>(_blogContext.Categories.ToList());
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _mapper.Map<List<CategoryDto>>(_blogContext.Categories
                .Where(c=>c.ParentId==parentId).ToList());
        }

        public List<CategoryDto> GetParentCategories()
        {
            return _mapper.Map<List<CategoryDto>>(_blogContext.Categories
                .Where(c => c.ParentId == null).ToList());
        }

        public CategoryDto? GetCategoryBy(int id)
        {
            var category = _blogContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return null;
            return _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto? GetCategoryBy(string slug)
        {
            var category = _blogContext.Categories.FirstOrDefault(c => c.Slug == slug);

            if (category == null)
                return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public bool IsSlugExist(string slug)
        {
            return _blogContext.Categories.Any(c => c.Slug == slug.ToSlug());
        }
    }
}
