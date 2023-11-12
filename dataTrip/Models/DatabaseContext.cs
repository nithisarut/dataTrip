using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace dataTrip.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=DESKTOP-DKFGVCH\\SQLEXPRESS;Database=DataTrip;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer("Server=10.103.0.16,1433;Database=DataTrip;MultipleActiveResultSets=true;Trusted_Connection=False; User ID=student;Password=Cs@2700;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
               new Role()
               {
                   Id = 1,
                   RoleName = "ผู้ใช้งาน",
               },
                new Role()
                {
                    Id = 2,
                    RoleName = "ผู้ให้บริการ",
                }
            );


            modelBuilder.Entity<Types>().HasData(
             new Types()
             {
                 Id = 1,
                 TypeName = "แหล่งท่องเที่ยวเชิงนิเวศ ",
             },
              new Types()
              {
                  Id = 2,
                  TypeName = "แหล่งท่องเที่ยวทางโบราณสถานและประวัติศาสตร์ ",
              },
                new Types()
                {
                    Id = 3,
                    TypeName = "แหล่งท่องเที่ยวทางธรรมชาติ ",
                },
                  new Types()
                  {
                      Id = 4,
                      TypeName = "แหล่งท่องเที่ยวประเภทชายหาดและเกาะ",
                  },
                    new Types()
                    {
                        Id = 5,
                        TypeName = "แหล่งท่องเที่ยวทางวัฒนธรรม ประเพณี",
                    }

          );

            modelBuilder.Entity<ClassTrip>().HasData(
         new ClassTrip()
         {
             Id = 1,
             Name = "ทัวร์ราคาถูก ",
         },
          new ClassTrip()
          {
              Id = 2,
              Name = "ทัวร์พรีเมี่ยม",
          }
           

      );

        }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<OrderTrip> OrderTrips { get; set; }
        public DbSet<ClassTrip> ClassTrips { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AddMultipleLocations> AddMultipleLocations { get; set; }

    }
}
