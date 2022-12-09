using easytransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;


namespace easytransfer.Pages
{
    public class TransactionHistoryModel : PageModel
    {


       [BindProperty]
        public TransactionHistory TransactionHistory { get; set; }

        DatabaseContext _Context;

        public TransactionHistoryModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
         }

        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            var transactionhistory = TransactionHistory;
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var data = (from easyuser_ in _Context.easyuser
                        where easyuser_.uid == TransactionHistory.sender
                        select easyuser_).SingleOrDefault();
            
            EasyUser sender = data;
            sender.balance = sender.balance - TransactionHistory.amount;
            _Context.Entry(sender).Property(x => x.balance).IsModified = true;


            var data2 = (from easyuser_ in _Context.easyuser
                        where easyuser_.uid == TransactionHistory.receiver
                        select easyuser_).SingleOrDefault();

            EasyUser receiver = data2;
            receiver.balance = receiver.balance + TransactionHistory.amount;
            _Context.Entry(receiver).Property(x => x.balance).IsModified = true;



            //transactionhistory.sender = "kouassib1";
            //transactionhistory.receiver = "kouassib1";
            //transactionhistory.amount = 1000;
            transactionhistory.transactiondate = System.DateTime.Now;
            transactionhistory.transactiontype = 1;
           


            var result = _Context.Add(transactionhistory);
            _Context.SaveChanges();

            return RedirectToPage("AllNormalUser");
        }

    }
}
