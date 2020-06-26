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
    public class HomeRentalsController : ApiController
    {
        private HolidaysEntities db = new HolidaysEntities();

        [Route("api/HomeRentals")]
        public IQueryable<HomeRental> GetHomeRentals()
        {
            return db.HomeRentals;
        }

        [Route("api/HomeRentals/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllHomeRentalsByOwnerId(int id)
        {
            IEnumerable<HomeRental> homeRental = db.HomeRentals.Where(s => s.OwnerId == id).ToList();

            if (homeRental == null)
            {
                return NotFound();
            }

            return Ok(homeRental);
        }

        // PUT: api/HomeRentals/5
        [Route]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHomeRental(int id, HomeRental homeRental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != homeRental.HomeId)
            {
                return BadRequest();
            }

            db.Entry(homeRental).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeRentalExists(id))
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

        // POST: api/HomeRentals
        [HttpPost]
        [Route("api/HomeRentals")]
        [ResponseType(typeof(HomeRental))]
        public IHttpActionResult PostHomeRental(HomeRental homeRental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HomeRentals.Add(homeRental);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HomeRentalExists(homeRental.HomeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = homeRental.HomeId }, homeRental);
        }

        // DELETE: api/HomeRentals/5
        [ResponseType(typeof(HomeRental))]
        public IHttpActionResult DeleteHomeRental(int id)
        {
            HomeRental homeRental = db.HomeRentals.Find(id);
            if (homeRental == null)
            {
                return NotFound();
            }

            db.HomeRentals.Remove(homeRental);
            db.SaveChanges();

            return Ok(homeRental);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HomeRentalExists(int id)
        {
            return db.HomeRentals.Count(e => e.HomeId == id) > 0;
        }
    }
}