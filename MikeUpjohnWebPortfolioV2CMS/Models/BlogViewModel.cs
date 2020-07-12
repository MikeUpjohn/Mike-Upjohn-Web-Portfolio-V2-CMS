using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    public class BlogViewModel
    {
        public int BlogID { get; set; }

        [Required(ErrorMessage="Blog Title is a required field.")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage ="Blog Author is a required field.")]
        public string BlogAuthor { get; set; }

        [Required(ErrorMessage ="Blog Post Date is a required field.")]
        public DateTime BlogDate { get; set; }

        [AllowHtml]
        [Required(ErrorMessage ="Blog Summary is a required field.")]
        public string BlogSummary { get; set; }

        [AllowHtml]
        [Required(ErrorMessage ="Blog Post is a required field.")]
        public string BlogPost { get; set; }

        public int? BlogImageID { get; set; }
        public int? ThumbnailImageID { get; set; }

        [Required(ErrorMessage ="Blog Image is a required field.")]
        public string SelectedBlogImageID { get; set; }
        //public List<SelectListItem> BlogImage { get; set; }
        public SelectList BlogImages { get; set; }

        [Required(ErrorMessage ="Blog Thumbnail Image is a required field.")]
        public string SelectedThumbnailImageID { get; set; }
        //public List<SelectListItem> ThumbnailImages { get; set; }
        public SelectList BlogThumbnailImages { get; set; }

        public bool IsDisabled { get; set; }

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