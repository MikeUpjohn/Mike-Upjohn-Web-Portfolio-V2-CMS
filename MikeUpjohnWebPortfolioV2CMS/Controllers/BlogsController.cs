using Microsoft.AspNet.Identity;
using MikeUpjohnWebPortfolioV2CMS.Code;
using MikeUpjohnWebPortfolioV2CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeUpjohnWebPortfolioV2CMS.Controllers
{
    public class BlogsController : BaseController
    {
        public ActionResult Index(int? pageNumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                int page = pageNumber != null ? (int)pageNumber : 1;

                List<BlogListViewModel> blogList = new List<BlogListViewModel>();
                blogList = (from x in db.Blogs
                            orderby x.BlogDate descending
                            select new BlogListViewModel
                            {
                                BlogID = x.BlogID,
                                BlogTitle = x.BlogTitle,
                                BlogDate = x.BlogDate,
                                IsDisabled = x.IsDisabled,
                                IsDeleted = x.IsDeleted,
                                BlogModifiedDate = x.ModifiedDate
                            }).ToList();

                ViewBag.BodyClass = Settings.BodyClass.BLOGS;

                ViewBag.CurrentPage = page;
                ViewBag.CountOfItems = blogList.Count();
                return View(blogList.Skip((page - 1) * Settings.PAGINATIONITEMSPERPAGE).Take(Settings.PAGINATIONITEMSPERPAGE).ToList());
            }

            return HttpNotFound();
        }

        //public ActionResult Add()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        BlogViewModel blogViewModel = new BlogViewModel();
        //        blogViewModel.BlogID = 0;

        //        List<ImageListViewModel> images = new List<ImageListViewModel>();
        //        images = (from x in db.Images
        //                  where !x.IsDeleted
        //                  select new ImageListViewModel
        //                  {
        //                      ImageID = x.ImageID,
        //                      ImageFileName = x.ImageFileName
        //                  }).ToList();

        //        List<SelectListItem> blogImages = images.Select(s => new SelectListItem
        //        {
        //            Value = s.HashedImageID,
        //            Text = s.ImageFileName,
        //        }).ToList();

        //        blogViewModel.BlogImages = new SelectList(blogImages, 4);

        //        blogViewModel.ThumbnailImages = blogViewModel.BlogImages;

        //        ViewBag.BodyClass = Settings.BodyClass.BLOGS;
        //        ViewBag.IsNewBlog = true;

        //        return View(blogViewModel);
        //    }

        //    return HttpNotFound();
        //}

        public ActionResult Edit(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                BlogViewModel blogViewModel = new BlogViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    int blogID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(id), out blogID))
                    {
                        List<Blog> blogs = DatabaseCommands.GetBlogs(true, false);

                        if (blogs.Where(x => x.BlogID == blogID).Count() == 1)
                        {
                            blogViewModel = (from x in blogs
                                             where x.BlogID == blogID
                                             select new BlogViewModel
                                             {
                                                 BlogID = x.BlogID,
                                                 BlogTitle = x.BlogTitle,
                                                 BlogAuthor = x.BlogAuthor,
                                                 BlogDate = x.BlogDate,
                                                 BlogSummary = x.BlogSummary,
                                                 BlogPost = x.BlogPost,
                                                 BlogImageID = x.BlogImageID,
                                                 ThumbnailImageID = x.BlogThumbnailImageID,
                                                 IsDisabled = x.IsDisabled
                                             }).SingleOrDefault();
                        }
                    }

                    ViewBag.IsNewBlog = false;
                }
                else
                {
                    blogViewModel.BlogID = 0;
                    blogViewModel.BlogDate = DateTime.Now;

                    ViewBag.IsNewBlog = true;
                }

                List<ImageListViewModel> images = new List<ImageListViewModel>();
                images = (from x in db.Images
                          where !x.IsDeleted
                          select new ImageListViewModel
                          {
                              ImageID = x.ImageID,
                              ImageFileName = x.ImageFileName
                          }).ToList();

                List<SelectListItem> blogImages = images.Select(s => new SelectListItem
                {
                    Value = s.HashedImageID,
                    Text = s.ImageFileName,
                }).ToList();

                if (blogViewModel.BlogImageID != null && blogViewModel.BlogImageID != 0)
                {
                    int blogImageID = (int)blogViewModel.BlogImageID;
                    blogViewModel.BlogImages = new SelectList(blogImages, "Value", "Text", CodeLibrary.CypherString.Encrypt(blogImageID));
                }
                else
                {
                    blogViewModel.BlogImages = new SelectList(blogImages, "Value", "Text");
                }

                if (blogViewModel.ThumbnailImageID != null && blogViewModel.ThumbnailImageID != 0)
                {
                    int thumbnailImageID = (int)blogViewModel.ThumbnailImageID;
                    blogViewModel.BlogThumbnailImages = new SelectList(blogImages, "Value", "Text", CodeLibrary.CypherString.Encrypt(thumbnailImageID));
                }
                else
                {
                    blogViewModel.BlogThumbnailImages = new SelectList(blogImages, "Value", "Text");
                }

                ViewBag.BodyClass = Settings.BodyClass.BLOGS;

                return View(blogViewModel);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(BlogViewModel form)
        {
            if (ModelState.IsValid)
            {
                string hashedBlogID = form.HashedBlogID;
                int blogID = 0;

                if (int.TryParse(CodeLibrary.CypherString.Decrypt(hashedBlogID), out blogID))
                {
                    Blog blog = new Blog();
                    if (blogID > 0)
                    {
                        blog = db.Blogs.Where(x => x.BlogID == blogID).SingleOrDefault();
                    }

                    blog.BlogTitle = form.BlogTitle;
                    blog.BlogAuthor = form.BlogAuthor;
                    blog.BlogDate = form.BlogDate;
                    blog.BlogSummary = form.BlogSummary;
                    blog.BlogPost = form.BlogPost;

                    int selectedBlogImageID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(form.SelectedBlogImageID), out selectedBlogImageID))
                    {
                        if (selectedBlogImageID > 0)
                        {
                            blog.BlogImageID = selectedBlogImageID;
                        }
                    }

                    int selectedThumbnailImageID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(form.SelectedThumbnailImageID), out selectedThumbnailImageID))
                    {
                        if (selectedThumbnailImageID > 0)
                        {
                            blog.BlogThumbnailImageID = selectedThumbnailImageID;
                        }
                    }

                    blog.IsDisabled = form.IsDisabled;

                    blog.ModifiedBy = User.Identity.GetUserId();
                    blog.ModifiedDate = DateTime.Now;

                    if (blogID == 0)
                    {
                        blog.BlogCreatedDate = DateTime.Now;
                        db.Blogs.Add(blog);
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index", "Blogs");
                }
            }

            List<ImageListViewModel> images = new List<ImageListViewModel>();
            images = (from x in db.Images
                      where !x.IsDeleted
                      select new ImageListViewModel
                      {
                          ImageID = x.ImageID,
                          ImageFileName = x.ImageFileName
                      }).ToList();

            List<SelectListItem> blogImages = images.Select(s => new SelectListItem
            {
                Value = s.HashedImageID,
                Text = s.ImageFileName,
            }).ToList();

            form.BlogImages = new SelectList(blogImages, "Value", "Text", form.SelectedBlogImageID);
            form.BlogThumbnailImages = new SelectList(blogImages, "Value", "Text", form.SelectedThumbnailImageID);

            ViewBag.BodyClass = Settings.BodyClass.BLOGS;
            ViewBag.IsNewBlog = false;

            return View("Edit", form);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int blogID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(id), out blogID))
                    {
                        var blog = db.Blogs.Where(x => x.BlogID == blogID).SingleOrDefault();
                        if (blog != null)
                        {
                            blog.IsDeleted = true;
                            blog.ModifiedDate = DateTime.Now;
                            blog.ModifiedBy = User.Identity.GetUserId();

                            db.SaveChanges();

                            return Json("true");
                        }

                    }
                }
            }

            return Json("false");
        }
    }
}