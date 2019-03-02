using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOLD.CustomerDomain.MVC.LogicalUnits
{
    /// <summary>
    /// A convenient way to inject GOLD services into every LogicalUnit in the customer domain.
    /// </summary>
    public abstract class LogicalUnitBase : LogicalUnit
    {
        protected readonly IExecutionManager ExecutionManager;

        public LogicalUnitBase() : this(DependencyResolver.Current.GetService<IExecutionManager>())
        {
        }

        public LogicalUnitBase(IExecutionManager executionManager)
        {
            ExecutionManager = executionManager;
        }

        public abstract override Component GetNextComponent();

        public abstract override void HandleOutcome(Outcome outcome);
    }
}