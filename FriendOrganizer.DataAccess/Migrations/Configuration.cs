namespace FriendOrganizer.DataAccess.Migrations
{
    using FriendOrganizer.Model;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            context.Friends.AddOrUpdate(
                f => f.FirstName,
                new Friend { FirstName = "Thomas", LastName = "Huber" },
                new Friend { FirstName = "Andreas", LastName = "Boehler" },
                new Friend { FirstName = "Julia", LastName = "Egin" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "TypeScript" },
                new ProgrammingLanguage { Name = "F#" },
                new ProgrammingLanguage { Name = "Swift" },
                new ProgrammingLanguage { Name = "Java" });
        }
    }
}
