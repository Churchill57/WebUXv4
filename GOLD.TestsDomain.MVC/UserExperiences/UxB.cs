using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Outcomes;
using GOLD.TestsDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.CustomerDomain.MVC.UserExperiences
{
    //[Authorize(Roles = "Admin, pleb")]

    // N.B. This particular component will not be a directly launchable component and so does not carry primary/secondary route attributes.
    // It can however be invoked from a LogicalUnit such as LuPreviewCustomer.
    public class UxB : UserExperience, IUxB
    {

    }
}