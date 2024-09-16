using AutoMapper;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Models.Category;
using Blog.CoreLayer.Services.Categories;
using Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Web.Areas.Admin.Controllers
{
    
    public class CategoryController : AdminControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }
        [Route("admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View(new CreateCategoryViewModel()
            {
                ParentId = parentId
            });
        }
        [HttpPost("admin/category/add/{parentId?}")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateCategoryViewModel ViewModel, int? parentId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var categoryDto = _mapper.Map<CreateCategoryDto>(ViewModel);
            var result = _categoryService.CreateCategory(categoryDto);
            //if (result.Status == OperationResultStatus.Error)
            //{
            //    ModelState.AddModelError("Slug", result.Message);
            //    return View();
            //}

            //SuccessAlert("گروه اضافه شذ");

            //return RedirectToAction("Index");
            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryBy(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var categoryViewModel = _mapper.Map<EditCategoryViewModel>(category);
            return View(categoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _categoryService.EditCategory(_mapper.Map<EditCategoryDto>(viewModel));
            if (result.Status == OperationResultStatus.NotFound)
            {
                ModelState.AddModelError(nameof(viewModel.Title), result.Message);
                return View();
            }
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError(nameof(viewModel.Slug),result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var subCategory = _categoryService.GetChildCategories(parentId);
            return new JsonResult(subCategory);
        }
    }
}
