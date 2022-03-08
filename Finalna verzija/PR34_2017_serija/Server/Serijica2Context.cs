namespace Server
{
    using Common.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Serijica2Context : DbContext
    {
        // Your context has been configured to use a 'Serijica2Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Server.Serijica2Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Serijica2Context' 
        // connection string in the application configuration file.
        public Serijica2Context()
            : base("name=Serijica2Context")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Genre> Genres { get; set; }
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