using MikeUpjohnWebPortfolioV2CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeUpjohnWebPortfolioV2CMS.Code
{
    public static class DatabaseCommands
    {
        //public static MikeUpjohnCMSEntities db = new MikeUpjohnCMSEntities();

        //public static List<Image> GetImages(bool showDeleted)
        //{
        //    List<Image> images = db.Images.ToList();

        //    if(!showDeleted)
        //    {
        //        images = images.Where(x => !x.IsDeleted).ToList();
        //    }

        //    return images;
        //}

        public static List<Project> GetProjects(bool showDisabled, bool showDeleted)
        {
            using (MikeUpjohnCMSEntities db = new MikeUpjohnCMSEntities())
            {
                List<Project> projects = db.Projects.ToList();

                if (!showDisabled)
                {
                    projects = projects.Where(x => !x.IsDisabled).ToList();
                }

                if (!showDeleted)
                {
                    projects = projects.Where(x => !x.IsDeleted).ToList();
                }

                return projects;
            }
        }

        public static List<Blog> GetBlogs(bool showDisabled, bool showDeleted)
        {
            using (MikeUpjohnCMSEntities db = new MikeUpjohnCMSEntities())
            {
                List<Blog> blogs = db.Blogs.ToList();

                if (!showDisabled)
                {
                    blogs = blogs.Where(x => !x.IsDisabled).ToList();
                }

                if (!showDeleted)
                {
                    blogs = blogs.Where(x => !x.IsDeleted).ToList();
                }

                return blogs;
            }
        }

        public static List<ImageViewModel> GetImages(bool showDeleted)
        {
            using (MikeUpjohnCMSEntities db = new MikeUpjohnCMSEntities())
            {
                List<ImageViewModel> images = (from x in db.Images
                                      select new ImageViewModel
                                      {
                                          ImageID = x.ImageID,
                                          FileName = x.ImageFileName,
                                          CreatedDate = x.ImageCreatedDate,
                                          IsDeleted = x.IsDeleted,
                                          ModifiedBy = x.ModifiedBy,
                                          ModifiedDate = x.ModifiedDate
                                      }).ToList();

                if (!showDeleted)
                {
                    images = images.Where(x => !x.IsDeleted).ToList();
                }

                return images;
            }
        }
    }
}