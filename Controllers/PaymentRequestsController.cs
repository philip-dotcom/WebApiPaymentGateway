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
    public class PaymentRequestsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/PaymentRequests
        public IQueryable<PaymentRequest> GetPaymentRequests()
        {
            return db.PaymentRequests;
        }

        // GET: api/PaymentRequests/5
        [ResponseType(typeof(PaymentRequest))]
        public IHttpActionResult GetPaymentRequest(int id)
        {
            PaymentRequest paymentRequest = db.PaymentRequests.Find(id);
            if (paymentRequest == null)
            {
                return NotFound();
            }

            return Ok(paymentRequest);
        }

        // PUT: api/PaymentRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaymentRequest(int id, PaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentRequest.PaymentRequestId)
            {
                return BadRequest();
            }

            db.Entry(paymentRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentRequestExists(id))
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

        // POST: api/PaymentRequests
        [ResponseType(typeof(PaymentRequest))]
        public IHttpActionResult PostPaymentRequest(PaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentRequests.Add(paymentRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paymentRequest.PaymentRequestId }, paymentRequest);
        }

        // DELETE: api/PaymentRequests/5
        [ResponseType(typeof(PaymentRequest))]
        public IHttpActionResult DeletePaymentRequest(int id)
        {
            PaymentRequest paymentRequest = db.PaymentRequests.Find(id);
            if (paymentRequest == null)
            {
                return NotFound();
            }

            db.PaymentRequests.Remove(paymentRequest);
            db.SaveChanges();

            return Ok(paymentRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentRequestExists(int id)
        {
            return db.PaymentRequests.Count(e => e.PaymentRequestId == id) > 0;
        }
    }
}