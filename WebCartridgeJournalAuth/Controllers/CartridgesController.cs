using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCartridgeJournalAuth.Models;
using WebCartridgeJournalAuth.ViewModels;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebCartridgeJournalAuth.Controllers
{
    public class CartridgesController : Controller
    {
        public CartridgesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        // GET: Cartridges
        public ActionResult Index()
        {
            var cartridges = db.Cartridges.Include(c => c.Brand).Include(c => c.Color).Include(c => c.Department);
            return View(cartridges.ToList());
        }
        [AllowAnonymous]
        // GET: Cartridges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartridge cartridge = db.Cartridges.Find(id);
            if (cartridge == null)
            {
                return HttpNotFound();
            }
            return View(cartridge);
        }
        [AllowAnonymous]
        // GET: Cartridges/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepatmentName");
            return View();
        }

        // POST: Cartridges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartridgeID,ColorID,BrandId,DepartmentID,Purchase_Date,Installation_Date,Deinstallation_Date")] Cartridge cartridge)
        {
            if (ModelState.IsValid)
            {
                db.Cartridges.Add(cartridge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandID", "BrandName", cartridge.BrandId);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorId", "ColorName", cartridge.ColorID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepatmentName", cartridge.DepartmentID);
            return View(cartridge);
        }

        // GET: Cartridges/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartridge cartridge = db.Cartridges.Find(id);
            if (cartridge == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandID", "BrandName", cartridge.BrandId);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorId", "ColorName", cartridge.ColorID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepatmentName", cartridge.DepartmentID);
            return View(cartridge);
        }

        // POST: Cartridges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CartridgeID,ColorID,BrandId,DepartmentID,Purchase_Date,Installation_Date,Deinstallation_Date")] Cartridge cartridge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartridge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandID", "BrandName", cartridge.BrandId);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorId", "ColorName", cartridge.ColorID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepatmentName", cartridge.DepartmentID);
            return View(cartridge);
        }

        // GET: Cartridges/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartridge cartridge = db.Cartridges.Find(id);
            if (cartridge == null)
            {
                return HttpNotFound();
            }
            return View(cartridge);
        }

        // POST: Cartridges/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cartridge cartridge = db.Cartridges.Find(id);
            db.Cartridges.Remove(cartridge);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search()
        {
            var vm = new SearchViewModel();
            ViewBag.BrandId = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepatmentName");
            ViewBag.CartridgeID = new SelectList(db.Cartridges, "CartridgeId", "Purchase_Date");
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Search(SearchViewModel vm, string action)
        {

            vm.Catridges = db.Cartridges.Include(c => c.Brand).Include(c => c.Color).Include(c => c.Department)
                                                .Where(x => x.BrandId == vm.SearchedCartridge.BrandId
                                                 && x.ColorID == vm.SearchedCartridge.ColorID
                                                 && x.DepartmentID == vm.SearchedCartridge.DepartmentID
                                                /* && x.CartridgeID == vm.SearchedCartridge.CartridgeID*/);
            if (vm.SearchedCartridge.Purchase_Date != null)
            {
                vm.Catridges = vm.Catridges.Where(x => x.Purchase_Date >= vm.SearchedCartridge.Purchase_Date);
            }

            if (vm.SearchedCartridgeForInput2.Purchase_Date != null)
            {
                vm.Catridges = vm.Catridges.Where(x => x.Purchase_Date <= vm.SearchedCartridgeForInput2.Purchase_Date);
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorId", "ColorName");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepatmentName");
            ViewBag.CartridgeID = new SelectList(db.Cartridges, "CartridgeId", "Purchase_Date");

            if (action == "Export")
            {
                var excelTable = vm.Catridges.Select(x => new
                {
                    Модель = x.Brand.BrandName,
                    Цвет = x.Color.ColorName,
                    Куплен = x.Purchase_Date,
                    Установлен = x.Installation_Date,
                    Отдел = x.Department.DepatmentName
                });
                       
                var grid = new GridView();
                grid.DataSource = excelTable;
                grid.DataBind();

                
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "utf-8";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                string style = @"<style> .textmode { mso-number-format:\@; } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
                Response.Write(style);
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                return RedirectToAction("Search");
            }

            return View(vm);
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult ExportToExcel(Cartridge cartridge)
        //{
        //    var cartridgeSearchedList = new DataTable("customTestTable");

        //    //cartridgeSearchedList.Columns.Add("column 1", typeof(int));
        //    //cartridgeSearchedList.Columns.Add("column 2", typeof(string));

        //    //cartridgeSearchedList.Rows.Add(1, "cartridge 1");
        //    //cartridgeSearchedList.Rows.Add(2, "cartridge 2");

        //    var grid = new GridView();
        //    grid.DataSource = cartridgeSearchedList;
        //    grid.DataBind();

        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=Cartridge Report.xls");
        //    Response.ContentType = "application/ms-excel";

        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    grid.RenderControl(htw);

        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();

        //    return View(cartridge);
        //}
    }
}