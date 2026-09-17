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
    public class WebhookEndpointsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/WebhookEndpoints
        public IQueryable<WebhookEndpoint> GetWebhookEndpoints()
        {
            return db.WebhookEndpoints;
        }

        // GET: api/WebhookEndpoints/5
        [ResponseType(typeof(WebhookEndpoint))]
        public IHttpActionResult GetWebhookEndpoint(int id)
        {
            WebhookEndpoint webhookEndpoint = db.WebhookEndpoints.Find(id);
            if (webhookEndpoint == null)
            {
                return NotFound();
            }

            return Ok(webhookEndpoint);
        }

        // PUT: api/WebhookEndpoints/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebhookEndpoint(int id, WebhookEndpoint webhookEndpoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webhookEndpoint.WebhookId)
            {
                return BadRequest();
            }

            db.Entry(webhookEndpoint).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebhookEndpointExists(id))
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

        // POST: api/WebhookEndpoints
        [ResponseType(typeof(WebhookEndpoint))]
        public IHttpActionResult PostWebhookEndpoint(WebhookEndpoint webhookEndpoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebhookEndpoints.Add(webhookEndpoint);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webhookEndpoint.WebhookId }, webhookEndpoint);
        }

        // DELETE: api/WebhookEndpoints/5
        [ResponseType(typeof(WebhookEndpoint))]
        public IHttpActionResult DeleteWebhookEndpoint(int id)
        {
            WebhookEndpoint webhookEndpoint = db.WebhookEndpoints.Find(id);
            if (webhookEndpoint == null)
            {
                return NotFound();
            }

            db.WebhookEndpoints.Remove(webhookEndpoint);
            db.SaveChanges();

            return Ok(webhookEndpoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebhookEndpointExists(int id)
        {
            return db.WebhookEndpoints.Count(e => e.WebhookId == id) > 0;
        }
    }
}