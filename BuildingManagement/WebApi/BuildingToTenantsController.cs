using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BuildingManagement.DAL;
using BuildingManagement.Models;
using Newtonsoft.Json;

namespace BuildingManagement.WebApi {
	public class BuildingToTenantsController : ApiController {
		private PropertyCatalogContext _db = new PropertyCatalogContext();

		// GET: api/BuildingToTenants/5
		[ResponseType(typeof(IEnumerable<Tenant>))]
		public IHttpActionResult GetBuildingToTenant(int id) {
			List<Tenant> availableTenants =_db.Tenants.Where(t => t.BuildingToTenants.All(b => b.BuildingId != id)).ToList();

			return Ok(availableTenants);
		}

		// PUT: api/BuildingToTenants/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutBuildingToTenant(int id, BuildingToTenant buildingToTenant) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (id != buildingToTenant.BuildingId) {
				return BadRequest();
			}

			_db.Entry(buildingToTenant).State = EntityState.Modified;

			try {
				_db.SaveChanges();
			} catch (DbUpdateConcurrencyException) {
				if (!BuildingToTenantExists(id)) {
					return NotFound();
				}
				throw;
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/BuildingToTenants
		[ResponseType(typeof(BuildingToTenant))]
		public IHttpActionResult PostBuildingToTenant(BuildingToTenant buildingToTenant) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			_db.BuildingToTenants.Add(buildingToTenant);

			try {
				_db.SaveChanges();
			} catch (DbUpdateException) {
				if (BuildingToTenantExists(buildingToTenant.BuildingId)) {
					return Conflict();
				}
				throw;
			}
			Tenant tenant = _db.Tenants.Find(buildingToTenant.TenantId);
			if (tenant == null) {
				return Conflict();
			}

			return CreatedAtRoute("DefaultApi", new { id = buildingToTenant.BuildingId }, new { tenant.Name, tenant.Id, buildingToTenant.ExpirationDate });
		}

		// DELETE: api/BuildingToTenants/5
		[ResponseType(typeof(BuildingToTenant))]
		public IHttpActionResult DeleteBuildingToTenant(int id, int tenantId) {
			BuildingToTenant buildingToTenant = _db.BuildingToTenants.First(b => b.BuildingId == id && b.TenantId == tenantId);

			if (buildingToTenant == null) {
				return NotFound();
			}

			_db.BuildingToTenants.Remove(buildingToTenant);
			_db.SaveChanges();

			return Ok(buildingToTenant);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				_db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool BuildingToTenantExists(int id) {
			return _db.BuildingToTenants.Count(e => e.BuildingId == id) > 0;
		}
	}
}