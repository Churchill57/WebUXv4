using GOLD.CustomerDomain.ApiModels;
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
//using GOLD.CustomerDomain.DataAccess;

namespace GOLD.CustomerDomain.WebApi.Controllers
{
    public class CustomersSyncController : ApiController
    {
        private GOLD.CustomerDomain.DataAccess.CustomerDomainDBContext db = new GOLD.CustomerDomain.DataAccess.CustomerDomainDBContext();

        // GET: api/Customers
        public IQueryable<Customer> GetCustomers()
        {
            return from c in db.Customers
                   select new Customer { ID = c.ID, DOB = c.DOB, FirstName = c.FirstName, Gender = c.Gender, LastName = c.LastName, NINO = c.NINO, Title = c.Title };
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            var c = db.Customers.Find(id);
            var customer = new Customer { ID = c.ID, DOB = c.DOB, FirstName = c.FirstName, Gender = c.Gender, LastName = c.LastName, NINO = c.NINO, Title = c.Title };
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c.ID)
            {
                return BadRequest();
            }

            var customer = new GOLD.CustomerDomain.DataAccess.Customer { ID = c.ID, DOB = c.DOB, FirstName = c.FirstName, Gender = c.Gender, LastName = c.LastName, NINO = c.NINO, Title = c.Title };
            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = new GOLD.CustomerDomain.DataAccess.Customer { ID = c.ID, DOB = c.DOB, FirstName = c.FirstName, Gender = c.Gender, LastName = c.LastName, NINO = c.NINO, Title = c.Title };
            db.Customers.Add(customer);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.ID }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.ID == id) > 0;
        }
    }
}