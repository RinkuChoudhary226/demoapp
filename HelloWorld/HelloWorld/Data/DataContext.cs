using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder objmodel)
        //{
        //    objmodel.Entity<Employee>().ToTable("Employee_New");
        //}
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
