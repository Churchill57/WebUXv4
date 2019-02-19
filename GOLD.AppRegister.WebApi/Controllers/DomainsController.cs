using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
//using GOLD.AppRegister.DataAccess;
using GOLD.AppRegister.ApiModels;

namespace GOLD.AppRegisterAPI.Controllers
{
    public class DomainsController : ApiController
    {
        private AppRegister.DataAccess.AppRegisterDBContext _db { get; }
        public DomainsController(GOLD.AppRegister.DataAccess.AppRegisterDBContext db)
        {
            _db = db;
        }

        // GET: api/Domains
        public IQueryable<Domain> GetDomains()
        {
            return from d in _db.Domains
                   select new Domain { ID = d.ID, Name = d.Name, Description = d.Description };

        }

        // GET: api/Domains/5
        [ResponseType(typeof(Domain))]
        public async Task<IHttpActionResult> GetDomain(int id)
        {
            var d = await _db.Domains.FindAsync(id);
            if (d == null)
            {
                return NotFound();
            }

            return Ok(new Domain { ID = d.ID, Name = d.Name, Description = d.Description });
        }

        // PUT: api/Domains/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDomain(Guid id, Domain domain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != domain.ID)
            {
                return BadRequest();
            }

            _db.Entry(domain).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DomainExists(id))
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

        // POST: api/Domains
        [ResponseType(typeof(Domain))]
        public async Task<IHttpActionResult> PostDomain(Domain domain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Domains.Add(new GOLD.AppRegister.DataAccess.Domain { ID = domain.ID, Name = domain.Name, Description = domain.Description });
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = domain.ID }, domain);
        }

        // DELETE: api/Domains/5
        [ResponseType(typeof(Domain))]
        public async Task<IHttpActionResult> DeleteDomain(int id)
        {
            var d = await _db.Domains.FindAsync(id);
            if (d == null)
            {
                return NotFound();
            }

            _db.Domains.Remove(new GOLD.AppRegister.DataAccess.Domain { ID = d.ID, Name = d.Name, Description = d.Description });
            await _db.SaveChangesAsync();

            return Ok(d);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DomainExists(Guid id)
        {
            return _db.Domains.Count(e => e.ID == id) > 0;
        }
    }
}