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

        // POST: api/ExecutionThreads
        //[ResponseType(typeof(ExecutionThread))]
        public async Task<IHttpActionResult> PostExecutionThread(ExecutionThread executionThread)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var et = Map(executionThread);
            _dbContext.ExecutionThreads.Add(et);
            await _dbContext.SaveChangesAsync();

            var savedExecutionThread = Map(et);

            return CreatedAtRoute("DefaultApi", new { id = et.ID }, savedExecutionThread);
        }

        private GOLD.AppExecution.DataAccess.ExecutionThread Map(ExecutionThread executionThread)
        {
            return new GOLD.AppExecution.DataAccess.ExecutionThread()
            {

                ID = executionThread.ID,
                ComponentExecutingID = executionThread.ComponentExecutingID,
                ExecutingComponentsJson = JsonConvert.SerializeObject(executionThread.ExecutingComponents),
                ExecutingComponentTitle = executionThread.ExecutingComponentTitle,
                ExecutionStatus = (int)executionThread.ExecutionStatus,
                LaunchCommandLineJson = executionThread.LaunchCommandLine, // TODO: Parse launch command somewhere sometime!
                LaunchInputsJson = JsonConvert.SerializeObject(executionThread.LaunchInputs),
                LockDateTime = executionThread.LockDateTime,
                LockUserID = executionThread.LockUserID,
                LockUserName = executionThread.LockUserName,
                PendingOutcomeJson = JsonConvert.SerializeObject(executionThread.PendingOutcome),
                RootComponentTitle = executionThread.RootComponentTitle
            };
        }
        private ExecutionThread Map(GOLD.AppExecution.DataAccess.ExecutionThread executionThread)
        {
            return new ExecutionThread()
            {
                ID = executionThread.ID,
                ComponentExecutingID = executionThread.ComponentExecutingID,
                ExecutingComponents = JsonConvert.DeserializeObject<List<ExecutingComponent>>(executionThread.ExecutingComponentsJson),
                ExecutingComponentTitle = executionThread.ExecutingComponentTitle,
                ExecutionStatus = LogicalUnitStatusEnum.Initialised,  // TODO: What??
                LaunchCommandLine = executionThread.LaunchCommandLineJson,
                LaunchInputs = JsonConvert.DeserializeObject<Dictionary<string,string>>(executionThread.LaunchInputsJson),
                LockDateTime = executionThread.LockDateTime,
                LockUserID = executionThread.LockUserID,
                LockUserName = executionThread.LockUserName,
                PendingOutcome = JsonConvert.DeserializeObject<Outcome>(executionThread.LaunchInputsJson),
                RootComponentTitle = executionThread.RootComponentTitle
            };
        }

        //// GET: api/ExecutionThreads/5
        //[ResponseType(typeof(ExecutionThread))]
        //public IHttpActionResult GetExecutionThread(int id)
        //{
        //    ExecutionThread executionThread = _dbContext.ExecutionThreads.Find(id);
        //    if (executionThread == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(executionThread);
        //}

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