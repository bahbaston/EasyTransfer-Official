using easytransfer.Models;

namespace easytransfer
{
    public class TransactionHistoryModel
    {
        public int sender { get; set; } = 2; // sender must be usertype 2 (employee)
        public int receiver { get; set; } = 1; // receiver must be usertype 1 (normal user)
        public double amount { get; set; }
    }
}