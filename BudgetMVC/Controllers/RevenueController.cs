using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BudgetMVC.Model.Entity;
using BudgetMVC.Model.EntityFramework;

namespace BudgetMVC.Controllers
{
    public class RevenueController : ApiController
    {
        private BudgetContext db = new BudgetContext();

        // GET api/Revenue
        public IEnumerable<Revenue> GetRevenues()
        {
            return db.Revenues.AsEnumerable();
        }

        // GET api/Revenue/5
        public Revenue GetRevenue(long id)
        {
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return revenue;
        }

        // PUT api/Revenue/5
        public HttpResponseMessage PutRevenue(long id, Revenue revenue)
        {
            if (ModelState.IsValid && id == revenue.ID)
            {
                db.Entry(revenue).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, revenue);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Revenue
        public HttpResponseMessage PostRevenue(Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                db.Revenues.Add(revenue);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, revenue);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = revenue.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Revenue/5
        public HttpResponseMessage DeleteRevenue(long id)
        {
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Revenues.Remove(revenue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, revenue);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}