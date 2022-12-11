using easytransfer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace easytransfer.Pages
{
    public class TransactionListModel : PageModel
    {
        private readonly DatabaseContext _context;

        public TransactionListModel(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public List<TransactionHistory> TransactionList { get; set; }
        public List<EasyUser> EasyUserList { get; set; }

        public void OnGet()
        {
            // Initialize the TransactionList and EasyUserList properties
            TransactionList = new List<TransactionHistory>();
            EasyUserList = new List<EasyUser>();

            // Populate the TransactionList property with a list of transactions
            TransactionList.AddRange(GetTransactionList());
        }

        private List<TransactionHistory> GetTransactionList()
        {
            // Get the transactions and users from the database
            var transactions = _context.transactionhistory.ToList();
            var users = _context.easyuser.ToList();

            // Join the TransactionHistory and EasyUser models on the senderId and receiverId properties
            var joined =
            from th in transactions
            join sender in users on th.sender equals sender.uid
            join receiver in users on th.receiver equals receiver.uid

            select new { th, sender, receiver };


            // Filter the joined table by usertype
            var filtered = joined.Where(x => x.sender.usertype == 2 && x.receiver.usertype == 1);

            // Return the filtered transactions as a list
            return filtered.Select(x => new TransactionHistory
            {
                trhisid = x.th.trhisid,
                sender = x.sender.uid,
                receiver = x.receiver.uid,
                amount = x.th.amount,
                transactiontype= x.th.transactiontype,
                transactiondate = x.th.transactiondate
            }).ToList();
        }
    }
}