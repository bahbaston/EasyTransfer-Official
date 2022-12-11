using easytransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace easytransfer.Pages
{
    public class DepositAgentModel : PageModel
    {
        [BindProperty]
        public TransactionHistory TransactionHistory { get; set; }

        DatabaseContext _Context;

        public DepositAgentModel(DatabaseContext databasecontext)
        {
            _Context = databasecontext;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Check if user is of type 2
            var sender = _Context.easyuser.SingleOrDefault(x => x.uid == TransactionHistory.sender);
            if (sender.usertype != 2)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Only Agent can send to normal user");
                return;
            }

            // Check if receiver is of type 1
            var receiver = _Context.easyuser.SingleOrDefault(x => x.uid == TransactionHistory.receiver);
            if (receiver.usertype != 1)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Only Agent can send to normal user");
                return;
            }
            // Check if sender has enough balance
            if (sender.balance < TransactionHistory.amount)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Insufficient balance");
                return;
            }

            if (!ModelState.IsValid)
            {
                return;
            }

            // Update sender's balance
            sender.balance = sender.balance - TransactionHistory.amount;
            _Context.Entry(sender).Property(x => x.balance).IsModified = true;

            // Update receiver's balance
            receiver.balance = receiver.balance + TransactionHistory.amount;
            _Context.Entry(receiver).Property(x => x.balance).IsModified = true;

            TransactionHistory.transactiondate = System.DateTime.Now;
            TransactionHistory.transactiontype = 1;

            var result = _Context.Add(TransactionHistory);
            _Context.SaveChanges();

            // Redirect to AllUsers page
            Response.Redirect("TransactionHistoryAgent");
        }

    }
}