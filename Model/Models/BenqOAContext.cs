using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Model.Models.Mapping;

namespace Model.Models
{
    public partial class BenqOAContext : DbContext
    {
        static BenqOAContext()
        {
            Database.SetInitializer<BenqOAContext>(null);
        }

        public BenqOAContext()
            : base("Name=BenqOAContext")
        {
        }

        public DbSet<Announce> Announces { get; set; }
        public DbSet<AnnounceType> AnnounceTypes { get; set; }
        public DbSet<AskForLeave> AskForLeaves { get; set; }
        public DbSet<AskForLeaveType> AskForLeaveTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Permisson> Permissons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Project_Discuss> Project_Discuss { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Reim> Reims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Role_Permisson> Role_Permisson { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Role> User_Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnnounceMap());
            modelBuilder.Configurations.Add(new AnnounceTypeMap());
            modelBuilder.Configurations.Add(new AskForLeaveMap());
            modelBuilder.Configurations.Add(new AskForLeaveTypeMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new FileTypeMap());
            modelBuilder.Configurations.Add(new PermissonMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new Project_DiscussMap());
            modelBuilder.Configurations.Add(new PurchaseMap());
            modelBuilder.Configurations.Add(new ReimMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new Role_PermissonMap());
            modelBuilder.Configurations.Add(new SexMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TripMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new User_RoleMap());
        }
    }
}
