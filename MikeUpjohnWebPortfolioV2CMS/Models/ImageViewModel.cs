using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    public class ImageViewModel
    {
        public int ImageID { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }

        private string hashedImageID = "";
        public string HashedImageID
        {
            get
            {
                if (string.IsNullOrEmpty(hashedImageID))
                {
                    hashedImageID = CodeLibrary.CypherString.Encrypt(ImageID);
                }

                return hashedImageID;
            }
            set
            {
                hashedImageID = value;
            }
        }
    }
}