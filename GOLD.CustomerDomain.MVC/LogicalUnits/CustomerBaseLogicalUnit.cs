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
    public abstract class CustomerBaseLogicalUnit : LogicalUnit
    {
        protected readonly IExecutionManager ExecutionManager;

        public CustomerBaseLogicalUnit() : this(DependencyResolver.Current.GetService<IExecutionManager>())
        {
        }

        public CustomerBaseLogicalUnit(IExecutionManager executionManager)
        {
            ExecutionManager = executionManager;
        }

        public abstract override Component GetNextComponent();

        public abstract override void HandleOutcome(Outcome outcome);
    }
}