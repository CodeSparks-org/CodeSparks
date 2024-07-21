using CodeSparks.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CodeSparks.Areas.Identity.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }



        public SignUpModel(UserManager<AppUser> userManager)
        {
            Input = new InputModel();
            _userManager = userManager;
        }
        public void OnGet(string username)
        {
            var prevData = TempData["InputModel"] as InputModel;
            if (prevData == null)
            {
                Input.Username = username;
            }
            else
            {
                Input = prevData;
                TempData.Clear();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["InputModel"] = Input; // Store model in TempData
                return Page();
            }

            if (Input.Password != Input.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return Page();
            }

            var user = new AppUser { UserName = Input.Username, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Successful user creation logic
                return RedirectToPage("/Index"); // Replace with desired redirect
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

    }
}
