using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    public class BlogListViewModel
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public DateTime BlogDate { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime BlogModifiedDate { get; set; }

        private string hashedBlogID = "";
        public string HashedBlogID
        {
            get
            {
                if (string.IsNullOrEmpty(hashedBlogID))
                {
                    hashedBlogID = CodeLibrary.CypherString.Encrypt(BlogID);
                }

                return hashedBlogID;
            }
            set
            {
                this.hashedBlogID = value;
            }
        }
    }
}