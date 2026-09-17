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
    public class PayoutTransactionsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/PayoutTransactions
        public IQueryable<PayoutTransaction> GetPayoutTransactions()
        {
            return db.PayoutTransactions;
        }

        // GET: api/PayoutTransactions/5
        [ResponseType(typeof(PayoutTransaction))]
        public IHttpActionResult GetPayoutTransaction(long id)
        {
            PayoutTransaction payoutTransaction = db.PayoutTransactions.Find(id);
            if (payoutTransaction == null)
            {
                return NotFound();
            }

            return Ok(payoutTransaction);
        }

        // PUT: api/PayoutTransactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayoutTransaction(long id, PayoutTransaction payoutTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payoutTransaction.PayoutTransactionId)
            {
                return BadRequest();
            }

            db.Entry(payoutTransaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayoutTransactionExists(id))
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

        // POST: api/PayoutTransactions
        [ResponseType(typeof(PayoutTransaction))]
        public IHttpActionResult PostPayoutTransaction(PayoutTransaction payoutTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PayoutTransactions.Add(payoutTransaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payoutTransaction.PayoutTransactionId }, payoutTransaction);
        }

        // DELETE: api/PayoutTransactions/5
        [ResponseType(typeof(PayoutTransaction))]
        public IHttpActionResult DeletePayoutTransaction(long id)
        {
            PayoutTransaction payoutTransaction = db.PayoutTransactions.Find(id);
            if (payoutTransaction == null)
            {
                return NotFound();
            }

            db.PayoutTransactions.Remove(payoutTransaction);
            db.SaveChanges();

            return Ok(payoutTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayoutTransactionExists(long id)
        {
            return db.PayoutTransactions.Count(e => e.PayoutTransactionId == id) > 0;
        }
    }
}