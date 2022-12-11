using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace easytransfer.Models
{
   
        [Table("transactionhistory")]
        public class TransactionHistory
        {
            [Key]
            public int trhisid { get; set; }

            [Required(ErrorMessage = "Enter the sender")]
            public int sender { get; set; }

            [Required(ErrorMessage = "Enter the receiver")]
            public int receiver { get; set; }

            [Required(ErrorMessage = "Enter amount")]
            public double amount { get; set; }

            public DateTime transactiondate { get; set; }

            public int transactiontype { get; set; }




    }
}
