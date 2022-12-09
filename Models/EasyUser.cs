using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace easytransfer.Models
{
    [Table("easyuser")]
    public class EasyUser
    {
        [Key]
        public int uid { get; set; }
        [Required(ErrorMessage = "Enter full name")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Enter username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter password")]

        public string password { get; set; }

        public DateTime? birthdate { get; set; }

        public double balance { get; set; }

        public DateTime accountcreateddate { get; set; }

        public int usertype { get; set; }
    }
}



