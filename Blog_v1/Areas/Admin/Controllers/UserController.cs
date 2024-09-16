using Blog.CoreLayer.Dtos.Users;
using Blog.CoreLayer.Models.User;
using Blog.CoreLayer.Profiles;
using Blog.CoreLayer.Services.Users;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string fullName = "", int role = 1)
        {
            var users = _userService.GetUsersByFilter(new UserFilterParams()
            {
                PageId = pageId,
                FullName = fullName,
                //Role =((role>=0)&&(role<=2))?(UserRole)role:UserRole.User,
                Take = 2
            });
            return View(users);
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View(new CreateUserViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(CreateUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = _userService.CreateUser(viewModel);
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(EditUserDto dto)
        {
            if (dto.FullName == null || dto.Role == null)
            {
                return RedirectAndShowAlert(new OperationResult()
                {
                    Status = OperationResultStatus.NotFound,
                    Message = "فیلد ها را به درستی پر کنید"
                }, RedirectToAction("Index"));
            }

            var result =  _userService.EditUser(dto);
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        public IActionResult ShowEditModal(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return RedirectAndShowAlert(new OperationResult()
                {
                    Status = OperationResultStatus.NotFound,
                    Message = "کاربر پیدا نشد"
                }, RedirectToAction("Index"));
            }
            return PartialView("_EditUser",UserMapper.Map(user));
        }
        
    }
}
