using System.Linq;
using System.Web.Mvc;
using BuildingManagement.DAL;

namespace BuildingManagement.Controllers {
	public class BuildingsController : Controller {
		private readonly PropertyCatalogContext _db = new PropertyCatalogContext();

		public ActionResult Index() {
			return View(_db.Buildings.ToList());
		}
	}
}