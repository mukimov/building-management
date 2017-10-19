using System;
using System.Linq;
using BuildingManagement.Models;

namespace BuildingManagement.DAL {
	public class PropertyCatalogInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PropertyCatalogContext> {
		protected override void Seed(PropertyCatalogContext context) {
			Enumerable.Range(0, 20).Select(x => new Building {
				Name = Truncate(Faker.CompanyFaker.Name(), 30),
				NumberOfFloors = Faker.NumberFaker.Number(1, 199),
				City = Truncate(Faker.LocationFaker.City(), 30),
				ZipCode = $"0{Faker.LocationFaker.ZipCode()}",
				Address = Truncate(Faker.LocationFaker.Street(), 30),
				YearOfConstruction = Faker.DateTimeFaker.DateTimeBetweenYears(50)
			}).ToList().ForEach(b => context.Buildings.Add(b));

			context.SaveChanges();

			Enumerable.Range(0, 100).Select(x => new Tenant {
				Name = Truncate(Faker.NameFaker.Name(), 20)
			}).ToList().ForEach(t => context.Tenants.Add(t));

			context.SaveChanges();

			foreach (Building building in context.Buildings) {
				var buildingToTenant1 = new BuildingToTenant {
					BuildingId = building.Id,
					TenantId = Faker.NumberFaker.Number(1, 49),
					ExpirationDate = Faker.DateTimeFaker.DateTimeBetweenYears(10, 20)
				};
				var buildingToTenant2 = new BuildingToTenant {
					BuildingId = building.Id,
					TenantId = Faker.NumberFaker.Number(50, 99),
					ExpirationDate = Faker.DateTimeFaker.DateTimeBetweenYears(10, 20)
				};
				context.BuildingToTenants.Add(buildingToTenant1);
				context.BuildingToTenants.Add(buildingToTenant2);
			}

			context.SaveChanges();
		}

		private string Truncate(string value, int maxLength) {
			return string.IsNullOrEmpty(value) ? value : value.Substring(0, Math.Min(value.Length, maxLength));
		}
	}
}