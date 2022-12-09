using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using easytransfer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace easytransfer.Pages
{
    public class DepositModel : PageModel
    {
        [BindProperty]
        public Deposit Deposit { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        DatabaseContext _Context;

        public DepositModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
        }

        public void OnGet()
        {

        }

        public async Task<ActionResult> OnPost()
        {
            // Check if the user is logged in.
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "You must be logged in to make a deposit");
                return Page();
            }

            // Check if the amount provided by the user is valid.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the current user from the database.
            var user = _Context.easyuser.SingleOrDefault(
                u => u.uid == Deposit.Uid
            );

            // If the user is not found, show an error message and stay on the page.
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid user");
                return Page();
            }

            // Update the user's balance in the database.
            user.balance += (int)Deposit.Amount;
            _Context.Entry(user).Property(x => x.balance).IsModified = true;
            _Context.SaveChanges();

            // Create a new authentication ticket for the user and add the
            // remember me flag to it.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.uid.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = RememberMe
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // Redirect to the AllUsers page.
            return RedirectToPage("AllUsers");
        }
    }
}