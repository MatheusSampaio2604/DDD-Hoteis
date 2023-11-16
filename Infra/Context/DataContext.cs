
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Home> Home { get; set; }
        public DbSet<Acomodacao> Acomodacao { get; set; }
        public DbSet<Tarifas> Tarifas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(k => k.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }



            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly, x => x.Namespace == "Infra.Mapping");
        }


        private static string GetStringConectionConfig()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hotelaria;Integrated Security=True";
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetStringConectionConfig());
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableDetailedErrors();
        }
    }
}
