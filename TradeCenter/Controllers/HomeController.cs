using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeCenter.Models;

namespace TradeCenter.Controllers
{
    public class HomeController : Controller
    {
        private TradeCenterDB _context = new TradeCenterDB();

        //
        // GET: Home/Index
        public ActionResult Index(ProductsSortOrder sortOrder = ProductsSortOrder.PublishDate)
        {
            List<Product> products;

            switch (sortOrder)
            {
                case ProductsSortOrder.Title:
                    products = _context.Products.Include("Owner").OrderBy(p => p.Title).ToList();
                    break;
                case ProductsSortOrder.PublishDate:
                    products = _context.Products.Include("Owner").OrderByDescending(p => p.DatePublished).ToList();
                    break;
                default:
                    products = _context.Products.Include("Owner").ToList();
                    break;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", products);
            }

            return View(products);
        }

        //
        // GET: Home/About
        public ActionResult About()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
            base.Dispose(disposing);
        }

        ~HomeController()  // Overrides Object.Finalize()
        {
            Dispose();
        }
    }
}