using MusiCodeWebApp.Areas.ManagerPanel.Filters;
using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerLoginRequiredFilter]
    public class BrandController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        // GET: ManagerPanel/Brand
        public ActionResult Index()
        {
            return View(db.Brands.Where(x => x.IsDeleted == false).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand model, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                #region Logo Kontrolü
                if (logo != null)
                {
                    FileInfo fi = new FileInfo(logo.FileName);
                    if (fi.Extension == ".jpg" || fi.Extension == ".png")
                    {
                        string isim = Guid.NewGuid().ToString() + fi.Extension;
                        logo.SaveAs(Server.MapPath("~/Areas/ManagerPanel/Assets/images/BrandImages/" + isim));
                        model.Logo = isim;
                    }
                    else
                    {
                        ViewBag.hataMesaji = "Dosya uzantısı .jpg veya .png olmalıdır.";
                    }
                }
                else
                {
                    model.Logo = "noImage.png";
                }
                #endregion

                try
                {
                    db.Brands.Add(model);
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.hataMesaji = "Bir hata oluştu";
                }
            }
            return RedirectToAction("Index", "Brand");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Brand b = db.Brands.Find(id);
            if (b != null)
            {
                return View(b);
            }
            else
            {
                return RedirectToAction("Index", "Brand");
            }
        }

        [HttpPost]
        public ActionResult Edit(Brand model, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                #region Logo Kontrolü
                if (logo != null)
                {
                    FileInfo fi = new FileInfo(logo.FileName);
                    if (fi.Extension == ".jpg" || fi.Extension == ".png")
                    {
                        string isim = Guid.NewGuid().ToString() + fi.Extension;
                        logo.SaveAs(Server.MapPath("~/Areas/ManagerPanel/Assets/images/BrandImages/" + isim));
                        model.Logo = isim;
                    }
                    else
                    {
                        ViewBag.hataMesaji = "Dosya uzantısı .jpg veya .png olmalıdır.";
                    }
                }
                #endregion

                try
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["mesaj"] = "Model güncelleme başarılı.";
                }
                catch
                {
                    ViewBag.hataMesaji = "Bir hata oluştu";
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Brand");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Brand model = db.Brands.Find(id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    db.SaveChanges();
                    TempData["mesaj"] = "Marka başarıyla silindi.";
                }
            }
            return RedirectToAction("Index", "Brand");
        }

        public ActionResult State(int? id)
        {
            if (id != null)
            {
                Brand model = db.Brands.Find(id);
                if (model != null)
                {
                    model.IsActive = !(model.IsActive);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Brand");
        }
    }
}