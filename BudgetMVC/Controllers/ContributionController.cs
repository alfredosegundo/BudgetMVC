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
    public class ContributionController : ApiController
    {
        private BudgetContext db = new BudgetContext();

        // GET api/Contribution
        public IEnumerable<Contribution> GetContributions()
        {
            return db.Contributions.AsEnumerable();
        }

        // GET api/Contribution/5
        public Contribution GetContribution(long id)
        {
            Contribution contribution = db.Contributions.Find(id);
            if (contribution == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contribution;
        }

        // PUT api/Contribution/5
        public HttpResponseMessage PutContribution(long id, Contribution contribution)
        {
            if (ModelState.IsValid && id == contribution.ID)
            {
                db.Entry(contribution).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, contribution);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Contribution
        public HttpResponseMessage PostContribution(Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                db.Contributions.Add(contribution);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contribution);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contribution.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Contribution/5
        public HttpResponseMessage DeleteContribution(long id)
        {
            Contribution contribution = db.Contributions.Find(id);
            if (contribution == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Contributions.Remove(contribution);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contribution);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}