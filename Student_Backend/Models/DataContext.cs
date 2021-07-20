using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Backend.Models
{
    public class DataContext:DbContext
    {
         public  DbSet<Student> Students { get; set; }
        public  DbSet<Teacher> Teachers { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                   .HasMany(u => u.Students)
                   .WithOne(k => k.Teacher);
                
        }
    }
}
