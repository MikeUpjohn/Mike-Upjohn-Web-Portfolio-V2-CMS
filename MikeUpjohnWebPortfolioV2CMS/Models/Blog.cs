//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MikeUpjohnWebPortfolioV2CMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Blog
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public System.DateTime BlogDate { get; set; }
        public string BlogSummary { get; set; }
        public string BlogPost { get; set; }
        public Nullable<int> BlogImageID { get; set; }
        public Nullable<int> BlogThumbnailImageID { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime BlogCreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Image Image { get; set; }
        public virtual Image Image1 { get; set; }
    }
}
