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
        [Route("~/api/ComplianceDocuments")]

    public class ComplianceDocumentsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/ComplianceDocuments
        [Route("~/api/GetAllComplianceDocuments")]
        [HttpGet]

        public IQueryable<ComplianceDocument> GetComplianceDocuments()
        {
            return db.ComplianceDocuments;
        }

        // GET: api/ComplianceDocuments/5
        [Route("~/api/GetComplianceDocumentById/{id}")]
        [HttpGet]

        [ResponseType(typeof(ComplianceDocument))]
        public IHttpActionResult GetComplianceDocument(int id)
        {
            ComplianceDocument complianceDocument = db.ComplianceDocuments.Find(id);
            if (complianceDocument == null)
            {
                return NotFound();
            }

            return Ok(complianceDocument);
        }

        // PUT: api/ComplianceDocuments/5
        [Route("~/api/UpdateComplianceDocument/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComplianceDocument(int id, ComplianceDocument complianceDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complianceDocument.DocumentId)
            {
                return BadRequest();
            }

            db.Entry(complianceDocument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplianceDocumentExists(id))
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

        // POST: api/ComplianceDocuments
         [Route("~/api/CreateComplianceDocument")]
         [HttpPost]

        [ResponseType(typeof(ComplianceDocument))]
        public IHttpActionResult PostComplianceDocument(ComplianceDocument complianceDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComplianceDocuments.Add(complianceDocument);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complianceDocument.DocumentId }, complianceDocument);
        }

        // DELETE: api/ComplianceDocuments/5
        [Route("~/api/DeleteComplianceDocument/{id}")]
        [HttpDelete]
        [ResponseType(typeof(ComplianceDocument))]
        public IHttpActionResult DeleteComplianceDocument(int id)
        {
            ComplianceDocument complianceDocument = db.ComplianceDocuments.Find(id);
            if (complianceDocument == null)
            {
                return NotFound();
            }

            db.ComplianceDocuments.Remove(complianceDocument);
            db.SaveChanges();

            return Ok(complianceDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplianceDocumentExists(int id)
        {
            return db.ComplianceDocuments.Count(e => e.DocumentId == id) > 0;
        }
    }
}