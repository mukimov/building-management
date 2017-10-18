using BuildingManagement.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BuildingManagement.DAL {
	public class PropertyCatalogContext : DbContext {
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Tenant> Tenants { get; set; }
		public DbSet<BuildingToTenant> BuildingToTenants { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}