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
using WebApiPaymentGateway.Models;

namespace WebApiPaymentGateway.Controllers
{
    public class PayoutsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/Payouts
        public IQueryable<Payout> GetPayouts()
        {
            return db.Payouts;
        }

        // GET: api/Payouts/5
        [ResponseType(typeof(Payout))]
        public IHttpActionResult GetPayout(int id)
        {
            Payout payout = db.Payouts.Find(id);
            if (payout == null)
            {
                return NotFound();
            }

            return Ok(payout);
        }

        // PUT: api/Payouts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayout(int id, Payout payout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payout.PayoutId)
            {
                return BadRequest();
            }

            db.Entry(payout).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayoutExists(id))
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

        // POST: api/Payouts
        [ResponseType(typeof(Payout))]
        public IHttpActionResult PostPayout(Payout payout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payouts.Add(payout);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payout.PayoutId }, payout);
        }

        // DELETE: api/Payouts/5
        [ResponseType(typeof(Payout))]
        public IHttpActionResult DeletePayout(int id)
        {
            Payout payout = db.Payouts.Find(id);
            if (payout == null)
            {
                return NotFound();
            }

            db.Payouts.Remove(payout);
            db.SaveChanges();

            return Ok(payout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayoutExists(int id)
        {
            return db.Payouts.Count(e => e.PayoutId == id) > 0;
        }
    }
}