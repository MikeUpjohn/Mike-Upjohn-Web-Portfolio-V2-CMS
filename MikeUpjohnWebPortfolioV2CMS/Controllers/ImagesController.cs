using MikeUpjohnWebPortfolioV2CMS.Code;
using MikeUpjohnWebPortfolioV2CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MikeUpjohnWebPortfolioV2CMS.Controllers
{
    public class ImagesController : BaseController
    {
        public ActionResult Index(int? pageNumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                int page = pageNumber != null ? (int)pageNumber : 1;

                using (MikeUpjohnCMSEntities db = new MikeUpjohnCMSEntities())
                {
                    List<ImageListViewModel> imageList = new List<ImageListViewModel>();

                    imageList = (from x in db.Images
                                 select new ImageListViewModel
                                 {
                                     ImageID = x.ImageID,
                                     ImageFileName = x.ImageFileName,
                                     ImageCreatedDate = x.ImageCreatedDate,
                                     ImageModifiedDate = x.ModifiedDate,
                                     IsDeleted = x.IsDeleted
                                 }).ToList();

                    ViewBag.CurrentPage = pageNumber;
                    ViewBag.CountOfItems = imageList.Count();

                    ViewBag.BodyClass = Settings.BodyClass.IMAGES;
                    return View(imageList.Skip((page - 1) * Settings.PAGINATIONITEMSPERPAGE).Take(Settings.PAGINATIONITEMSPERPAGE).ToList());
                }
            }

            return HttpNotFound();
        }

        public ActionResult Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.BodyClass = Settings.BodyClass.IMAGES;
                return View();
            }

            return HttpNotFound();
        }

        public ActionResult Edit(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string decryptedImageID = CodeLibrary.CypherString.Decrypt(id);
                int imageID = 0;

                if (!string.IsNullOrEmpty(decryptedImageID))
                {
                    if (int.TryParse(decryptedImageID, out imageID))
                    {
                        ImageViewModel image = new ImageViewModel();
                        image = (from x in db.Images
                                 where x.ImageID == imageID
                                 select new ImageViewModel
                                 {
                                     ImageID = x.ImageID,
                                     FileName = x.ImageFileName
                                 }).SingleOrDefault();

                        ViewBag.BodyClass = Settings.BodyClass.IMAGES;
                        return View(image);
                    }
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Add(ImageViewModel form)
        {
            if (ModelState.IsValid)
            {
                // Copy image file to server
                var fileName = "";
                var originalFileExtension = Path.GetExtension(form.ImageFile.FileName);

                if (!string.IsNullOrEmpty(form.FileName))
                {
                    fileName = form.FileName;
                    if (!fileName.EndsWith(originalFileExtension))
                    {
                        fileName = (fileName.Contains(".") ? fileName.Remove(fileName.IndexOf(".")) : fileName);
                        fileName += originalFileExtension;
                    }
                }
                else
                {
                    fileName = form.ImageFile.FileName;
                }

                form.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/uploads/"), fileName));

                MemoryStream target = new MemoryStream();
                form.ImageFile.InputStream.CopyTo(target);

                Image image = new Image();
                image.ImageFileName = fileName;


                image.ImageFile = target.ToArray();
                image.IsDeleted = false;
                image.ImageCreatedDate = DateTime.Now;
                image.ModifiedDate = DateTime.Now;
                image.ModifiedBy = User.Identity.GetUserId();

                db.Images.Add(image);
                db.SaveChanges();

                return RedirectToAction("Index", "Images");
            }

            return null;
        }

        [HttpPost]
        public ActionResult Edit(ImageViewModel form)
        {
            if (ModelState.IsValid)
            {
                string decryptedImageID = CodeLibrary.CypherString.Decrypt(form.HashedImageID);
                int imageID = 0;

                if (!string.IsNullOrEmpty(decryptedImageID))
                {
                    if (int.TryParse(decryptedImageID, out imageID))
                    {
                        //List<ImageViewModel> images = DatabaseCommands.GetImages(false);
                        db.Database.CommandTimeout = 600;
                        var image = db.Images.Where(x => x.ImageID == imageID).SingleOrDefault();

                        if (image != null)
                        {
                            string newFileName = "";

                            if (!string.IsNullOrEmpty(form.FileName))
                            {
                                string originalFileExtension = Path.GetExtension(image.ImageFileName);
                                newFileName = form.FileName;

                                if (!newFileName.EndsWith(originalFileExtension))
                                {
                                    newFileName = (newFileName.Contains(".") ? newFileName.Remove(newFileName.IndexOf(".")) : newFileName);
                                    newFileName += originalFileExtension;
                                }

                                System.IO.File.Move(Server.MapPath("~/uploads/" + image.ImageFileName), Server.MapPath("~/uploads/" + newFileName));

                                image.ImageFileName = newFileName;
                                image.ModifiedDate = DateTime.Now;
                                image.ModifiedBy = User.Identity.GetUserId();

                                db.SaveChanges();
                            }

                            return RedirectToAction("Index", "Images");
                        }
                    }
                }

                return RedirectToAction("Edit", "Images", new { @id = form.HashedImageID });
            }

            return null;
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int imageID = 0;
                if (int.TryParse(CodeLibrary.CypherString.Decrypt(id), out imageID))
                {
                    db.Database.CommandTimeout = 600;
                    var image = db.Images.Where(x => x.ImageID == imageID).SingleOrDefault();
                    if (image != null)
                    {
                        image.IsDeleted = true;
                        image.ModifiedDate = DateTime.Now;
                        image.ModifiedBy = User.Identity.GetUserId();

                        db.SaveChanges();

                        return Json("true");
                    }
                }

                return Json("false");
            }

            return Json("false");
        }
    }
}