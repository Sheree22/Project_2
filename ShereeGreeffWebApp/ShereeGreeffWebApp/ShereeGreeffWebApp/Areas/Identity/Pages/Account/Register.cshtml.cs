using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ShereeGreeffWebApp.Areas.Identity.Data;

namespace ShereeGreeffWebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ShereeGreeffWebAppUser> _signInManager;
        private readonly UserManager<ShereeGreeffWebAppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ShereeGreeffWebAppUser> userManager,
            SignInManager<ShereeGreeffWebAppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            //properties
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Enter name")]
            [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Name must have a maximum lenght of 15 and minimum lenght of 3")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            [StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Lastname must have a maximum lenght of 15 and minimum lenght of 3")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Enter age")]
            [Range(18, 55, ErrorMessage = "Age must be between 18 and 55")]
            public int Age { get; set; }

            [Required]
            [Display(Name = "Enter number")]
            [RegularExpression("^[0-9]{10}$", ErrorMessage = "Contact number can only be 10 digits with values between 0 and 9")]
            public int Number { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = "Enter proper email address")]
            [Display(Name = "Enter email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Enter password")]
            [DataType(DataType.Password)]
           // [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must contain at least 8 characters and must have 1 alphabet value and 1 numeric value")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Name { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ViewData["roles"] = _roleManager.Roles.ToList();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var role = _roleManager.FindByIdAsync(Input.Name).Result;
            if (ModelState.IsValid)
            {
                var user = new ShereeGreeffWebAppUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, Age = Input.Age, Number = Input.Number};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, role.Name);

                      var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                       code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                       var callbackUrl = Url.Page(
                           "/Account/ConfirmEmail",
                           pageHandler: null,
                           values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                           protocol: Request.Scheme);

                       await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                           $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                       if (_userManager.Options.SignIn.RequireConfirmedAccount)
                       {
                           return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                       }
                       else
                       {
                           await _signInManager.SignInAsync(user, isPersistent: false);
                           return LocalRedirect(returnUrl);
                       }
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
