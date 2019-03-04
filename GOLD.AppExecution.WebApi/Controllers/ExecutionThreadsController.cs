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
using GOLD.AppExecution.ApiModels;
using GOLD.Core.Enums;
using GOLD.Core.Outcomes;
using Newtonsoft.Json;

namespace GOLD.AppExecution.WebApi.Controllers
{
    public class ExecutionThreadsController : ApiController
    {
        private AppExecution.DataAccess.AppExecutionDBContext _dbContext { get; }
        public ExecutionThreadsController(GOLD.AppExecution.DataAccess.AppExecutionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/ExecutionThreads/5
        [ResponseType(typeof(ExecutionThread))]
        public async Task<IHttpActionResult> GetExecutionThreadAsync(int id)
        {
            GOLD.AppExecution.DataAccess.ExecutionThread executionThreadDB = await _dbContext.ExecutionThreads.FindAsync(id);
            if (executionThreadDB == null) return NotFound();
            return Ok(Map(executionThreadDB));
        }


        // POST: api/ExecutionThreads
        [ResponseType(typeof(ExecutionThread))]
        public async Task<IHttpActionResult> PostExecutionThreadAsync(ExecutionThread executionThread)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var executionThreadDB = Map(executionThread);
            _dbContext.ExecutionThreads.Add(executionThreadDB);
            await _dbContext.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = executionThreadDB.ID }, Map(executionThreadDB));
        }

        [ResponseType(typeof(ExecutionThread))]
        public async Task<IHttpActionResult> PutExecutionThreadAsync(ExecutionThread executionThread)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var executionThreadDB = Map(executionThread);
            _dbContext.Entry(executionThreadDB).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(Map(executionThreadDB));
        }


        private GOLD.AppExecution.DataAccess.ExecutionThread Map(ExecutionThread executionThread)
        {
            return new GOLD.AppExecution.DataAccess.ExecutionThread()
            {

                ID = executionThread.ID,
                ComponentExecutingID = executionThread.ComponentExecutingID,
                ExecutingComponentsJson = JsonConvert.SerializeObject(executionThread.ExecutingComponents),
                ExecutingComponentTitle = executionThread.ExecutingComponentTitle,
                ExecutionStatus = executionThread.ExecutionStatus,
                LaunchCommandLineJson = executionThread.LaunchCommandLine, // TODO: Parse launch command somewhere sometime!
                LaunchInputsJson = JsonConvert.SerializeObject(executionThread.LaunchInputs),
                LockDateTime = executionThread.LockDateTime,
                LockUserID = executionThread.LockUserID,
                LockUserName = executionThread.LockUserName,
                PendingOutcomeJson = JsonConvert.SerializeObject(executionThread.PendingOutcome),
                RootComponentTitle = executionThread.RootComponentTitle
            };
        }
        private ExecutionThread Map(GOLD.AppExecution.DataAccess.ExecutionThread executionThreadDB)
        {
            return new ExecutionThread()
            {
                ID = executionThreadDB.ID,
                ComponentExecutingID = executionThreadDB.ComponentExecutingID,
                ExecutingComponents = JsonConvert.DeserializeObject<List<ExecutingComponent>>(executionThreadDB.ExecutingComponentsJson),
                ExecutingComponentTitle = executionThreadDB.ExecutingComponentTitle,
                ExecutionStatus = executionThreadDB.ExecutionStatus,
                LaunchCommandLine = executionThreadDB.LaunchCommandLineJson,
                LaunchInputs = JsonConvert.DeserializeObject<Dictionary<string,string>>(executionThreadDB.LaunchInputsJson),
                LockDateTime = executionThreadDB.LockDateTime,
                LockUserID = executionThreadDB.LockUserID,
                LockUserName = executionThreadDB.LockUserName,
                PendingOutcome = JsonConvert.DeserializeObject<GOLD.AppExecution.ApiModels.Outcome>(executionThreadDB.PendingOutcomeJson),
                RootComponentTitle = executionThreadDB.RootComponentTitle
            };
        }

        //// PUT: api/ExecutionThreads/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutExecutionThread(int id, ExecutionThread executionThread)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != executionThread.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _dbContext.Entry(executionThread).State = EntityState.Modified;

        //    try
        //    {
        //        _dbContext.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ExecutionThreadExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/ExecutionThreads
        //[ResponseType(typeof(ExecutionThread))]
        //public IHttpActionResult PostExecutionThread(ExecutionThread executionThread)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _dbContext.ExecutionThreads.Add(executionThread);
        //    _dbContext.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = executionThread.ID }, executionThread);
        //}

        //// DELETE: api/ExecutionThreads/5
        //[ResponseType(typeof(ExecutionThread))]
        //public IHttpActionResult DeleteExecutionThread(int id)
        //{
        //    ExecutionThread executionThread = _dbContext.ExecutionThreads.Find(id);
        //    if (executionThread == null)
        //    {
        //        return NotFound();
        //    }

        //    _dbContext.ExecutionThreads.Remove(executionThread);
        //    _dbContext.SaveChanges();

        //    return Ok(executionThread);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _dbContext.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ExecutionThreadExists(int id)
        //{
        //    return _dbContext.ExecutionThreads.Count(e => e.ID == id) > 0;
        //}
    }
}