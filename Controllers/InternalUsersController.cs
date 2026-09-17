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
    public class InternalUsersController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/InternalUsers
        public IQueryable<InternalUser> GetInternalUsers()
        {
            return db.InternalUsers;
        }

        // GET: api/InternalUsers/5
        [ResponseType(typeof(InternalUser))]
        public IHttpActionResult GetInternalUser(int id)
        {
            InternalUser internalUser = db.InternalUsers.Find(id);
            if (internalUser == null)
            {
                return NotFound();
            }

            return Ok(internalUser);
        }

        // PUT: api/InternalUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInternalUser(int id, InternalUser internalUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != internalUser.InternalUserId)
            {
                return BadRequest();
            }

            db.Entry(internalUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternalUserExists(id))
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

        // POST: api/InternalUsers
        [ResponseType(typeof(InternalUser))]
        public IHttpActionResult PostInternalUser(InternalUser internalUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InternalUsers.Add(internalUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = internalUser.InternalUserId }, internalUser);
        }

        // DELETE: api/InternalUsers/5
        [ResponseType(typeof(InternalUser))]
        public IHttpActionResult DeleteInternalUser(int id)
        {
            InternalUser internalUser = db.InternalUsers.Find(id);
            if (internalUser == null)
            {
                return NotFound();
            }

            db.InternalUsers.Remove(internalUser);
            db.SaveChanges();

            return Ok(internalUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InternalUserExists(int id)
        {
            return db.InternalUsers.Count(e => e.InternalUserId == id) > 0;
        }
    }
}