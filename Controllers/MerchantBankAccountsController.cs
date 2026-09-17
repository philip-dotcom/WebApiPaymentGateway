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
    public class MerchantBankAccountsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/MerchantBankAccounts
        public IQueryable<MerchantBankAccount> GetMerchantBankAccounts()
        {
            return db.MerchantBankAccounts;
        }

        // GET: api/MerchantBankAccounts/5
        [ResponseType(typeof(MerchantBankAccount))]
        public IHttpActionResult GetMerchantBankAccount(int id)
        {
            MerchantBankAccount merchantBankAccount = db.MerchantBankAccounts.Find(id);
            if (merchantBankAccount == null)
            {
                return NotFound();
            }

            return Ok(merchantBankAccount);
        }

        // PUT: api/MerchantBankAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMerchantBankAccount(int id, MerchantBankAccount merchantBankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != merchantBankAccount.BankAccountId)
            {
                return BadRequest();
            }

            db.Entry(merchantBankAccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantBankAccountExists(id))
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

        // POST: api/MerchantBankAccounts
        [ResponseType(typeof(MerchantBankAccount))]
        public IHttpActionResult PostMerchantBankAccount(MerchantBankAccount merchantBankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MerchantBankAccounts.Add(merchantBankAccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = merchantBankAccount.BankAccountId }, merchantBankAccount);
        }

        // DELETE: api/MerchantBankAccounts/5
        [ResponseType(typeof(MerchantBankAccount))]
        public IHttpActionResult DeleteMerchantBankAccount(int id)
        {
            MerchantBankAccount merchantBankAccount = db.MerchantBankAccounts.Find(id);
            if (merchantBankAccount == null)
            {
                return NotFound();
            }

            db.MerchantBankAccounts.Remove(merchantBankAccount);
            db.SaveChanges();

            return Ok(merchantBankAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MerchantBankAccountExists(int id)
        {
            return db.MerchantBankAccounts.Count(e => e.BankAccountId == id) > 0;
        }
    }
}