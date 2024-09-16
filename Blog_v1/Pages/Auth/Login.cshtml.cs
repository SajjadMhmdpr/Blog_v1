using Blog.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Blog.CoreLayer.Dtos.Users;
using Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blog.Web.Pages.Auth
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        #region Properties
        [BindProperty]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }
        [BindProperty]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Password { get; set; }

        #endregion
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userService.LoginUser2(new UserLoginDto()
            {
                UserName = UserName,
                Password = Password
            });

            if (user.UserName==null)
            {
                //ModelState.AddModelError("UserName",result.Message);    //use from LoginUser and OperationResult
                ModelState.AddModelError("UserName","کاربری با این مشخصات یافت نشد");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim("test","test"),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),

            };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(claimPrincipal ,  new AuthenticationProperties() { IsPersistent = true });

            return RedirectToPage("../Index");
            //return Redirect("/Index");

        }
    }
}
