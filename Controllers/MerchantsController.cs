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
    [Microsoft.AspNetCore.Mvc.Produces("application/json")]
    [Route("~/api/Merchants")]
    public class MerchantsController : ApiController
    {
        private MerchantPayLiteEntities db = new MerchantPayLiteEntities();

        // GET: api/Merchants

        [Route("~/api/GetAllMerchants")]
        public IQueryable<Merchant> GetMerchants()
        {
            return db.Merchants;
        }

        // GET: api/Merchants/5

        [Route("~/api/GetMerchantById/{id}")]   
        [ResponseType(typeof(Merchant))]
        [HttpGet]
        public IHttpActionResult GetMerchant(int id)
        {
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return NotFound();
            }

            return Ok(merchant);
        }

        // PUT: api/Merchants/5


        [Route("~/api/UpdateMerchant/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMerchant(int id, Merchant merchant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != merchant.MerchantID)
            {
                return BadRequest();
            }

            db.Entry(merchant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantExists(id))
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

        // POST: api/Merchants

        [Route("~/api/CreateMerchant")]
        [HttpPost]
        [ResponseType(typeof(Merchant))]
        public IHttpActionResult PostMerchant(Merchant merchant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Merchants.Add(merchant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = merchant.MerchantID }, merchant);
        }

        // DELETE: api/Merchants/5
        [Route("~/api/DeleteMerchant/{id}")]
        [HttpDelete]
        [ResponseType(typeof(Merchant))]
        public IHttpActionResult DeleteMerchant(int id)
        {
            Merchant merchant = db.Merchants.Find(id);
            if (merchant == null)
            {
                return NotFound();
            }

            db.Merchants.Remove(merchant);
            db.SaveChanges();

            return Ok(merchant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MerchantExists(int id)
        {
            return db.Merchants.Count(e => e.MerchantID == id) > 0;
        }
    }
}