namespace mvc_project
{
    using Models;
    using Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model1 : DbContext
    {
        
        public Model1()
            : base("name=Model1")
        {
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}