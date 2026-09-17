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
    public class FraudFlagsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/FraudFlags
        public IQueryable<FraudFlag> GetFraudFlags()
        {
            return db.FraudFlags;
        }

        // GET: api/FraudFlags/5
        [ResponseType(typeof(FraudFlag))]
        public IHttpActionResult GetFraudFlag(int id)
        {
            FraudFlag fraudFlag = db.FraudFlags.Find(id);
            if (fraudFlag == null)
            {
                return NotFound();
            }

            return Ok(fraudFlag);
        }

        // PUT: api/FraudFlags/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFraudFlag(int id, FraudFlag fraudFlag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fraudFlag.FlagId)
            {
                return BadRequest();
            }

            db.Entry(fraudFlag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FraudFlagExists(id))
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

        // POST: api/FraudFlags
        [ResponseType(typeof(FraudFlag))]
        public IHttpActionResult PostFraudFlag(FraudFlag fraudFlag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FraudFlags.Add(fraudFlag);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fraudFlag.FlagId }, fraudFlag);
        }

        // DELETE: api/FraudFlags/5
        [ResponseType(typeof(FraudFlag))]
        public IHttpActionResult DeleteFraudFlag(int id)
        {
            FraudFlag fraudFlag = db.FraudFlags.Find(id);
            if (fraudFlag == null)
            {
                return NotFound();
            }

            db.FraudFlags.Remove(fraudFlag);
            db.SaveChanges();

            return Ok(fraudFlag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FraudFlagExists(int id)
        {
            return db.FraudFlags.Count(e => e.FlagId == id) > 0;
        }
    }
}