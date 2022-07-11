using ProductStoresPip.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductStoresPip.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private ProductStoreEntities db=new ProductStoreEntities();
        public ActionResult Dashboard()
        {
            try
            {
                return View(db.Products.ToList());
            }
            catch (Exception ex)
            {
                return View(ex.Message, "error");
            }
        }
        public ActionResult Details(int? id)
        {
            return View(db.Products.Where(x=>x.id==id).FirstOrDefault());
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product pro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(pro);
                    db.SaveChanges();

                }
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data=db.Products.SingleOrDefault(x => x.id == id);
            if(data==null)
            {
                return HttpNotFound();
            }
            return View(data);

          
        }
       [HttpPost]
       public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
                return View(product);
            }
           catch(Exception ex)
            {
                return View(ex.Message, "Error");
            }
            
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.Products.SingleOrDefault(x => x.id == id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                return View(ex.Message,"Error");
            }
            
           
        }
        public ActionResult Search(string search)
        {
            try
            {
                if (search==null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var data = db.Products.Where(x => x.Product1.Contains(search)).ToList();
                    return View(data);
                }
            }
            catch(Exception ex)
            {
                return View(ex.Message, "error");
            }
        }
    }
}