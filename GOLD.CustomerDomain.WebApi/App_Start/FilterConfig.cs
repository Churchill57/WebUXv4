﻿using System.Web;
using System.Web.Mvc;

namespace GOLD.CustomerDomain.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
