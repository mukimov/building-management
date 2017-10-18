using System.Collections.Generic;
using BuildingManagement.Models;

namespace BuildingManagement.DAL {
	public class PropertyCatalogInitializer :System.Data.Entity.DropCreateDatabaseIfModelChanges<PropertyCatalogContext> {
		protected override void Seed(PropertyCatalogContext context) {
			var buildings = new List<Building> {
				new Building {
					Name = Faker.NameFaker.Name(),
					NumberOfFloors = Faker.NumberFaker.Number(200),
					City = Faker.LocationFaker.City(),
					ZipCode = Faker.LocationFaker.ZipCode(),
					Address = Faker.LocationFaker.Street(),
					YearOfCunstruction = Faker.DateTimeFaker.DateTimeBetweenYears(1950, 2010)
				}
			};
			buildings.ForEach(b => context.Buildings.Add(b));
			context.SaveChanges();
		}
	}
}