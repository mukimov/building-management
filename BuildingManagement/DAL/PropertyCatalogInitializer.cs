using System.Linq;
using BuildingManagement.Models;

namespace BuildingManagement.DAL {
	public class PropertyCatalogInitializer : System.Data.Entity.DropCreateDatabaseAlways<PropertyCatalogContext> {
		protected override void Seed(PropertyCatalogContext context) {
			Enumerable.Range(0, 20).Select(x => new Building {
				Name = Faker.NameFaker.Name(),
				NumberOfFloors = Faker.NumberFaker.Number(200),
				City = Faker.LocationFaker.City(),
				ZipCode = Faker.LocationFaker.ZipCode(),
				Address = Faker.LocationFaker.Street(),
				YearOfConstruction = Faker.DateTimeFaker.DateTimeBetweenYears(50)
			}).ToList().ForEach(b => context.Buildings.Add(b));

			context.SaveChanges();
		}
	}
}