using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeUpjohnWebPortfolioV2CMS.Code
{
    public static class Settings
    {
        public static class BodyClass
        {
            public static readonly string ACCOUNTS = "accounts";
            public static readonly string BLOGS = "blogs";
            public static readonly string PROJECTS = "projects";
            public static readonly string IMAGES = "images";
        }

        public static readonly string IMAGEUPLOADPATH = "~/uploads/";
        public static readonly int PAGINATIONITEMSPERPAGE = 10;
        public static readonly int PAGINATIONOFFSETITEMCOUNT = 5;
        public static readonly string SITENAME = "Mike Upjohn Web Portfolio V2 CMS";
    }
}