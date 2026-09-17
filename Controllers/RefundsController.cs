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
    public class RefundsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/Refunds
        public IQueryable<Refund> GetRefunds()
        {
            return db.Refunds;
        }

        // GET: api/Refunds/5
        [ResponseType(typeof(Refund))]
        public IHttpActionResult GetRefund(int id)
        {
            Refund refund = db.Refunds.Find(id);
            if (refund == null)
            {
                return NotFound();
            }

            return Ok(refund);
        }

        // PUT: api/Refunds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRefund(int id, Refund refund)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != refund.RefundId)
            {
                return BadRequest();
            }

            db.Entry(refund).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefundExists(id))
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

        // POST: api/Refunds
        [ResponseType(typeof(Refund))]
        public IHttpActionResult PostRefund(Refund refund)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Refunds.Add(refund);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = refund.RefundId }, refund);
        }

        // DELETE: api/Refunds/5
        [ResponseType(typeof(Refund))]
        public IHttpActionResult DeleteRefund(int id)
        {
            Refund refund = db.Refunds.Find(id);
            if (refund == null)
            {
                return NotFound();
            }

            db.Refunds.Remove(refund);
            db.SaveChanges();

            return Ok(refund);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RefundExists(int id)
        {
            return db.Refunds.Count(e => e.RefundId == id) > 0;
        }
    }
}