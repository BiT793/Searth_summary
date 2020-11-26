using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace JH_Service.Models
{
    public class JHContext : DbContext
    {
        static string comName = System.Net.Dns.GetHostName();

        public JHContext() : base(@"Server="+comName+@"\SQLEXPRESS;Database=JH_DB;Trusted_Connection=True;")
        {
        }
        public DbSet<ReservAccount> ReservAccounts { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vacanci> Vacancis { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
    }
       
}