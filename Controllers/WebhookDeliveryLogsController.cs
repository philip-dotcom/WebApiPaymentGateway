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
    public class WebhookDeliveryLogsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/WebhookDeliveryLogs
        public IQueryable<WebhookDeliveryLog> GetWebhookDeliveryLogs()
        {
            return db.WebhookDeliveryLogs;
        }

        // GET: api/WebhookDeliveryLogs/5
        [ResponseType(typeof(WebhookDeliveryLog))]
        public IHttpActionResult GetWebhookDeliveryLog(long id)
        {
            WebhookDeliveryLog webhookDeliveryLog = db.WebhookDeliveryLogs.Find(id);
            if (webhookDeliveryLog == null)
            {
                return NotFound();
            }

            return Ok(webhookDeliveryLog);
        }

        // PUT: api/WebhookDeliveryLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebhookDeliveryLog(long id, WebhookDeliveryLog webhookDeliveryLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webhookDeliveryLog.DeliveryId)
            {
                return BadRequest();
            }

            db.Entry(webhookDeliveryLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebhookDeliveryLogExists(id))
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

        // POST: api/WebhookDeliveryLogs
        [ResponseType(typeof(WebhookDeliveryLog))]
        public IHttpActionResult PostWebhookDeliveryLog(WebhookDeliveryLog webhookDeliveryLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebhookDeliveryLogs.Add(webhookDeliveryLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webhookDeliveryLog.DeliveryId }, webhookDeliveryLog);
        }

        // DELETE: api/WebhookDeliveryLogs/5
        [ResponseType(typeof(WebhookDeliveryLog))]
        public IHttpActionResult DeleteWebhookDeliveryLog(long id)
        {
            WebhookDeliveryLog webhookDeliveryLog = db.WebhookDeliveryLogs.Find(id);
            if (webhookDeliveryLog == null)
            {
                return NotFound();
            }

            db.WebhookDeliveryLogs.Remove(webhookDeliveryLog);
            db.SaveChanges();

            return Ok(webhookDeliveryLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebhookDeliveryLogExists(long id)
        {
            return db.WebhookDeliveryLogs.Count(e => e.DeliveryId == id) > 0;
        }
    }
}