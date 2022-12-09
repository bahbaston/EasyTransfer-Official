using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using easytransfer.Models;

namespace easytransfer.Models
{
    public class DatabaseContext : DbContext
    {

        public DbSet<EasyUser> easyuser { get; set; }
        public DbSet<TransactionHistory> transactionhistory { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
