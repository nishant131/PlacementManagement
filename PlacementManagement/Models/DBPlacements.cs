namespace PlacementManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBPlacements : DbContext
    {
        // Your context has been configured to use a 'DBPlacements' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PlacementManagement.Models.DBPlacements' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBPlacements' 
        // connection string in the application configuration file.
        public DBPlacements()
            : base("name=DBPlacements")
        {
            Database.SetInitializer<DBPlacements>(new CreateDatabaseIfNotExists<DBPlacements>());
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Placementstatus> Placementstatus { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}