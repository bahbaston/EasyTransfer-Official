using easytransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace easytransfer.Pages
{
    public class AddEmployeeModel : PageModel
    {


        [BindProperty]
        public EasyUser EasyUser { get; set; }

        DatabaseContext _Context;

        public AddEmployeeModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
        }

        public void OnGet()
        {

        }

        public ActionResult OnPost()
        {
            var easyuser = EasyUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            easyuser.birthdate = null;
            easyuser.balance = 0;
            easyuser.usertype = 2;
            easyuser.accountcreateddate = System.DateTime.Now;


            var result = _Context.Add(easyuser);
            _Context.SaveChanges();

            return RedirectToPage("AllEmployee");
        }
    }
}
