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
    public class RoomsController : ApiController
    {
        private RoomEntities db = new RoomEntities();

        // GET: api/Rooms
        [AllowAnonymous]
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }
        
        // GET: api/Rooms/5
        // Get all the records of a single hotel manager by Id 
        [ResponseType(typeof(Room))]
        [AllowAnonymous]
        public IQueryable<Room> GetRoom(string id)
        {
            var details = db.Rooms.Where(x => x.hotelMan_id == id);

            if (details == null)
            {
                return null;
            }
            return details;
        }

   

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        [AllowAnonymous]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.room_id)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        [AllowAnonymous]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.room_id }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        [AllowAnonymous]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.room_id == id) > 0;
        }
    }
}