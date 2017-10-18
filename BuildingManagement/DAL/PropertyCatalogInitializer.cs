using System.Linq;
using BuildingManagement.Models;

namespace BuildingManagement.DAL {
	public class PropertyCatalogInitializer : System.Data.Entity.DropCreateDatabaseAlways<PropertyCatalogContext> {
		protected override void Seed(PropertyCatalogContext context) {
			Enumerable.Range(0, 20).Select(x => new Building {
				Name = Faker.CompanyFaker.Name(),
				NumberOfFloors = Faker.NumberFaker.Number(199),
				City = Faker.LocationFaker.City(),
				ZipCode = Faker.LocationFaker.ZipCode(),
				Address = Faker.LocationFaker.Street(),
				YearOfConstruction = Faker.DateTimeFaker.DateTimeBetweenYears(50)
			}).ToList().ForEach(b => context.Buildings.Add(b));

			context.SaveChanges();

			Enumerable.Range(0, 100).Select(x => new Tenant {
				Name = Faker.NameFaker.Name()
			}).ToList().ForEach(t => context.Tenants.Add(t));

			context.SaveChanges();


			Enumerable.Range(1, 20).SelectMany(x => new [] {
				new BuildingToTenant {
					BuildingId = x,
					TenantId = Faker.NumberFaker.Number(1, 49),
					ExpirationDate = Faker.DateTimeFaker.DateTimeBetweenYears(10, 20)
				}, 
				new BuildingToTenant {
					BuildingId = x,
					TenantId = Faker.NumberFaker.Number(50, 99),
					ExpirationDate = Faker.DateTimeFaker.DateTimeBetweenYears(10, 20)
				},
			}).ToList().ForEach(b => context.BuildingToTenants.Add(b));

			context.SaveChanges();
		}
	}
}