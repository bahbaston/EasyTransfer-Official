using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using easytransfer.Models;

namespace easytransfer.Pages
{
    public class EditUserModel : PageModel
    {
        DatabaseContext _Context;
        public EditUserModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
        }

        [BindProperty]
        public EasyUser EasyUser { get; set; }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                var data = (from easyuser in _Context.easyuser
                            where easyuser.uid == id
                            select easyuser).SingleOrDefault();
                EasyUser = data;
            }
        }

        public ActionResult OnPost()
        {
            var easyuser = EasyUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _Context.Entry(easyuser).Property(x => x.fullname).IsModified = true;
            _Context.Entry(easyuser).Property(x => x.username).IsModified = true;
            _Context.Entry(easyuser).Property(x => x.password).IsModified = true;
            _Context.Entry(easyuser).Property(x => x.balance).IsModified = true;
            _Context.SaveChanges();
            return RedirectToPage("AllUsers");
        }
    }
}