using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models {
	public class Tenant {
		public int Id { get; set; }
		[Required, MaxLength(20)]
		public string Name { get; set; }
		public virtual ICollection<BuildingToTenant> BuildingToTenants { get; set; }
	}
}