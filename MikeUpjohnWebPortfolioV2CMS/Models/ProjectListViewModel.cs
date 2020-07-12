using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    public class ProjectListViewModel
    {
        public int ProjectID { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime ProjectPostDate { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModifiedDate { get; set; }

        private string hashedProjectID = "";
        public string HashedProjectID
        {
            get
            {
                if (string.IsNullOrEmpty(hashedProjectID))
                {
                    hashedProjectID = CodeLibrary.CypherString.Encrypt(ProjectID);
                }

                return hashedProjectID;
            }
            set
            {
                this.hashedProjectID = value;
            }
        }
    }
}