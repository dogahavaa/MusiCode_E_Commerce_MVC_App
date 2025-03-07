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
    public class ProductController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        // GET: ManagerPanel/Product
        public ActionResult Index()
        {
            return View(db.Products.Where(x => x.IsDeleted == false).ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                #region Resim Kontrolü
                if (imageFile != null)
                {
                    FileInfo fi = new FileInfo(imageFile.FileName);
                    if (fi.Extension == ".jpg" || fi.Extension == ".png")
                    {
                        string isim = Guid.NewGuid().ToString() + fi.Extension;
                        imageFile.SaveAs(Server.MapPath("~/Areas/ManagerPanel/Assets/images/ProductImages/" + isim));
                        model.Image = isim;
                    }
                    else
                    {
                        ViewBag.hataMesaji = "Dosya uzantısı .jpg veya .png olmalıdır.";
                    }
                }
                else
                {
                    model.Image = "noImage.png";
                }
                #endregion

                #region Fiyat Kontrolü

                string fiyat = model.Price.ToString();
                if (fiyat.Contains("."))
                {
                    fiyat = fiyat.Replace(".", ",");
                }
                model.Price = Convert.ToDouble(fiyat);

                #endregion

                try
                {
                    db.Products.Add(model);
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.hataMesaji = "Bir hata oluştu";
                }
            }
            return RedirectToAction("Index", "Product");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            Product b = db.Products.Find(id);
            if (b != null)
            {
                return View(b);
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }

        [HttpPost]
        public ActionResult Edit(Product model, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                #region Resim Kontrolü
                if (imageFile != null)
                {
                    FileInfo fi = new FileInfo(imageFile.FileName);
                    if (fi.Extension == ".jpg" || fi.Extension == ".png")
                    {
                        string isim = Guid.NewGuid().ToString() + fi.Extension;
                        imageFile.SaveAs(Server.MapPath("~/Areas/ManagerPanel/Assets/images/ProductImages/" + isim));
                        model.Image = isim;
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
                    TempData["mesaj"] = "Ürün güncelleme başarılı.";
                }
                catch
                {
                    ViewBag.hataMesaji = "Bir hata oluştu";
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Product");
        }


        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Product model = db.Products.Find(id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    db.SaveChanges();
                    TempData["mesaj"] = "Ürün başarıyla silindi.";
                }
            }
            return RedirectToAction("Index", "Product");
        }


        public ActionResult State(int? id)
        {
            if (id != null)
            {
                Product model = db.Products.Find(id);
                if (model != null)
                {
                    model.IsActive = !(model.IsActive);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Product");
        }
    }
}