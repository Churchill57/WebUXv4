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
        private AppRegister.DataAccess.AppRegisterDBContext _dbContext { get; }
        public DomainsController(GOLD.AppRegister.DataAccess.AppRegisterDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Domains
        public IQueryable<Domain> GetDomains()
        {
            return from d in _dbContext.Domains
                   select new Domain { ID = d.ID, Name = d.Name, Description = d.Description };

        }

        // GET: api/Domains/5
        [ResponseType(typeof(Domain))]
        public async Task<IHttpActionResult> GetDomain(string id)
        {
            Guid guid = new Guid(id);
            var d = await _dbContext.Domains.FindAsync(guid);
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

            _dbContext.Entry(domain).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
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

            _dbContext.Domains.Add(new GOLD.AppRegister.DataAccess.Domain { ID = domain.ID, Name = domain.Name, Description = domain.Description });
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = domain.ID }, domain);
        }

        // DELETE: api/Domains/5
        [ResponseType(typeof(Domain))]
        public async Task<IHttpActionResult> DeleteDomain(int id)
        {
            var d = await _dbContext.Domains.FindAsync(id);
            if (d == null)
            {
                return NotFound();
            }

            _dbContext.Domains.Remove(new GOLD.AppRegister.DataAccess.Domain { ID = d.ID, Name = d.Name, Description = d.Description });
            await _dbContext.SaveChangesAsync();

            return Ok(d);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DomainExists(Guid id)
        {
            return _dbContext.Domains.Count(e => e.ID == id) > 0;
        }
    }
}