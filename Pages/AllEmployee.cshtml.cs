using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easytransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace easytransfer.Pages
{
    public class AllEmployeeModel : PageModel
    {
        DatabaseContext _Context;
        public AllEmployeeModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
        }

        public List<EasyUser> easyuserList { get; set; }
        public void OnGet()
        {
            var data = (from easyuserlist in _Context.easyuser
                        select easyuserlist).ToList();

            easyuserList = data;
        }

        public ActionResult OnGetDelete(int? id)
        {
            if (id != null)
            {
                var data = (from easyuser in _Context.easyuser
                            where easyuser.uid == id
                            select easyuser).SingleOrDefault();

                _Context.Remove(data);
                _Context.SaveChanges();
            }
            return RedirectToPage("AllEmployee");
        }
    }
}
