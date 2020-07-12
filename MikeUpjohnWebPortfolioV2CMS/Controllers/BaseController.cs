using MikeUpjohnWebPortfolioV2CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeUpjohnWebPortfolioV2CMS.Controllers
{
    public class BaseController : Controller
    {
        public MikeUpjohnCMSEntities db = new MikeUpjohnCMSEntities();
    }
}