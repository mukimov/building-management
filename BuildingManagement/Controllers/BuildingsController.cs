using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
			var buildings = _db.Buildings.Include(b => b.BuildingToTenants);
			return View(buildings.ToList());
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
			ViewBag.Id = new SelectList(_db.BuildingImages, "BuildingId", "BuildingId");
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

			ViewBag.Id = new SelectList(_db.BuildingImages, "BuildingId", "BuildingId", building.Id);
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
			ViewBag.Id = new SelectList(_db.BuildingImages, "BuildingId", "BuildingId", building.Id);
			return View(building);
		}

		// POST: Buildings/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,NumberOfFloors,YearOfConstruction,City,ZipCode,Address")] Building building) {
			if (ModelState.IsValid) {
				if (Request.Files.Count > 0) {
					HttpPostedFileBase file = Request.Files[0];
					if (file != null) {
						string path = Path.Combine(Server.MapPath(StoragePath), file.FileName);
						file.SaveAs(path);
						building.Image = file.FileName;
					}
				}
				_db.Entry(building).State = EntityState.Modified;
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Id = new SelectList(_db.BuildingImages, "BuildingId", "BuildingId", building.Id);
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

			_db.Buildings.Remove(building ?? throw new InvalidOperationException());
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
