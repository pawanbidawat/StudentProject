using Microsoft.EntityFrameworkCore;
using StudentProject.Models;

namespace StudentProject.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext>options):base(options)
        {
            
        }

        public DbSet<StudentData> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentData>().HasData(
                new StudentData { Id = 1, FirstName = "Saurabh",LastName="Sharma",Course="Mca" , Email="saurabh@gmail.com" , Phone="9834562398", Gender="Male", Password = "default" },
                new StudentData { Id = 2, FirstName = "Anita", LastName = "Roy", Course = "Mca", Email = "anita@gmail.com", Phone = "6754562398", Gender = "Female", Password = "default" },
                new StudentData { Id = 3, FirstName = "Sarthak", LastName = "Barman", Course = "Mca", Email = "sarthak@gmail.com", Phone = "7834562398", Gender = "Male", Password = "default" }
                );
        }
    }
}
