﻿using System.Web;

namespace MinMVC
{
    public class RequestContext
    {
        public virtual HttpContextBase HttpContext { get; set; }
        public virtual RouteData RouteData { get; set; }
    }
}
