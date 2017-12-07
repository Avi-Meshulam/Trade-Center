using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TradeCenter.Models;

namespace TradeCenter.Controllers
{
    public class ShoppingCartController : Controller
    {
        private TradeCenterDB _context = new TradeCenterDB();

        //
        // GET: ShoppingCart/ShoppingCart
        public ActionResult ShoppingCart()
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var products = _context.Products.Where(p => p.UserID == user.ID).ToList();

            if (products == null)
                return HttpNotFound();

            ViewBag.TotalPrice = CalcTotalAmount(products);

            return View(products);
        }

        //
        // GET: ShoppingCart/Remove/?productID={Product ID}
        public ActionResult Remove(int productID)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

            var product = _context.Products.SingleOrDefault(p => p.ID == productID);
            if (product == null)
                return HttpNotFound();

            product.State = ProductState.Avaialble;
            product.UserID = null;

            _context.SaveChanges();

            Messages.Set(AppMessages.ItemRemovedFromCart);

            return RedirectToAction("ShoppingCart");
        }

        //
        // GET: ShoppingCart/Buy
        public ActionResult Buy()
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

            _context.Products.Where(p => p.UserID == user.ID && p.State == ProductState.InCart).ToList()
                .ForEach(p =>
                {
                    p.State = ProductState.Sold;
                    p.OwnerID = (long)p.UserID;
                    p.UserID = null;
                });

            _context.SaveChanges();

            Messages.Set(AppMessages.TransactionCompleted);

            return RedirectToAction("ShoppingCart");
        }

        // Returns updated Total Price, formatted as currency
        public string chkProduct_Click(int productID)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var product = _context.Products.SingleOrDefault(p => p.ID == productID);

            if (product != null)
            {
                product.State = product.State == ProductState.InCart ?
                        ProductState.OnHold : ProductState.InCart;
                _context.SaveChanges();
            }
            
            return CalcTotalAmount(user).ToString("C");
        }

        // Returns Json(true) if there are items in cart
        public JsonResult btnBuy_Click()
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            return Json(
                new
                {
                    isCartFull = _context.Products.Any(p => p.UserID == user.ID && p.State == ProductState.InCart)
                });
        }

        private decimal CalcTotalAmount(IEnumerable<Product> products)
        {
            return products == null ? 0 :
                (products
                    .Where(p => p.State == ProductState.InCart)
                    .Sum(p => (decimal?)p.Price) ?? 0);
        }

        private decimal CalcTotalAmount(User user)
        {
            return user == null ? 0 :
                (_context.Products.
                    Where(p => p.UserID == user.ID && p.State == ProductState.InCart)
                    .Sum(p => (decimal?)p.Price) ?? 0);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
            base.Dispose(disposing);
        }

        ~ShoppingCartController()  // Overrides Object.Finalize()
        {
            Dispose();
        }
    }
}