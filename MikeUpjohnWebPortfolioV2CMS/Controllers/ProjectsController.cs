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
    public class ProjectsController : BaseController
    {
        public ActionResult Index(int? pageNumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                int page = pageNumber != null ? (int)pageNumber : 1;

                List<ProjectListViewModel> projectList = new List<ProjectListViewModel>();
                projectList = (from x in db.Projects
                               orderby x.ProjectPostDate
                               select new ProjectListViewModel
                               {
                                   ProjectID = x.ProjectID,
                                   ProjectTitle = x.ProjectTitle,
                                   ProjectPostDate = x.ProjectPostDate,
                                   IsDisabled = x.IsDisabled,
                                   IsDeleted = x.IsDeleted,
                                   LastModifiedDate = x.ModifiedDate
                               }).ToList();

                ViewBag.BodyClass = Code.Settings.BodyClass.PROJECTS;

                ViewBag.CurrentPage = page;
                ViewBag.CountOfItems = projectList.Count();

                return View(projectList.Skip((page - 1) * Settings.PAGINATIONITEMSPERPAGE).Take(Settings.PAGINATIONITEMSPERPAGE).ToList());
            }

            return HttpNotFound();
        }

        public ActionResult Edit(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ProjectViewModel projectViewModel = new ProjectViewModel();
                if (!string.IsNullOrEmpty(id))
                {
                    int projectID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(id), out projectID))
                    {
                        List<Project> projects = DatabaseCommands.GetProjects(true, false);

                        if (projects.Where(x => x.ProjectID == projectID).Count() == 1)
                        {
                            projectViewModel = (from x in projects
                                                where x.ProjectID == projectID
                                                select new ProjectViewModel
                                                {
                                                    ProjectID = x.ProjectID,
                                                    ProjectTitle = x.ProjectTitle,
                                                    ProjectDateDescription = x.ProjectDateDescription,
                                                    ProjectPostDate = x.ProjectPostDate,
                                                    ProjectSummary = x.ProjectSummary,
                                                    ProjectDescription = x.ProjectDescription,
                                                    ProjectLink = x.ProjectLink,
                                                    ProjectImageID = x.ProjectImageID,
                                                    ProjectThumbnailImageID = x.ProjectThumbnailImageID,
                                                    IsDisabled = x.IsDisabled
                                                }).SingleOrDefault();
                        }
                    }

                    ViewBag.IsNewProject = false;
                }
                else
                {
                    projectViewModel.ProjectID = 0;
                    projectViewModel.ProjectPostDate = DateTime.Now;
                    ViewBag.IsNewProject = true;

                }

                List<ImageListViewModel> images = new List<ImageListViewModel>();
                images = (from x in db.Images
                          where !x.IsDeleted
                          select new ImageListViewModel
                          {
                              ImageID = x.ImageID,
                              ImageFileName = x.ImageFileName
                          }).ToList();

                List<SelectListItem> projectImages = images.Select(s => new SelectListItem
                {
                    Value = s.HashedImageID,
                    Text = s.ImageFileName,
                }).ToList();

                if (projectViewModel.ProjectImageID != null && projectViewModel.ProjectImageID != 0)
                {
                    int projectImageID = (int)projectViewModel.ProjectImageID;
                    projectViewModel.ProjectImages = new SelectList(projectImages, "Value", "Text", CodeLibrary.CypherString.Encrypt(projectImageID));
                }
                else
                {
                    projectViewModel.ProjectImages = new SelectList(projectImages, "Value", "Text");
                }

                if (projectViewModel.ProjectThumbnailImageID != null && projectViewModel.ProjectThumbnailImageID != 0)
                {
                    int projectThumbnailImageID = (int)projectViewModel.ProjectThumbnailImageID;
                    projectViewModel.ProjectThumbnailImages = new SelectList(projectImages, "Value", "Text", CodeLibrary.CypherString.Encrypt(projectThumbnailImageID));
                }
                else
                {
                    projectViewModel.ProjectThumbnailImages = new SelectList(projectImages, "Value", "Text");
                }

                ViewBag.BodyClass = Settings.BodyClass.PROJECTS;

                return View(projectViewModel);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(ProjectViewModel form)
        {
            int projectID = 0;
            string hashedProjectID = form.HashedProjectID;
            if (int.TryParse(CodeLibrary.CypherString.Decrypt(hashedProjectID), out projectID))
            {
                if (ModelState.IsValid)
                {
                    Project project = new Project();
                    if (projectID > 0)
                    {
                        project = db.Projects.Where(x => x.ProjectID == projectID).SingleOrDefault();
                    }

                    project.ProjectTitle = form.ProjectTitle;
                    project.ProjectDateDescription = form.ProjectDateDescription;
                    project.ProjectPostDate = form.ProjectPostDate;
                    project.ProjectSummary = form.ProjectSummary;
                    project.ProjectDescription = form.ProjectDescription;
                    project.ProjectLink = form.ProjectLink;

                    int selectedProjectImageID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(form.SelectedProjectImageID), out selectedProjectImageID))
                    {
                        if (selectedProjectImageID > 0)
                        {
                            project.ProjectImageID = selectedProjectImageID;
                        }
                    }

                    int selectedProjectThumbnailImageID = 0;
                    if (int.TryParse(CodeLibrary.CypherString.Decrypt(form.SelectedThumbnailImageID), out selectedProjectThumbnailImageID))
                    {
                        if (selectedProjectThumbnailImageID > 0)
                        {
                            project.ProjectThumbnailImageID = selectedProjectThumbnailImageID;
                        }
                    }

                    project.IsDisabled = form.IsDisabled;
                    project.IsDeleted = false;
                    project.ModifiedBy = User.Identity.GetUserId();
                    project.ModifiedDate = DateTime.Now;

                    if (projectID == 0)
                    {
                        project.ProjectCreatedDate = DateTime.Now;
                        db.Projects.Add(project);
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index", "Projects");
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

            form.ProjectImages = new SelectList(blogImages, "Value", "Text", form.SelectedProjectImageID);
            form.ProjectThumbnailImages = new SelectList(blogImages, "Value", "Text", form.SelectedThumbnailImageID);

            ViewBag.BodyClass = Settings.BodyClass.PROJECTS;
            ViewBag.IsNewBlog = (projectID > 0 ? false : true);

            return View("Edit", form);
        }
    }
}