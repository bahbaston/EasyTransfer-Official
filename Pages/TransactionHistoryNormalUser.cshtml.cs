using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easytransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace easytransfer.Pages
{
    public class TransactionHistoryNormalUserModel : PageModel
    {
        private readonly DatabaseContext _context;

        public TransactionHistoryNormalUserModel(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public List<TransactionHistory> TransactionHistoryNormalUser { get; set; }
        public List<EasyUser> EasyUser { get; set; }

        public void OnGet()
        {

            TransactionHistoryNormalUser = new List<TransactionHistory>();
            EasyUser = new List<EasyUser>();
            TransactionHistoryNormalUser.AddRange(GetTransactionList());
        }

        private List<TransactionHistory> GetTransactionList()
        {
            var transactionsnormalusers = _context.transactionhistory.ToList();
            var users = _context.easyuser.ToList();
            var joined =
            from th in transactionsnormalusers
            join sender in users on th.sender equals sender.uid
            join receiver in users on th.receiver equals receiver.uid
            select new { th, sender, receiver };
            var choice = joined.Where(x => x.sender.usertype == 1 && x.receiver.usertype == 1);
           
            return choice.Select(x => new TransactionHistory
            {
                trhisid = x.th.trhisid,
                sender = x.sender.uid,
                receiver = x.receiver.uid,
                amount = x.th.amount,
                transactiontype = x.th.transactiontype,
                transactiondate = x.th.transactiondate
            }).ToList();
        }
    }
}