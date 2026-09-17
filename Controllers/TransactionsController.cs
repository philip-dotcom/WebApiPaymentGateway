using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiPaymentGateway.Models;

namespace WebApiPaymentGateway.Controllers
{
    public class TransactionsController : Controller
    {
        private paymentgatewaydbEntities db = new paymentgatewaydbEntities();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Customer).Include(t => t.Merchant).Include(t => t.PaymentAttempt).Include(t => t.PaymentRequest);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName");
            ViewBag.MerchantId = new SelectList(db.Merchants, "MerchantId", "LegalBusinessName");
            ViewBag.PaymentAttemptId = new SelectList(db.PaymentAttempts, "PaymentAttemptId", "PaymentMethod");
            ViewBag.PaymentRequestId = new SelectList(db.PaymentRequests, "PaymentRequestId", "Reference");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,MerchantId,CustomerId,PaymentRequestId,PaymentAttemptId,Reference,PaymentMethod,Amount,Currency,Status,Environment,OccurredAt")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", transaction.CustomerId);
            ViewBag.MerchantId = new SelectList(db.Merchants, "MerchantId", "LegalBusinessName", transaction.MerchantId);
            ViewBag.PaymentAttemptId = new SelectList(db.PaymentAttempts, "PaymentAttemptId", "PaymentMethod", transaction.PaymentAttemptId);
            ViewBag.PaymentRequestId = new SelectList(db.PaymentRequests, "PaymentRequestId", "Reference", transaction.PaymentRequestId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", transaction.CustomerId);
            ViewBag.MerchantId = new SelectList(db.Merchants, "MerchantId", "LegalBusinessName", transaction.MerchantId);
            ViewBag.PaymentAttemptId = new SelectList(db.PaymentAttempts, "PaymentAttemptId", "PaymentMethod", transaction.PaymentAttemptId);
            ViewBag.PaymentRequestId = new SelectList(db.PaymentRequests, "PaymentRequestId", "Reference", transaction.PaymentRequestId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,MerchantId,CustomerId,PaymentRequestId,PaymentAttemptId,Reference,PaymentMethod,Amount,Currency,Status,Environment,OccurredAt")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", transaction.CustomerId);
            ViewBag.MerchantId = new SelectList(db.Merchants, "MerchantId", "LegalBusinessName", transaction.MerchantId);
            ViewBag.PaymentAttemptId = new SelectList(db.PaymentAttempts, "PaymentAttemptId", "PaymentMethod", transaction.PaymentAttemptId);
            ViewBag.PaymentRequestId = new SelectList(db.PaymentRequests, "PaymentRequestId", "Reference", transaction.PaymentRequestId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
