using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models {
	public class BuildingToTenant {
		[Key, Column(Order = 0)]
		public int BuildingId { get; set; }
		[Key, Column(Order = 1)]
		public int TenantId { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{DD.MM.YYYY}")]
		public DateTime ExpirationDate { get; set; }
		[ForeignKey("BuildingId")]
		public virtual Building Building { get; set; }
		[ForeignKey("TenantId")]
		public virtual Tenant Tenant { get; set; }
	}
}