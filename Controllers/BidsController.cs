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
    public class BidsController : ApiController
    {
        private BidEntities db = new BidEntities();

        // GET: api/Bids
        public IQueryable<Bid> GetBids()
        {
            return db.Bids;
        }

        // GET: api/Bids/5
        [ResponseType(typeof(Bid))]
        [AllowAnonymous]
        public IQueryable<Bid> GetRoom(string id)
        {
            var details = db.Bids.Where(x => x.user_id == id);

            if (details == null)
            {
                return null;
            }
            return details;
        }

        // PUT: api/Bids/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBid(int id, Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bid.bid_id)
            {
                return BadRequest();
            }

            db.Entry(bid).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
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

        // POST: api/Bids
        [ResponseType(typeof(Bid))]
        [AllowAnonymous]
        public IHttpActionResult PostBid(Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bids.Add(bid);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bid.bid_id }, bid);
        }

        // DELETE: api/Bids/5
        [ResponseType(typeof(Bid))]
        public IHttpActionResult DeleteBid(int id)
        {
            Bid bid = db.Bids.Find(id);
            if (bid == null)
            {
                return NotFound();
            }

            db.Bids.Remove(bid);
            db.SaveChanges();

            return Ok(bid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BidExists(int id)
        {
            return db.Bids.Count(e => e.bid_id == id) > 0;
        }
    }
}