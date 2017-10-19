using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuildingManagement.DAL;
using BuildingManagement.Models;

namespace BuildingManagement.Controllers {
	public class BuildingsController : Controller {
		private readonly PropertyCatalogContext _db = new PropertyCatalogContext();
		private const string StoragePath = "~/images/upload";

		// GET: Buildings
		public ActionResult Index() {
			List<Building> buildings = _db.Buildings.Include(b => b.BuildingToTenants).ToList();
			return View(buildings);
		}

		// GET: Buildings/Details/5
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Building building = _db.Buildings.Find(id);
			if (building == null) {
				return HttpNotFound();
			}
			return View(building);
		}

		// GET: Buildings/Create
		public ActionResult Create() {
			return View();
		}

		// POST: Buildings/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,NumberOfFloors,YearOfConstruction,City,ZipCode,Address")] Building building) {
			if (ModelState.IsValid) {
				if (Request.Files.Count > 0) {
					HttpPostedFileBase file = Request.Files[0];
					if (file != null) {
						string path = Path.Combine(Server.MapPath(StoragePath), file.FileName);
						file.SaveAs(path);
						building.Image = file.FileName;
					}
				}
				_db.Buildings.Add(building);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(building);
		}

		// GET: Buildings/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Building building = _db.Buildings.Find(id);

			if (building == null) {
				return HttpNotFound();
			}

			building.SelectedTenantsList = _db.BuildingToTenants
				.Where(b => b.BuildingId == building.Id)
				.Select(b => b.TenantId).ToList();

			ViewBag.TentantList = new MultiSelectList(_db.Tenants, "Id", "Name", building.SelectedTenantsList);

			return View(building);
		}

		// POST: Buildings/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,NumberOfFloors,YearOfConstruction,City,ZipCode,Address,SelectedTenantsList")] Building building) {
			_db.Buildings.Attach(building);

			DbEntityEntry<Building> item = _db.Entry(building);

			item.Collection(i => i.BuildingToTenants).Load();

			building.BuildingToTenants.Clear();

			foreach (int selectedTenantId in building.SelectedTenantsList) {
				building.BuildingToTenants.Add(new BuildingToTenant {
					TenantId = selectedTenantId,
					ExpirationDate = DateTime.Now
				});
			}

			if (ModelState.IsValid) {
				if (Request.Files.Count > 0) {
					HttpPostedFileBase file = Request.Files[0];
					if (file != null && file.ContentLength > 0) {
						string path = Path.Combine(Server.MapPath(StoragePath), file.FileName);
						file.SaveAs(path);
						building.Image = file.FileName;
					}
				}
				_db.Entry(building).State = EntityState.Modified;
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.TentantList = new MultiSelectList(_db.Tenants, "Id", "Name", building.SelectedTenantsList);

			return View(building);
		}

		// GET: Buildings/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Building building = _db.Buildings.Find(id);
			if (building == null) {
				return HttpNotFound();
			}
			return View(building);
		}

		// POST: Buildings/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Building building = _db.Buildings.Include(b => b.BuildingToTenants)
				.FirstOrDefault(b => b.Id == id);

			if (building == null) {
				return HttpNotFound();
			}

			_db.Buildings.Remove(building);

			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				_db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
