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
    public class BankDetailsController : ApiController
    {
        private BankDetailsEntities db = new BankDetailsEntities();

        // GET: api/BankDetails
        public IQueryable<BankDetail> GetBankDetails()
        {
            return db.BankDetails;
        }

        // GET: api/BankDetails/5
        [ResponseType(typeof(BankDetail))]
        [AllowAnonymous]
        public IQueryable<BankDetail> GetRoom(string id)
        {
            var details = db.BankDetails.Where(x => x.user_id == id);

            if (details == null)
            {
                return null;
            }
            return details;
        }

        // PUT: api/BankDetails/5
        [ResponseType(typeof(void))]
        [AllowAnonymous]
        public IHttpActionResult PutBankDetail(int id, BankDetail bankDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankDetail.bank_id)
            {
                return BadRequest();
            }

            db.Entry(bankDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDetailExists(id))
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

        // POST: api/BankDetails
        [ResponseType(typeof(BankDetail))]
        [AllowAnonymous]
        public IHttpActionResult PostBankDetail(BankDetail bankDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BankDetails.Add(bankDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bankDetail.bank_id }, bankDetail);
        }

        // DELETE: api/BankDetails/5
        [ResponseType(typeof(BankDetail))]
        public IHttpActionResult DeleteBankDetail(int id)
        {
            BankDetail bankDetail = db.BankDetails.Find(id);
            if (bankDetail == null)
            {
                return NotFound();
            }

            db.BankDetails.Remove(bankDetail);
            db.SaveChanges();

            return Ok(bankDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankDetailExists(int id)
        {
            return db.BankDetails.Count(e => e.bank_id == id) > 0;
        }
    }
}