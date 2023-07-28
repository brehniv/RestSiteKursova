using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestSiteKursova.Data;
using RestSiteKursova.Models;

namespace RestSiteKursova.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDBContent _appDBContent;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,AppDBContent appDBcontent)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDBContent = appDBcontent;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    if (model.roles) { await _userManager.AddToRoleAsync(user, "courier"); }

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                var check_mail = await _userManager.FindByEmailAsync(model.Email);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (check_mail == null)
                {
                    ModelState.AddModelError("", "Користувача не знайдено");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильній логін або(і) пароль");
                }
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]

        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            User user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]

        public async Task<ActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                EditModel model = new EditModel { Email = user.Email, Year = user.Year };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.Year = model.Year;
                user.Email = model.Email;
                user.UserName = model.Email;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if(user.Email!=model.Email)
                    return RedirectToAction("Logout", "Account");
                    else return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Вибачте. Щось пішло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Користувача не знайдено");
            }

            return View(model);
        }
        [Authorize]

        public async Task<IActionResult> ChangePassword()
        {
            User user = await _userManager.GetUserAsync(User);
            ChangePasswordViewModel model = new ChangePasswordViewModel {};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Logout");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Старий пароль не співпадає");
                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ViewHistoryOrder()
        {
            User user = await _userManager.GetUserAsync(User);
            IEnumerable<Order> orders = _appDBContent.Order.Where(c => c.email == user.Id);
            return View(orders);
        }
    }
}