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
using GOLD.AppExecution.DataAccess;

namespace GOLD.AppExecution.WebApi.Controllers
{
    public class ExecutionThreadsController : ApiController
    {
        private AppExecutionDBContext db = new AppExecutionDBContext();

        // GET: api/ExecutionThreads
        public IQueryable<ExecutionThread> GetExecutionThreads()
        {
            return db.ExecutionThreads;
        }

        // GET: api/ExecutionThreads/5
        [ResponseType(typeof(ExecutionThread))]
        public IHttpActionResult GetExecutionThread(int id)
        {
            ExecutionThread executionThread = db.ExecutionThreads.Find(id);
            if (executionThread == null)
            {
                return NotFound();
            }

            return Ok(executionThread);
        }

        // PUT: api/ExecutionThreads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExecutionThread(int id, ExecutionThread executionThread)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != executionThread.ID)
            {
                return BadRequest();
            }

            db.Entry(executionThread).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExecutionThreadExists(id))
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

        // POST: api/ExecutionThreads
        [ResponseType(typeof(ExecutionThread))]
        public IHttpActionResult PostExecutionThread(ExecutionThread executionThread)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExecutionThreads.Add(executionThread);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = executionThread.ID }, executionThread);
        }

        // DELETE: api/ExecutionThreads/5
        [ResponseType(typeof(ExecutionThread))]
        public IHttpActionResult DeleteExecutionThread(int id)
        {
            ExecutionThread executionThread = db.ExecutionThreads.Find(id);
            if (executionThread == null)
            {
                return NotFound();
            }

            db.ExecutionThreads.Remove(executionThread);
            db.SaveChanges();

            return Ok(executionThread);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExecutionThreadExists(int id)
        {
            return db.ExecutionThreads.Count(e => e.ID == id) > 0;
        }
    }
}