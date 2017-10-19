using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BuildingManagement.Models {
	public class Tenant {
		public int Id { get; set; }
		[Required, MaxLength(20)]
		public string Name { get; set; }
		[JsonIgnore]
		public virtual ICollection<BuildingToTenant> BuildingToTenants { get; set; }
	}
}