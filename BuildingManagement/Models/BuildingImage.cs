using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models {
	public class BuildingImage {
		[Key]
		[ForeignKey("Building")]
		public int BuildingId { get; set; }
		public string FileName { get; set; }
		public string Extension { get; set; }
		public virtual Building Building { get; set; }
	}
}