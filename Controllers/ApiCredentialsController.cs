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
    [Route("~/api/ApiCredentials")]

    public class ApiCredentialsController : ApiController
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: api/ApiCredentials
      [Route("~/api/GetAllApiCredentials")]
       [HttpGet]

        public IQueryable<ApiCredential> GetApiCredentials()
        {
            return db.ApiCredentials;
        }

        // GET: api/ApiCredentials/5
        [Route("~/api/GetApiCredentialById/{id}")]
        [HttpGet]
        [ResponseType(typeof(ApiCredential))]
        public IHttpActionResult GetApiCredential(int id)
        {
            ApiCredential apiCredential = db.ApiCredentials.Find(id);
            if (apiCredential == null)
            {
                return NotFound();
            }

            return Ok(apiCredential);
        }

        // PUT: api/ApiCredentials/5
        [Route("~/api/UpdateApiCredential/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApiCredential(int id, ApiCredential apiCredential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apiCredential.CredentialId)
            {
                return BadRequest();
            }

            db.Entry(apiCredential).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiCredentialExists(id))
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

        // POST: api/ApiCredentials
        [Route("~/api/CreateApiCredential")]
        [HttpPost]
        [ResponseType(typeof(ApiCredential))]
        public IHttpActionResult PostApiCredential(ApiCredential apiCredential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApiCredentials.Add(apiCredential);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = apiCredential.CredentialId }, apiCredential);
        }

        // DELETE: api/ApiCredentials/5
        [Route("~/api/DeleteApiCredential/{id}")]
        [HttpDelete]
        [ResponseType(typeof(ApiCredential))]
        public IHttpActionResult DeleteApiCredential(int id)
        {
            ApiCredential apiCredential = db.ApiCredentials.Find(id);
            if (apiCredential == null)
            {
                return NotFound();
            }

            db.ApiCredentials.Remove(apiCredential);
            db.SaveChanges();

            return Ok(apiCredential);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApiCredentialExists(int id)
        {
            return db.ApiCredentials.Count(e => e.CredentialId == id) > 0;
        }
    }
}