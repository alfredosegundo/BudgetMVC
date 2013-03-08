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
using BudgetMVC.Model.Business;

namespace BudgetMVC.Controllers
{
    public class ContributionController : ApiController
    {
        private ContributionBusiness contributionBusiness;

        public ContributionController()
        {
            this.contributionBusiness = new ContributionBusiness(new BudgetContext());
        }
        // GET api/Contribution
        public IEnumerable<Contribution> GetContributions()
        {
            return contributionBusiness.FindAll();
        }

        // GET api/Contribution/5
        public Contribution GetContribution(long id)
        {
            Contribution contribution = contributionBusiness.Find(id);
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
                try
                {
                    contributionBusiness.Update(contribution);
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
                contributionBusiness.Insert(contribution);
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
            Contribution removedContribution;
            try
            {
                removedContribution = contributionBusiness.RemoveById(id);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, removedContribution);
        }

        protected override void Dispose(bool disposing)
        {
            contributionBusiness.Dispose();
            base.Dispose(disposing);
        }
    }
}