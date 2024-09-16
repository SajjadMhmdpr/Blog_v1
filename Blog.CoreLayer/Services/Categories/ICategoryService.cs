using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Utilities;

namespace Blog.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult CreateCategory(CreateCategoryDto dto);
        OperationResult EditCategory(EditCategoryDto dto);
        List<CategoryDto> GetAllCategories();
        List<CategoryDto> GetChildCategories(int parentId);
        List<CategoryDto> GetParentCategories();
        CategoryDto? GetCategoryBy(int id);
        CategoryDto? GetCategoryBy(string slug);
        bool IsSlugExist(string slug);
    }
}
