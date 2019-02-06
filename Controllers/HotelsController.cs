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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HotelsController : ApiController
    {
        private HotelEntities db = new HotelEntities();

        // GET: api/Hotels
        [AllowAnonymous]
        public IQueryable<Hotel> GetHotels()
        {
            return db.Hotels;
        }

        // GET: api/Hotels/5
        [ResponseType(typeof(Hotel))]
        [AllowAnonymous]

        public IQueryable<Hotel> GetHotelProvince(string prov)
        {
            var search = db.Hotels.Where(x => x.Province == prov);
            if (search == null)
            {
                return null;
            }
            return search;
        }
        [AllowAnonymous]
        public IQueryable<Hotel> GetHotelDetail(string prov,string city,int rate)
        {
            var search = db.Hotels.Where(x => x.Province == prov && x.City == city && x.rating == rate);
            if (search == null)
            {
                return null;
            }
            return search;
        }


        // PUT: api/Hotels/5
        [ResponseType(typeof(void))]
        [AllowAnonymous]
        public IHttpActionResult PutHotel(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.hotel_id)
            {
                return BadRequest();
            }

            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        [ResponseType(typeof(Hotel))]
        [AllowAnonymous]
        public IHttpActionResult PostHotel(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotels.Add(hotel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotel.hotel_id }, hotel);
        }

        // DELETE: api/Hotels/5
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult DeleteHotel(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotel);
            db.SaveChanges();

            return Ok(hotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelExists(int id)
        {
            return db.Hotels.Count(e => e.hotel_id == id) > 0;
        }
    }
}