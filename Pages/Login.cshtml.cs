using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using easytransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace easytransfer.Pages
{
    public class LoginModel : PageModel
    {
        DatabaseContext _Context;

        public LoginModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Uid { get; set; }
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _Context.easyuser.SingleOrDefault(
                u => u.uid == Input.Uid &&
                     u.password == Input.Password
            );

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid ID or password");
                return Page();
            }

            return RedirectToPage("AllUsers");
        }
    }
}