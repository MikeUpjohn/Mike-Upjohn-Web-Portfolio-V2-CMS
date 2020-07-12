using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    public class ProjectViewModel
    {
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Project Title is a required field.")]
        [MaxLength(ErrorMessage = "Project Title must be no longer than 500 characters.")]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "Project Date Description is a required field.")]
        [MaxLength(ErrorMessage = "Project Date Description must be no longer than 50 characters.")]
        public string ProjectDateDescription { get; set; }

        [Required(ErrorMessage = "Project Post Date is a required field.")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime ProjectPostDate { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Project Summary is a requried field.")]
        public string ProjectSummary { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Project Description is a required field.")]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = "Project Link is a required field.")]
        [MaxLength(ErrorMessage = "Project Link must be no longer than 500 characters.")]
        public string ProjectLink { get; set; }

        public int? ProjectImageID { get; set; }
        public int? ProjectThumbnailImageID { get; set; }

        [Required(ErrorMessage = "Project Image is a required field.")]
        public string SelectedProjectImageID { get; set; }

        public SelectList ProjectImages { get; set; }

        [Required(ErrorMessage = "Project Thumnail Image is a required field.")]
        public string SelectedThumbnailImageID { get; set; }

        public SelectList ProjectThumbnailImages { get; set; }

        public bool IsDisabled { get; set; }

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