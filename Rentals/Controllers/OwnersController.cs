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
using Rentals.Models;

namespace Rentals.Controllers
{
    public class OwnersController : ApiController
    {
        private HolidaysEntities db = new HolidaysEntities();

        // GET: api/Owners
        [HttpGet]
        [Route("api/Owners")]
        public IHttpActionResult GetOwners()
        {
            return Ok(db.Owners);
        }

        // GET: api/Owners/5
        [ResponseType(typeof(Owner))]
        public IHttpActionResult GetOwner(int id)
        {
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }

        // PUT: api/Owners/5
        [HttpPut]
        [Route("api/Owners/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwner(Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(owner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(owner.OwnerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/Owner")]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult PostOwner(Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Owners.Add(owner);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OwnerExists(owner.OwnerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = owner.OwnerId }, owner);
        }

        [HttpDelete]
        [Route("api/Owners/{id}")]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult DeleteOwner(int id)
        {
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return NotFound();
            }

            db.Owners.Remove(owner);
            db.SaveChanges();

            return Ok(owner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerExists(int id)
        {
            return db.Owners.Count(e => e.OwnerId == id) > 0;
        }
    }
}