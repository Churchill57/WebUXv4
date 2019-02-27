using GOLD.Core.AppManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOLD.CustomerDomain.MVC.UserExperiences
{
    /// <summary>
    /// A convenient way to inject GOLD services into every UserExperience in the customer domain.
    /// </summary>
    public class UserExperienceBase
    {
        protected readonly IExecutionManager ExecutionManager;

        public UserExperienceBase() : this(DependencyResolver.Current.GetService<IExecutionManager>())
        {
        }

        public UserExperienceBase(IExecutionManager executionManager)
        {
            ExecutionManager = executionManager;
        }

    }
}