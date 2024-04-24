
//using System;
//using System.Threading.Tasks;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using CompanyAPIs.Models;

//namespace CompanyAPIs.Data
//{
//    public class DataContext : DbContext
//    {
//        public DataContext(DbContextOptions<DataContext> options) : base(options) 
//        {
        
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            base.OnConfiguring(optionsBuilder);
//            optionsBuilder.UseSqlServer("server=MINAMILAD\\MSSQLSERVER02;Database=Companydb; Trusted_Connection=true;TrustServerCertificate=true");
//        }

//        public DbSet<Users> Users { get; set; } 
//    }
//}
