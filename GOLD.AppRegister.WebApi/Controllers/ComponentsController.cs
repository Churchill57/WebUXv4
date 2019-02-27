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
using GOLD.AppRegister.ApiModels;
using GOLD.AppRegister.DataAccess;

namespace GOLD.AppRegister.WebApi.Controllers
{
    public class ComponentsController : ApiController
    {
        private AppRegister.DataAccess.AppRegisterDBContext _db { get; }
        public ComponentsController(GOLD.AppRegister.DataAccess.AppRegisterDBContext db)
        {
            _db = db;
        }

        // GET: api/Components
        public IQueryable<Component> GetComponents()
        {
            return _db.Components;
        }

        // GET: api/Components/5
        [ResponseType(typeof(Component))]
        public async Task<IHttpActionResult> GetComponent(int id)
        {
            Component component = await _db.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            return Ok(component);
        }

        // GET: api/Components/5
        //[Route("{id:string}")]
        [ResponseType(typeof(ComponentByInterfaceFullName))]
        public async Task<IHttpActionResult> GetComponentByInterfaceFullName(string id)
        {
            var componentByInterfaceFullName = await (
                from c in _db.Components
                join d in _db.Domains on c.DomainID equals d.ID
                where c.InterfaceFullname == id
                select new GOLD.AppRegister.ApiModels.ComponentByInterfaceFullName()
                {
                    ID = c.ID,
                    DomainID = d.ID,
                    DomainName = d.Name,
                    InterfaceFullname = c.InterfaceFullname,
                    IsPrimaryApp = c.IsPrimaryApp,
                    IsSecondaryApp = c.IsSecondaryApp,
                    PrimaryAppRoute = c.PrimaryAppRoute,
                    SecondaryAppRoute = c.SecondaryAppRoute,
                    Title = c.Title
                }
            ).FirstOrDefaultAsync();

            if (componentByInterfaceFullName == null)
            {
                componentByInterfaceFullName = new GOLD.AppRegister.ApiModels.ComponentByInterfaceFullName() { InterfaceFullname = "==>" + id };
                return Ok(componentByInterfaceFullName);
                //return NotFound();
            }
            return Ok(componentByInterfaceFullName);
        }

        // PUT: api/Components/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComponent(int id, Component component)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != component.ID)
            {
                return BadRequest();
            }

            _db.Entry(component).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // POST: api/Components
        [ResponseType(typeof(Component))]
        public async Task<IHttpActionResult> PostComponent(Component component)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Components.Add(component);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = component.ID }, component);
        }

        // DELETE: api/Components/5
        [ResponseType(typeof(Component))]
        public async Task<IHttpActionResult> DeleteComponent(int id)
        {
            Component component = await _db.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            _db.Components.Remove(component);
            await _db.SaveChangesAsync();

            return Ok(component);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComponentExists(int id)
        {
            return _db.Components.Count(e => e.ID == id) > 0;
        }
    }
}