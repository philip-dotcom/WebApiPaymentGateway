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
    [Route("~/api/AuditLogs")]


    public class AuditLogsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/AuditLogs
          [Route("~/api/GetAllAuditLogs")]
          [HttpGet]

        public IQueryable<AuditLog> GetAuditLogs()
        {
            return db.AuditLogs;
        }

        // GET: api/AuditLogs/5
        [Route("~/api/GetAuditLogById/{id}")]
        [HttpGet]

        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult GetAuditLog(long id)
        {
            AuditLog auditLog = db.AuditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return Ok(auditLog);
        }

        // PUT: api/AuditLogs/5
        [Route("~/api/UpdateAuditLog/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuditLog(long id, AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditLog.AuditLogId)
            {
                return BadRequest();
            }

            db.Entry(auditLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(id))
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

        // POST: api/AuditLogs
        [Route("~/api/CreateAuditLog")]
        [HttpPost]

        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult PostAuditLog(AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuditLogs.Add(auditLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = auditLog.AuditLogId }, auditLog);
        }

        // DELETE: api/AuditLogs/5
        [Route("~/api/DeleteAuditLog/{id}")]
        [HttpDelete]
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult DeleteAuditLog(long id)
        {
            AuditLog auditLog = db.AuditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            db.AuditLogs.Remove(auditLog);
            db.SaveChanges();

            return Ok(auditLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditLogExists(long id)
        {
            return db.AuditLogs.Count(e => e.AuditLogId == id) > 0;
        }
    }
}