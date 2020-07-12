using MikeUpjohnWebPortfolioV2CMS.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    public class ImageListViewModel
    {
        public int ImageID { get; set; }
        public string ImageFileName { get; set; }
        public byte[] ImageFile { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ImageCreatedDate { get; set; }
        public DateTime ImageModifiedDate { get; set; }

        public long ImageSize
        {
            get
            {
                string fileName = Settings.IMAGEUPLOADPATH + ImageFileName;

                if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(fileName)))
                {
                    return new FileInfo(System.Web.HttpContext.Current.Server.MapPath(fileName)).Length;
                }

                return 0;
            }
        }

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
                this.hashedImageID = value;
            }
        }
    }
}