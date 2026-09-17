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
    public class WebhookSubscriptionsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/WebhookSubscriptions
        [Route("~/api/GetAllWebhookSubscriptions")]
        [HttpGet]   
        public IQueryable<WebhookSubscription> GetWebhookSubscriptions()
        {
            return db.WebhookSubscriptions;
        }

        // GET: api/WebhookSubscriptions/5
        [Route("~/api/GetWebhookSubscriptionById/{id}")]
        [HttpGet]

        [ResponseType(typeof(WebhookSubscription))]
        public IHttpActionResult GetWebhookSubscription(int id)
        {
            WebhookSubscription webhookSubscription = db.WebhookSubscriptions.Find(id);
            if (webhookSubscription == null)
            {
                return NotFound();
            }

            return Ok(webhookSubscription);
        }

        // PUT: api/WebhookSubscriptions/5
        [Route("~/api/UpdateWebhookSubscription/{id}")]
        [HttpPut]

        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebhookSubscription(int id, WebhookSubscription webhookSubscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webhookSubscription.SubscriptionId)
            {
                return BadRequest();
            }

            db.Entry(webhookSubscription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebhookSubscriptionExists(id))
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

        // POST: api/WebhookSubscriptions
        [Route("~/api/CreateWebhookSubscription")]
        [HttpPost]
        [ResponseType(typeof(WebhookSubscription))]
        public IHttpActionResult PostWebhookSubscription(WebhookSubscription webhookSubscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebhookSubscriptions.Add(webhookSubscription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webhookSubscription.SubscriptionId }, webhookSubscription);
        }

        // DELETE: api/WebhookSubscriptions/5
        [Route("~/api/DeleteWebhookSubscription/{id}")]
        [HttpDelete]
        [ResponseType(typeof(WebhookSubscription))]

        public IHttpActionResult DeleteWebhookSubscription(int id)
        {
            WebhookSubscription webhookSubscription = db.WebhookSubscriptions.Find(id);
            if (webhookSubscription == null)
            {
                return NotFound();
            }

            db.WebhookSubscriptions.Remove(webhookSubscription);
            db.SaveChanges();

            return Ok(webhookSubscription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebhookSubscriptionExists(int id)
        {
            return db.WebhookSubscriptions.Count(e => e.SubscriptionId == id) > 0;
        }
    }
}