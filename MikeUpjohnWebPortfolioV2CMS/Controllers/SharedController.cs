using MikeUpjohnWebPortfolioV2CMS.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeUpjohnWebPortfolioV2CMS.Controllers
{
    public class SharedController : BaseController
    {
        [ChildActionOnly]
        public PartialViewResult _DesktopNavigation()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _MobileNavigation()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _Pagination(int currentPage, int countOfItems)
        {
            string currentURL = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            int indexOfPaginationURL = currentURL.IndexOf("/page-");

            if(indexOfPaginationURL > 0)
            {
                currentURL = currentURL.Substring(0, indexOfPaginationURL);
            }

            currentURL = !(currentURL.EndsWith("/")) ? currentURL + "/" : currentURL;

            ViewBag.CurrentPage = currentPage;
            ViewBag.CurrentPageURL = currentURL;
            ViewBag.MaximumPage = Math.Ceiling((double)countOfItems / Settings.PAGINATIONITEMSPERPAGE);

            return PartialView();
        }
    }
}