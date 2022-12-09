using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace easytransfer.Models
{
    public class Deposit
    {
        public int Uid { get; set; }
        public decimal Amount { get; set; }
        public bool RememberMe { get; set; }
    }
}