using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airlines.Data.AuthEntities;
using Airlines.Data.Entities;

namespace Airlines.Data.Repository
{
    public class AirlineContext : DbContext
    {
        public AirlineContext() : base("AirlineContext")
        { }
       
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceTeam> RaceTeams { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Stuardess> Stuardesses { get; set; }
        public DbSet<Navigator> Navigators { get; set; }
        public DbSet<RadioMan> RadioMen { get; set; }
        public DbSet<Query> Queries { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public class AirlineDbInitializer : CreateDatabaseIfNotExists<AirlineContext>
        {
            protected override void Seed(AirlineContext db)
            {
                Role admin = new Role { Name = "admin" };
                Role dispatcher = new Role { Name = "dispatcher" };
                Role user = new Role { Name = "user" };
                db.Roles.Add(admin);
                db.Roles.Add(user);
                db.Roles.Add(dispatcher);
                db.Users.Add(new User
                {
                    Username = "admin",
                    Email = "someemail@gmail.com",
                    Password = "admin",
                    Role = admin
                });

                base.Seed(db);
            }
        }
    }
}
