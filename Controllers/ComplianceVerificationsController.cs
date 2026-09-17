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
    public class ComplianceVerificationsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/ComplianceVerifications
        public IQueryable<ComplianceVerification> GetComplianceVerifications()
        {
            return db.ComplianceVerifications;
        }

        // GET: api/ComplianceVerifications/5
        [ResponseType(typeof(ComplianceVerification))]
        public IHttpActionResult GetComplianceVerification(int id)
        {
            ComplianceVerification complianceVerification = db.ComplianceVerifications.Find(id);
            if (complianceVerification == null)
            {
                return NotFound();
            }

            return Ok(complianceVerification);
        }

        // PUT: api/ComplianceVerifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComplianceVerification(int id, ComplianceVerification complianceVerification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complianceVerification.VerificationId)
            {
                return BadRequest();
            }

            db.Entry(complianceVerification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplianceVerificationExists(id))
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

        // POST: api/ComplianceVerifications
        [ResponseType(typeof(ComplianceVerification))]
        public IHttpActionResult PostComplianceVerification(ComplianceVerification complianceVerification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComplianceVerifications.Add(complianceVerification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complianceVerification.VerificationId }, complianceVerification);
        }

        // DELETE: api/ComplianceVerifications/5
        [ResponseType(typeof(ComplianceVerification))]
        public IHttpActionResult DeleteComplianceVerification(int id)
        {
            ComplianceVerification complianceVerification = db.ComplianceVerifications.Find(id);
            if (complianceVerification == null)
            {
                return NotFound();
            }

            db.ComplianceVerifications.Remove(complianceVerification);
            db.SaveChanges();

            return Ok(complianceVerification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplianceVerificationExists(int id)
        {
            return db.ComplianceVerifications.Count(e => e.VerificationId == id) > 0;
        }
    }
}