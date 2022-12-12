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

            // Check if sender is of type 1
            var sender = _Context.easyuser.SingleOrDefault(x => x.uid == TransactionHistory.sender);
            if (sender.usertype != 1)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Only normal user can send to normal user");
                return Page();
            }

            // Check if receiver is of type 1
            var receiver = _Context.easyuser.SingleOrDefault(x => x.uid == TransactionHistory.receiver);
            if (receiver.usertype != 1)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Only normal user can send to normal user");
                return Page();
            }

            // Check if sender has enough balance
            if (sender.balance < TransactionHistory.amount)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Insufficient balance");
                return Page();
            }

            sender.balance = sender.balance - TransactionHistory.amount;
            _Context.Entry(sender).Property(x => x.balance).IsModified = true;

            receiver.balance = receiver.balance + TransactionHistory.amount;
            _Context.Entry(receiver).Property(x => x.balance).IsModified = true;

            transactionhistory.transactiondate = System.DateTime.Now;
            transactionhistory.transactiontype = 3;

            var result = _Context.Add(transactionhistory);
            _Context.SaveChanges();

            return RedirectToPage("AllUsers");
        }
    }
}