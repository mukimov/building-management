using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Foolproof;

namespace BuildingManagement.Models {
	public class Building {
		public int Id { get; set; }
		[MaxLength(30)]
		public string Name { get; set; }
		[Required, Range(1, 200), DisplayName("Number of floors")]
		public int NumberOfFloors { get; set; }
		[DataType(DataType.Date)]
		[Required, DisplayName("Year of Construction")]
		[GreaterThan("StartDate", ErrorMessage = "Year of Construction must be greater than 1/1/1900.")]
		public DateTime YearOfConstruction { get; set; }
		[Required, MaxLength(30)]
		public string City { get; set; }
		[Required, RegularExpression("^\\d{5}$")]
		public string ZipCode { get; set; }
		[Required, MaxLength(30)]
		public string Address { get; set; }
		public virtual BuildingImage BuildingImage { get; set; }
		public virtual ICollection<BuildingToTenant> BuildingToTenants { get; set; }

		[NotMapped]
		public DateTime StartDate { get; } = new DateTime(1900, 1, 1);
	}
}