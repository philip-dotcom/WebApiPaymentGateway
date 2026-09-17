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
    public class WalletLedgerEntriesController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/WalletLedgerEntries
        public IQueryable<WalletLedgerEntry> GetWalletLedgerEntries()
        {
            return db.WalletLedgerEntries;
        }

        // GET: api/WalletLedgerEntries/5
        [ResponseType(typeof(WalletLedgerEntry))]
        public IHttpActionResult GetWalletLedgerEntry(long id)
        {
            WalletLedgerEntry walletLedgerEntry = db.WalletLedgerEntries.Find(id);
            if (walletLedgerEntry == null)
            {
                return NotFound();
            }

            return Ok(walletLedgerEntry);
        }

        // PUT: api/WalletLedgerEntries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWalletLedgerEntry(long id, WalletLedgerEntry walletLedgerEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != walletLedgerEntry.LedgerEntryId)
            {
                return BadRequest();
            }

            db.Entry(walletLedgerEntry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletLedgerEntryExists(id))
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

        // POST: api/WalletLedgerEntries
        [ResponseType(typeof(WalletLedgerEntry))]
        public IHttpActionResult PostWalletLedgerEntry(WalletLedgerEntry walletLedgerEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WalletLedgerEntries.Add(walletLedgerEntry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = walletLedgerEntry.LedgerEntryId }, walletLedgerEntry);
        }

        // DELETE: api/WalletLedgerEntries/5
        [ResponseType(typeof(WalletLedgerEntry))]
        public IHttpActionResult DeleteWalletLedgerEntry(long id)
        {
            WalletLedgerEntry walletLedgerEntry = db.WalletLedgerEntries.Find(id);
            if (walletLedgerEntry == null)
            {
                return NotFound();
            }

            db.WalletLedgerEntries.Remove(walletLedgerEntry);
            db.SaveChanges();

            return Ok(walletLedgerEntry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WalletLedgerEntryExists(long id)
        {
            return db.WalletLedgerEntries.Count(e => e.LedgerEntryId == id) > 0;
        }
    }
}