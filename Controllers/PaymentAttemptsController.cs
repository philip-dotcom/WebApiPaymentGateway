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
    public class PaymentAttemptsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/PaymentAttempts
        public IQueryable<PaymentAttempt> GetPaymentAttempts()
        {
            return db.PaymentAttempts;
        }

        // GET: api/PaymentAttempts/5
        [ResponseType(typeof(PaymentAttempt))]
        public IHttpActionResult GetPaymentAttempt(long id)
        {
            PaymentAttempt paymentAttempt = db.PaymentAttempts.Find(id);
            if (paymentAttempt == null)
            {
                return NotFound();
            }

            return Ok(paymentAttempt);
        }

        // PUT: api/PaymentAttempts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaymentAttempt(long id, PaymentAttempt paymentAttempt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentAttempt.PaymentAttemptId)
            {
                return BadRequest();
            }

            db.Entry(paymentAttempt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentAttemptExists(id))
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

        // POST: api/PaymentAttempts
        [ResponseType(typeof(PaymentAttempt))]
        public IHttpActionResult PostPaymentAttempt(PaymentAttempt paymentAttempt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentAttempts.Add(paymentAttempt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paymentAttempt.PaymentAttemptId }, paymentAttempt);
        }

        // DELETE: api/PaymentAttempts/5
        [ResponseType(typeof(PaymentAttempt))]
        public IHttpActionResult DeletePaymentAttempt(long id)
        {
            PaymentAttempt paymentAttempt = db.PaymentAttempts.Find(id);
            if (paymentAttempt == null)
            {
                return NotFound();
            }

            db.PaymentAttempts.Remove(paymentAttempt);
            db.SaveChanges();

            return Ok(paymentAttempt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentAttemptExists(long id)
        {
            return db.PaymentAttempts.Count(e => e.PaymentAttemptId == id) > 0;
        }
    }
}