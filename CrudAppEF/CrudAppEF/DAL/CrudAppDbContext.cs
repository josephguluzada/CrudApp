using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppEF.DAL
{
    public class CrudAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=CATALYST\\SQLEXPRESS;Database=CrudApp;Trusted_Connection=True") ;
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Contact>().Property(x => x.FirstName).IsRequired(true).HasMaxLength(30);
            modelBuilder.Entity<Contact>().Property(x => x.LastName).IsRequired(true).HasMaxLength(40);
            modelBuilder.Entity<Contact>().Property(x => x.Phone).IsRequired(true).HasMaxLength(50);
            modelBuilder.Entity<Contact>().Property(x => x.Email).IsRequired(true).HasMaxLength(50);
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
