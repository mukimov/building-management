using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models {
	public class Building {
		public int Id { get; set; }
		[MaxLength(30)]
		public string Name { get; set; }
		[Required, Range(1, 200)]
		public int NumberOfFloors { get; set; }
		[DataType(DataType.Date)]
		[Required, Range(typeof(DateTime), "1/1/1900", "1/1/2099")]
		public DateTime YearOfCunstruction { get; set; }
		[Required, MaxLength(30)]
		public string City { get; set; }
		[Required, RegularExpression("^\\d{6}$")]
		public string ZipCode { get; set; }
		[Required, MaxLength(30)]
		public string Address { get; set; }
		public virtual BuildingImage BuildingImage { get; set; }
		public virtual ICollection<BuildingToTenant> BuildingToTenants { get; set; }
	}
}