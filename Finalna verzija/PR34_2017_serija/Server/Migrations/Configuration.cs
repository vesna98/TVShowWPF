namespace Server.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Server.Serijica2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Server.Serijica2Context context)
        {
            context.Users.Add(new Common.Models.User("admin","admin"));
        }
    }
}
