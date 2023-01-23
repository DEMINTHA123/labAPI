using labAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace labAPI
{
    public class LabDBContext : DbContext
    {
        public LabDBContext (DbContextOptions options) : base(options)
        {

        }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<Lab> Labs { get;set; }
        public DbSet<Chemicals> Chemicals { get; set; }
        public DbSet<Elements> Elements { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Experiment> Experiment { get; set; }
        public DbSet<NonAcademic> NonAcademic { get; set;}
        public DbSet<Reactions> Reactions { get; set; }
        public DbSet<Staff> Staff { get; set; }

    }
}
