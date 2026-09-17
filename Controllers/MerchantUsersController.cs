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
    public class MerchantUsersController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/MerchantUsers
        public IQueryable<MerchantUser> GetMerchantUsers()
        {
            return db.MerchantUsers;
        }

        // GET: api/MerchantUsers/5
        [ResponseType(typeof(MerchantUser))]
        public IHttpActionResult GetMerchantUser(int id)
        {
            MerchantUser merchantUser = db.MerchantUsers.Find(id);
            if (merchantUser == null)
            {
                return NotFound();
            }

            return Ok(merchantUser);
        }

        // PUT: api/MerchantUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMerchantUser(int id, MerchantUser merchantUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != merchantUser.MerchantUserId)
            {
                return BadRequest();
            }

            db.Entry(merchantUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantUserExists(id))
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

        // POST: api/MerchantUsers
        [ResponseType(typeof(MerchantUser))]
        public IHttpActionResult PostMerchantUser(MerchantUser merchantUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MerchantUsers.Add(merchantUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = merchantUser.MerchantUserId }, merchantUser);
        }

        // DELETE: api/MerchantUsers/5
        [ResponseType(typeof(MerchantUser))]
        public IHttpActionResult DeleteMerchantUser(int id)
        {
            MerchantUser merchantUser = db.MerchantUsers.Find(id);
            if (merchantUser == null)
            {
                return NotFound();
            }

            db.MerchantUsers.Remove(merchantUser);
            db.SaveChanges();

            return Ok(merchantUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MerchantUserExists(int id)
        {
            return db.MerchantUsers.Count(e => e.MerchantUserId == id) > 0;
        }
    }
}